using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Shared;

namespace DAT1
{
    public class DAT1
    {
        public Int32 MagicTest => 1145132081;

        public UInt32 Magic;
        public UInt32 Version;
        public UInt32 Size;
        public UInt16 BlockCount;
        public UInt16 FixupCount;

        public DAT1(BinaryReader br)
        {
            if(br.ReadUInt32() != 4674643 && br.ReadUInt32() != MagicTest)
            {
                br.BaseStream.Seek(36, 0x00);
                if(br.ReadUInt32() != MagicTest)
                {
                    throw new Exception("Not DAT1 file. Remove STG header or try another file.");
                }
            }

            br.BaseStream.Seek(0x00, 0x00);

            if (br.ReadUInt32() == 4674643)
            {
                br.BaseStream.Seek(0x08, 0x00);
                var offset = br.ReadUInt32();
                br.BaseStream.Seek((Align.To16(offset) + 0x10), 0x00);
            }
            else if(br.ReadUInt32() == MagicTest)
            {
                br.BaseStream.Seek(0x00, 0x00);
            }
            else
            {
                br.BaseStream.Seek(36, 0x00);
            }

            Magic = br.ReadUInt32();
            Version = br.ReadUInt32();
            Size = br.ReadUInt32();
            BlockCount = br.ReadUInt16();
            FixupCount = br.ReadUInt16();

            Console.WriteLine(Magic);
            Console.WriteLine(Version);
            Console.WriteLine(Size);
            Console.WriteLine(BlockCount);
            Console.WriteLine(FixupCount);
        }
    }
}
