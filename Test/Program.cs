using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Text;
using Shared;

namespace DAT1
{

    class Program
    {
        //static void Main(string[] args)
        //{
        //    Stream stream;
        //    BinaryReader br;
        //    DAT1 test;

        //    Console.WriteLine("Marvel's Spider-Man Remastered\n");
        //    //Console.WriteLine("hero_spiderman_2099.model");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_2099.model", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    //Console.WriteLine("\n\nhero_spiderman_uk_mask.material\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_uk_mask.material", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    //Console.WriteLine("\n\nhero_spiderman2099_head_c.texture\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman2099_head_c.texture", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    Console.WriteLine("\n\nvanity_spiderman_amazingsuit.config\n");
        //    stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanity_spiderman_amazingsuit.config", FileMode.Open);
        //    br = new BinaryReader(stream);
        //    test = new DAT1(br);
        //    Console.WriteLine("\n\n----------------------------\n\n");
        //    Console.ReadLine();

        //    Console.WriteLine("Marvel's Spider-Man 2\n");
        //    //Console.WriteLine("hero_spiderman_advanced.model\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced.model", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    //Console.WriteLine("\n\nhero_spiderman_advanced_head.material\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced_head.material", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    //Console.WriteLine("\n\nhero_spiderman_advanced_head_c.texture\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced_head_c.texture", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //    Console.WriteLine("\n\nvanity_i30_advanced_suit.config\n");
        //    stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanity_i30_advanced_suit.config", FileMode.Open);
        //    br = new BinaryReader(stream);
        //    test = new DAT1(br);
        //    Console.WriteLine("\n\n----------------------------\n\n");
        //    Console.ReadLine();

        //    //Console.WriteLine("Marvel's Wolverine\n");
        //    ////Console.WriteLine("hero_wolverine_battle.model\n");
        //    ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle.model", FileMode.Open);
        //    ////br = new BinaryReader(stream);
        //    ////test = new DAT1(br);
        //    ////Console.WriteLine("\n\nhero_wolverine_battle_torso.material\n");
        //    ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle_torso.material", FileMode.Open);
        //    ////br = new BinaryReader(stream);
        //    ////test = new DAT1(br);
        //    ////Console.WriteLine("\n\nhero_wolverine_battle_c.texture\n");
        //    ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle_torso_c.texture", FileMode.Open);
        //    ////br = new BinaryReader(stream);
        //    ////test = new DAT1(br);
        //    //Console.WriteLine("\n\nvanitybody_battle.config\n");
        //    //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanitybody_battle.config", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);

        //    //stream = File.Open("C:/Users/Agony/Downloads/vanity_i30_advanced_suit.config", FileMode.Open);
        //    //br = new BinaryReader(stream);
        //    //test = new DAT1(br);
        //}

        static void Main(string[] args)
        {
            BinaryReader br = new BinaryReader(File.OpenRead("C:/Users/27alexander.smith_ca/Desktop/Personal/IGTools/GameFiles/hero_spiderman_advanced_blue_a.material"));
            Shared.DAT1 test = new Shared.DAT1();

            test.m_DataFileId = br.ReadUInt32();
            test.m_VersionNumber = br.ReadUInt32();
            test.m_FileSize = br.ReadUInt32();
            test.m_BlockCount = br.ReadUInt16();
            test.m_FixupCount = br.ReadUInt16();

            Shared.DAT1.DataBlockHeader[] BlockHeaders = new Shared.DAT1.DataBlockHeader[test.m_BlockCount];

            for (int i = 0; i < test.m_BlockCount; i++)
            {
                BlockHeaders[i] = new Shared.DAT1.DataBlockHeader
                {
                    m_NameHash = br.ReadUInt32(),
                    m_Offset = br.ReadUInt32(),
                    m_Size = br.ReadUInt32()
                };
                Console.WriteLine($"Block {i}: {(Shared.Hashes.MaterialHashes)BlockHeaders[i].m_NameHash}");
            }
        }
    }
}
