using Shared;
using System;
using System.Collections.Generic;
using System.IO;

namespace DAT1
{
    public class ConfigBlock
    {
        UInt32 FieldCount;
        UInt32 ObjectSize;

        List<(UInt32, UInt16, UInt16)> FieldInfos = new List<(uint, ushort, ushort)> { };
        List<string> FieldNames = new List<string>();

        List<object> FieldValues = new List<object>();

        public ConfigBlock(BinaryReader br, DAT1 header, int blockOffset)
        {
            br.BaseStream.Seek(blockOffset + 0x08, 0x00);

            FieldCount = br.ReadUInt32();
            ObjectSize = br.ReadUInt32();
            Console.WriteLine($"FieldCount: {FieldCount.ToString("X")}");
            Console.WriteLine($"ObjectSize: {ObjectSize.ToString("X")}");


            var tempOffset = br.BaseStream.Position;
            for (int i = 0; i < FieldCount; i++)
            {
                br.BaseStream.Seek(tempOffset + (i * 8), 0x00);
                var ID = br.ReadUInt32();
                Console.WriteLine($"ID: {ID.ToString("X")}");
                var Flags = br.ReadUInt16();
                Console.WriteLine($"Flags: {Flags.ToString("X")}");
                br.BaseStream.Seek(br.BaseStream.Position + 0x01, 0x00);
                var Type = br.ReadByte();
                Console.WriteLine($"Type: {Type.ToString("X")}");
                FieldInfos.Add((ID, Flags, Type));
            }
            tempOffset = br.BaseStream.Position;
            for (int i = 0; i < FieldCount; i++)
            {
                //Console.WriteLine($"pos: {br.BaseStream.Position.ToString("X")}");
                var NameOffset = br.ReadUInt32();
                //Console.WriteLine($"NameOffset: {NameOffset.ToString("X")}");
                var tempOffset2 = br.BaseStream.Position;

                br.BaseStream.Seek(header.offset + NameOffset, 0x00);
                List<char> FieldNameList = new List<char> { };
                while (true)
                {
                    FieldNameList.Add(br.ReadChar());
                    if (FieldNameList.Contains('\0'))
                    {
                        FieldNameList.Remove('\0');
                        break;
                    }
                }
                string FieldName = new string(FieldNameList.ToArray());

                Console.WriteLine($"Field: {FieldName}");

                br.BaseStream.Seek(tempOffset2, 0x00);
            }

            for (int i = 0; i < FieldCount; i++)
            {
                switch (FieldInfos[i].Item3)
                {
                    /*UInt8*/   case 0x00: FieldValues.Add(br.ReadSByte()); break;
                    /*UInt16*/  case 0x01: FieldValues.Add(br.ReadInt16()); break;
                    /*UInt32*/  case 0x02: FieldValues.Add(br.ReadInt32()); break;
                    /*Int8*/    case 0x04: FieldValues.Add(br.ReadByte()); break;
                    /*Int16*/   case 0x05: FieldValues.Add(br.ReadUInt16()); break;
                    /*Int32*/   case 0x06: FieldValues.Add(br.ReadInt32()); break;
                    /*Float*/   case 0x08: FieldValues.Add(br.ReadSingle()); break;
                    /*String*/  case 0x0A: Console.WriteLine("String Field"); FieldValues.Add(new StringField(br, header)); break;
                    /*Object*/  case 0x0D: FieldValues.Add(new ConfigBlock(br, header, (int)br.BaseStream.Position)); break;
                    /*Bool*/    case 0x0F: FieldValues.Add(br.ReadByte() == 1 ? true : false); br.BaseStream.Seek(Align.To4((int)br.BaseStream.Position), 0x00); break;
                    /*ID*/      case 0x11: FieldValues.Add(br.ReadUInt64()); break;
                    /*Null*/    case 0x13: FieldValues.Add(null); break;
                    /*Unknown*/ default:   Console.WriteLine($"Unknown Field Type: {FieldInfos[i].Item3.ToString("X")}"); break;
                }
            }

            foreach (object item in FieldValues)
            {
                if (item is StringField stringField)
                {
                    Console.WriteLine($"Length: {stringField.CharLength}");
                    Console.WriteLine($"CRC32 hash: {stringField.CRC32.ToString("X")}");
                    Console.WriteLine($"Normalized CRC64: {stringField.CRC64N.ToString("X")}");
                    Console.WriteLine($"Value: {stringField.Value}");
                }
            }
        }
    }

    public class ConfigDef
    {
        public ConfigDef(BinaryReader br, DAT1 header)
        {
            ConfigBlock TypeBlock = new ConfigBlock(br, header, (int)(header.offset + header.BlockInfos[0].Item2));
            ConfigBlock BuiltBlock = new ConfigBlock(br, header, (int)(header.offset + header.BlockInfos[1].Item2));
        }
    }
}
