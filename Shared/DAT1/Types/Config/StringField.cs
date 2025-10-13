using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Shared;

namespace DAT1
{
    public class StringField
    {
        public UInt32 CharLength;
        public UInt32 CRC32;
        public UInt64 CRC64N;
        public string Value;

        public StringField(BinaryReader br, DAT1 header)
        {
            CharLength = br.ReadUInt32();
            CRC32 = br.ReadUInt32();
            CRC64N = br.ReadUInt64();

            Value = new string(br.ReadChars((int)CharLength));

            //br.BaseStream.Seek(Align.To4((int)br.BaseStream.Position + 1), 0x00);

            br.ReadBytes(4);

            //Console.WriteLine($"Length: {CharLength}");
            //Console.WriteLine($"CRC32 hash: {CRC32.ToString("X")}");
            //Console.WriteLine($"Normalized CRC64: {CRC64N.ToString("X")}");
            Console.WriteLine($"Value: {Value}");
        }
    }
}
