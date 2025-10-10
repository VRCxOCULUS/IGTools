using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shared;

namespace DAT1
{
    public class Config
    {
        UInt32 FieldCount;
        UInt32 ObjectSize;

        List<(UInt32, UInt16, UInt16, string)> FieldInfos = new List<(uint, ushort, ushort, string)> { };

        List<object> FieldValues = new List<object>();

        public Config(BinaryReader br, DAT1 header)
        {
            br.BaseStream.Seek(header.offset + header.BlockInfos[0].Item2 + 0x08, 0x00);
            FieldCount = br.ReadUInt32();
            ObjectSize = br.ReadUInt32();
            Console.WriteLine($"FieldCount: {FieldCount.ToString("X")}");
            Console.WriteLine($"ObjectSize: {ObjectSize.ToString("X")}");


            var tempOffset = br.BaseStream.Position;
            for (int i = 0; i < FieldCount; i++)
            {
                br.BaseStream.Seek(tempOffset + (i * 12), 0x00);
                var ID = br.ReadUInt32();
                var Flags = br.ReadUInt16();
                var Type = br.ReadUInt16();
                var NameOffset = br.ReadUInt32();

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

                FieldInfos.Add((ID, Flags, Type, FieldName));

                for (int x = 0; x < FieldCount; x++)
                {
                    Console.WriteLine($"Field {(x + 1)} Name: {FieldInfos[x].Item4}");
                }

                br.BaseStream.Seek(tempOffset, 0x00);

                switch (FieldInfos[i].Item3)
                {
                    /*UInt8*/   case 0x00: /*FieldValues.Add();*/ break;
                    /*UInt16*/  case 0x01: /*FieldValues.Add();*/ break;
                    /*UInt32*/  case 0x02: /*FieldValues.Add();*/ break;
                    /*Int8*/    case 0x04: /*FieldValues.Add();*/ break;
                    /*Int16*/   case 0x05: /*FieldValues.Add();*/ break;
                    /*Int32*/   case 0x06: /*FieldValues.Add();*/ break;
                    /*Float*/   case 0x08: FieldValues.Add(BitConverter.ToSingle(br.ReadBytes(4), 0)); break;
                    /*String*/  case 0x0A: FieldValues.Add(new StringField(br, header)); break;
                    /*Object*/  case 0x0D: FieldValues.Add(new Config(br, header)); break;
                    /*Bool*/    case 0x0F: FieldValues.Add(br.ReadByte() == 1 ? true : false); break;
                    /*ID*/      case 0x11: FieldValues.Add(br.ReadUInt64()); break;
                    /*Null*/    case 0x13: FieldValues.Add(null); break;
                    default:Console.WriteLine(FieldInfos[i].Item3.ToString("X")); break;
                }
            }
        }
    }
}
