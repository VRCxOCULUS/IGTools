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
        public UInt32 CRC64N;
        public string Value;

        public StringField(BinaryReader br, DAT1 header)
        {
            CharLength = br.ReadUInt32();
            CRC32 = br.ReadUInt32();
            CRC64N = br.ReadUInt32();

            List<char> ValueList = new List<char> { };
            while (true)
            {
                ValueList.Add(br.ReadChar());
                if (ValueList.Contains('\0'))
                {
                    ValueList.Remove('\0');
                    break;
                }
            }
            Value = new string(ValueList.ToArray());
        }
    }
}
