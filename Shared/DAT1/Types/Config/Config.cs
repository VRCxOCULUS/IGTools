using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace DAT1
{
    public class ObjectBlock
    {
        UInt32 FieldCount;
        UInt32 ObjectSize;

        List<(UInt32 NameCRC, UInt16 Flags, byte Type)> FieldInfos = new List<(uint NameCRC, ushort Flags, byte Type)>();
        List<string> FieldNames = new List<string>();
        List<object> FieldValues = new List<object>();

        public ObjectBlock(BinaryReader br, DAT1 header, int blockOffset)
        {
            br.BaseStream.Seek(blockOffset, SeekOrigin.Begin);

            ulong magic = br.ReadUInt64();
            if (magic != 0x0315004400000000)
                Console.WriteLine($"Warning: unexpected object magic 0x{magic:X}");

            FieldCount = br.ReadUInt32();
            ObjectSize = br.ReadUInt32();

            Console.WriteLine($"FieldCount: {FieldCount}");
            Console.WriteLine($"ObjectSize: {ObjectSize}");

            // Read field info table
            for (int i = 0; i < FieldCount; i++)
            {
                uint nameCrc = br.ReadUInt32();
                ushort flags = br.ReadUInt16();
                br.ReadByte(); // unknown byte, always 0x00
                byte type = br.ReadByte();

                FieldInfos.Add((nameCrc, flags, type));

                Console.WriteLine($"Field {i}: CRC={nameCrc:X8} Flags={flags:X4} Type={type:X2}");
            }

            // Read name offsets
            List<int> nameOffsets = new List<int>();
            for (int i = 0; i < FieldCount; i++)
            {
                int nameOffset = br.ReadInt32();
                nameOffsets.Add(nameOffset);
            }

            // Read field names from string block
            foreach (var nameOffset in nameOffsets)
            {
                long prev = br.BaseStream.Position;
                br.BaseStream.Seek(header.offset + nameOffset, SeekOrigin.Begin);

                var nameBuilder = new List<byte>();
                byte c;
                while ((c = br.ReadByte()) != 0x00)
                    nameBuilder.Add(c);

                string fieldName = Encoding.UTF8.GetString(nameBuilder.ToArray());
                FieldNames.Add(fieldName);
                Console.WriteLine($"Field name: {fieldName}");

                br.BaseStream.Seek(prev, SeekOrigin.Begin);
            }

            // Read field values
            for (int i = 0; i < FieldCount; i++)
            {
                var info = FieldInfos[i];
                object value = ReadFieldValue(br, header, info.Type);
                FieldValues.Add(value);
            }

            // Debug print
            for (int i = 0; i < FieldCount; i++)
            {
                Console.WriteLine($"Field {FieldNames[i]} ({FieldInfos[i].Type:X2}) = {FieldValues[i]}");
            }
        }

        private object ReadFieldValue(BinaryReader br, DAT1 header, byte type)
        {
            long start = br.BaseStream.Position;
            object value = null;

            switch (type)
            {
                case 0x00: value = br.ReadByte(); break; // UInt8
                case 0x01: value = br.ReadUInt16(); break; // UInt16
                case 0x02: value = br.ReadUInt32(); break; // UInt32
                case 0x04: value = br.ReadSByte(); break; // Int8
                case 0x05: value = br.ReadInt16(); break; // Int16
                case 0x06: value = br.ReadInt32(); break; // Int32
                case 0x08: value = br.ReadSingle(); break; // Float
                case 0x0A: value = ReadStringField(br); break; // String
                case 0x0D:
                    Console.WriteLine("Nested object field | entering subobject");
                    value = new ObjectBlock(br, header, (int)br.BaseStream.Position);
                    break;
                case 0x0F:
                    value = br.ReadByte() != 0;
                    break;
                case 0x11:
                    value = br.ReadUInt64();
                    break;
                case 0x13:
                    value = null; // Null
                    break;
                default:
                    Console.WriteLine($"Unknown field type {type:X2}");
                    break;
            }

            AlignStream(br);
            return value;
        }

        private void AlignStream(BinaryReader br)
        {
            long pos = br.BaseStream.Position;
            long aligned = (pos + 3) & ~3;
            if (aligned != pos)
                br.BaseStream.Seek(aligned, SeekOrigin.Begin);
        }

        private string ReadStringField(BinaryReader br)
        {
            int length = br.ReadInt32();
            uint crc32 = br.ReadUInt32();
            ulong crc64 = br.ReadUInt64();

            byte[] strData = br.ReadBytes(length);
            string value = Encoding.UTF8.GetString(strData);

            br.ReadByte(); // null terminator
            AlignStream(br);

            Console.WriteLine($"StringField: \"{value}\" (len={length}, crc32={crc32:X8}, crc64={crc64:X16})");
            return value;
        }

        // Writer method
        public void WriteToJson(JsonWriter writer)
        {
            for (int i = 0; i < FieldNames.Count; i++)
            {
                string name = FieldNames[i];
                object value = FieldValues[i];

                writer.WritePropertyName(name);

                if (value is ObjectBlock nestedBlock)
                {
                    writer.WriteStartObject();
                    nestedBlock.WriteToJson(writer);
                    writer.WriteEndObject();
                }
                else if (value == null)
                {
                    writer.WriteStartObject();
                    writer.WriteEndObject();
                }
                else
                {
                    writer.WriteValue(value);
                }
            }
        }
    }


    public class Config
    {
        public Config(BinaryReader br, DAT1 header)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\27alexander.smith_ca\Desktop\test.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                for (int i = 0; i < 2; i++)
                {
                    var (crc, offset, size) = header.BlockInfos[i];
                    Console.WriteLine($"--- Reading Block {i} (CRC={crc:X8}) ---");
                    ObjectBlock block = new ObjectBlock(br, header, (int)(header.offset + offset));

                    if(i == 0)
                        writer.WritePropertyName("Type");
                    else
                        writer.WritePropertyName("Def");
                    writer.WriteStartObject();
                    block.WriteToJson(writer);
                    writer.WriteEndObject();
                }

                writer.WriteEndObject();
            }
        }
    }

}
