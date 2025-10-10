using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextureViewer
{
    public class Texture
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BlockDef
        {
            public UInt32 id;
            public UInt32 offset;
            public UInt32 size;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TextureHeader
        {
            public UInt32 magic;
            public UInt32 version;
            public UInt32 size;
            public UInt16 blockCount;
            public UInt16 fixupCount;
            public BlockDef ddsInfoDef;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DDSInfo
        {
            public Int32 sdSize;
            public Int32 hdSize;
            public Int16 hdWidth;
            public Int16 hdHeight;
            public Int16 sdWidth;
            public Int16 sdHeight;
            public Int64 flags;
            public Int16 pixelFormat;
            public byte sdMips;
            public byte hdMips;
            public Int32 unknown;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Asset
        {
            public TextureHeader textureHeader;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] textureBuiltFileString;

            public DDSInfo ddsInfo;
        }

        public static Asset LoadTextureAsset(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);


            // Get the address of the pinned byte array
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(fileData, 128);

            // Create an instance of the struct and populate it from the byte array
            Asset asset = (Asset)Marshal.PtrToStructure(ptr, typeof(Asset));

            return asset;
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Path to asset: ");
            string filePath = Console.ReadLine();

            Texture.Asset textureAsset = Texture.LoadTextureAsset(filePath);

            Console.WriteLine($"Magic: {textureAsset.textureHeader.magic}");
            Console.WriteLine($"Version: {textureAsset.textureHeader.version}");
            Console.WriteLine($"Size: {textureAsset.textureHeader.size}");
            Console.WriteLine($"BlockCount: {textureAsset.textureHeader.blockCount}");
            Console.WriteLine($"FixupCount: {textureAsset.textureHeader.fixupCount}");
            Console.WriteLine($"DDSInfoDef - ID: {textureAsset.textureHeader.ddsInfoDef.id}");
            Console.WriteLine($"DDSInfoDef - Offset: {textureAsset.textureHeader.ddsInfoDef.offset}");
            Console.WriteLine($"DDSInfoDef - Size: {textureAsset.textureHeader.ddsInfoDef.size}");

            string assetTypeStr = new string(textureAsset.textureBuiltFileString).TrimEnd('\0');
            Console.WriteLine($"AssetTypeString: {assetTypeStr}");

            Console.WriteLine($"DDSInfo - SD Width: {textureAsset.ddsInfo.sdWidth}");
            Console.WriteLine($"DDSInfo - HD Width: {textureAsset.ddsInfo.hdWidth}");
        }
    }
}