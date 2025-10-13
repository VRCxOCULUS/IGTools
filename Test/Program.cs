using System;
using System.IO;

namespace DAT1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream;
            BinaryReader br;
            DAT1 test;

            //Console.WriteLine("Marvel's Spider-Man Remastered\n");
            ////Console.WriteLine("hero_spiderman_2099.model");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_2099.model", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_spiderman_uk_mask.material\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_uk_mask.material", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_spiderman2099_head_c.texture\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman2099_head_c.texture", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            //Console.WriteLine("\n\nvanity_spiderman_amazingsuit.config\n");
            //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanity_spiderman_amazingsuit.config", FileMode.Open);
            //br = new BinaryReader(stream);
            //test = new DAT1(br);
            //Console.WriteLine("\n\n----------------------------\n\n");

            //Console.WriteLine("Marvel's Spider-Man 2\n");
            ////Console.WriteLine("hero_spiderman_advanced.model\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced.model", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_spiderman_advanced_head.material\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced_head.material", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_spiderman_advanced_head_c.texture\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_spiderman_advanced_head_c.texture", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            //Console.WriteLine("\n\nvanity_i30_advanced_suit.config\n");
            //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanity_i30_advanced_suit.config", FileMode.Open);
            //br = new BinaryReader(stream);
            //test = new DAT1(br);
            //Console.WriteLine("\n\n----------------------------\n\n");

            //Console.WriteLine("Marvel's Wolverine\n");
            ////Console.WriteLine("hero_wolverine_battle.model\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle.model", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_wolverine_battle_torso.material\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle_torso.material", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            ////Console.WriteLine("\n\nhero_wolverine_battle_c.texture\n");
            ////stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/hero_wolverine_battle_torso_c.texture", FileMode.Open);
            ////br = new BinaryReader(stream);
            ////test = new DAT1(br);
            //Console.WriteLine("\n\nvanitybody_battle.config\n");
            //stream = File.Open("C:/Users/27alexander.smith_ca/Desktop/Personal/DAT1/DAT1/GameFiles/vanitybody_battle.config", FileMode.Open);
            //br = new BinaryReader(stream);
            //test = new DAT1(br);

            stream = File.Open("C:/Users/Agony/Downloads/vanity_i30_advanced_suit.config", FileMode.Open);
            br = new BinaryReader(stream);
            test = new DAT1(br);
        }
    }
}
