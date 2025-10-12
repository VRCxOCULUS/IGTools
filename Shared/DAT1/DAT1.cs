using System;
using System.Collections.Generic;
using System.IO;

using Shared;


namespace DAT1
{
    public class DAT1
    {
        public Int32 MagicTest => 1145132081;
        public Int32 offset = 0x10;

        public UInt32 Magic;
        public UInt32 Version;
        public UInt32 Size;
        public UInt16 BlockCount;
        public UInt16 FixupCount;

        public List<(UInt32, UInt32, UInt32)> BlockInfos = new List<(uint, uint, uint)> { };
        public List<(UInt32, UInt32)> FixupInfos = new List<(uint, uint)> { };

        public DAT1(BinaryReader br)
        {
            //---Find DAT1 file start---
            if(br.ReadUInt32() != 4674643 && br.ReadUInt32() != MagicTest)
            {
                br.BaseStream.Seek(36, 0x00);
                if(br.ReadUInt32() != MagicTest)
                {
                    throw new Exception("Not DAT1 file.");
                }
            }

            br.BaseStream.Seek(0x00, 0x00);

            if (br.ReadUInt32() == 4674643)
            {
                br.BaseStream.Seek(0x08, 0x00);
                offset += Align.To16(br.ReadInt32());
                offset += Align.To16(br.ReadInt32());
                br.BaseStream.Seek(offset, 0x00);
            }
            else if(br.ReadUInt32() == MagicTest)
            {
                offset = 0;
            }
            else
            {
                offset = 36;
            }

            br.BaseStream.Seek(offset, 0x00);
            //--------------------------------------//

            Magic = br.ReadUInt32();
            Version = br.ReadUInt32();
            Size = br.ReadUInt32();
            BlockCount = br.ReadUInt16();
            FixupCount = br.ReadUInt16();

            Console.WriteLine($"Pre-asset(TOC/STG) data size: 0x{offset.ToString("X")}");

            Console.WriteLine($"DAT1 magic: 0x{Magic.ToString("X")}");
            Console.WriteLine($"Asset version: 0x{Version.ToString("X4")}");
            Console.WriteLine($"Asset size: {Size}");
            Console.WriteLine($"Blocks: {BlockCount}");
            Console.WriteLine($"Fixups: {FixupCount}");

            for (int i = 0; i < BlockCount; i++)
            {
                var ID = br.ReadUInt32();
                var Offset = br.ReadUInt32();
                var Size = br.ReadUInt32();

                BlockInfos.Add((ID, Offset, Size));
            }

            for (int i = 0; i < FixupCount; i++)
            {
                var Start = br.ReadUInt32();
                var End = br.ReadUInt32();

                FixupInfos.Add((Start, End));
            }


            for (int i = 0; i < BlockInfos.Count; i++)
            {
                Console.WriteLine($"Block {(i + 1)} ID: 0x{BlockInfos[i].Item1.ToString("X8")}");
                Console.WriteLine($"Block {(i + 1)} Offset: 0x{BlockInfos[i].Item2.ToString("X")}");
                Console.WriteLine($"Block {(i + 1)} Size: {BlockInfos[i].Item3} bytes");
            }

            for (int i = 0; i < FixupInfos.Count; i++)
            {
                Console.WriteLine($"Fixup {(i + 1)} Start address: 0x{FixupInfos[i].Item1.ToString("X")}");
                Console.WriteLine($"Fixup {(i + 1)} End address: 0x{FixupInfos[i].Item2.ToString("X")}");
            }

            List<char> assetTypeList = new List<char> { };
            while (true)
            {
                assetTypeList.Add(br.ReadChar());
                if (assetTypeList.Contains('\0'))
                {
                    assetTypeList.Remove('\0');
                    break;
                }
            }
            string assetType = new string(assetTypeList.ToArray());


            switch (assetType)
            {
                case "Config Built File":
                    ConfigDef config = new ConfigDef(br, this);
                    break;
                case "Material Built File": Console.WriteLine("Material Built File"); break;
                case "Model Built File": Console.WriteLine("Model Built File"); break;
                case "Texture Built File": Console.WriteLine("Texture Built File"); break;
                default: throw new Exception("Asset type not supported.");
            }
        }
    }
}
