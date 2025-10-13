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
            br.BaseStream.Seek(blockOffset, 0x00);

            br.ReadUInt64(); // 0x0000000040001503
            FieldCount = br.ReadUInt32();
            ObjectSize = br.ReadUInt32();
            //Console.WriteLine($"FieldCount: {FieldCount.ToString("X")}");
            //Console.WriteLine($"ObjectSize: {ObjectSize.ToString("X")}");


            var tempOffset = br.BaseStream.Position;
            for (int i = 0; i < FieldCount; i++)
            {
                br.BaseStream.Seek(tempOffset + (i * 8), 0x00);
                var ID = br.ReadUInt32();
                //Console.WriteLine($"ID: {ID.ToString("X")}");
                var Flags = br.ReadUInt16();
                //Console.WriteLine($"Flags: {Flags.ToString("X")}");
                br.BaseStream.Seek(br.BaseStream.Position + 0x01, 0x00);
                var Type = br.ReadByte();
                //Console.WriteLine($"Type: {Type.ToString("X")}");
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
                    /*UInt8*/   case 0x00: byte UInt8value = br.ReadByte(); FieldValues.Add(UInt8value); Console.WriteLine($"Value: {UInt8value}"); break;
                    /*UInt16*/  case 0x01: UInt16 UInt16value = br.ReadUInt16(); FieldValues.Add(UInt16value); Console.WriteLine($"Value: {UInt16value}"); break;
                    /*UInt32*/  case 0x02: UInt32 UInt32value = br.ReadUInt32(); FieldValues.Add(UInt32value); Console.WriteLine($"Value: {UInt32value}"); break;
                    /*Int8*/    case 0x04: sbyte Int8value = br.ReadSByte(); FieldValues.Add(Int8value); Console.WriteLine($"Value: {Int8value}"); break;
                    /*Int16*/   case 0x05: Int16 Int16value = br.ReadInt16(); FieldValues.Add(Int16value); Console.WriteLine($"Value: {Int16value}"); break;
                    /*Int32*/   case 0x06: Int32 Int32value = br.ReadInt32(); FieldValues.Add(Int32value); Console.WriteLine($"Value: {Int32value}"); break;
                    /*Float*/   case 0x08: float Floatvalue = br.ReadSingle(); FieldValues.Add(Floatvalue); Console.WriteLine($"Value: {Floatvalue}"); break;
                    /*String*/  case 0x0A: string Stringvalue; FieldValues.Add(new StringField(br, header)); break;
                    /*Object*/  case 0x0D: FieldValues.Add(new ConfigBlock(br, header, (int)br.BaseStream.Position)); Console.WriteLine("Value: Object"); break;
                    /*Bool*/    case 0x0F: bool boolValue = (br.ReadInt32() == 1 ? true : false);  FieldValues.Add(boolValue); Console.WriteLine($"Value: {boolValue}"); break;
                    /*ID*/      case 0x11: FieldValues.Add(br.ReadUInt64()); break;
                    /*Null*/    case 0x13: FieldValues.Add(null); break;
                    /*Unknown*/ default:   Console.WriteLine($"Unknown Field Type: {FieldInfos[i].Item3.ToString("X")}"); break;
                }
            }

            //foreach (object item in FieldValues)
            //{
            //    if (item is StringField stringField)
            //    {
            //        Console.WriteLine($"Length: {stringField.CharLength}");
            //        Console.WriteLine($"CRC32 hash: {stringField.CRC32.ToString("X")}");
            //        Console.WriteLine($"Normalized CRC64: {stringField.CRC64N.ToString("X")}");
            //        Console.WriteLine($"Value: {stringField.Value}");
            //    }
            //}
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
