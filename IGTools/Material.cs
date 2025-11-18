using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace IGTools
{
    public class Material
    {
        public struct MaterialHeader
        {
            public UInt64 m_TemplatePath;

            public UInt64 m_Flags;

            public UInt32[] m_AVMaterialHash; // 4
            public UInt32[] m_AudioMaterialHash; // 4

            public float m_Alpha;
            public float m_AlphaTest;

            public float m_LodDist;
            public byte m_VoxelizationOrderBias;
            public sbyte m_LGCaptureBehavior;
            public byte m_ColorVolLayerMask;
            public byte m_PadA;

            public byte m_SsrMinGlossUNorm8;
            public byte m_SsrMaxGlossUNorm8;
            public Int16 m_ShadowBiasI16;
            public UInt32 m_AVMaterialColorVolumeMask; // Lower 16 bits contain kAvMaterialLayerMask_* swizzles packed into 4 nibbles for each layer, upper 16 bits contain the valid mask and bias packed into 4 nibbles
        }

        public struct MaterialSerializedData
        {
            public UInt32 m_TotalSize;

            public UInt32 m_ConstantCount;
            public UInt32 m_ConstantDataOffset;

            public UInt32 m_VariationCount;
            public UInt32 m_VariationOffset;

            public UInt32 m_TextureCount;
            public UInt32 m_TextureOffset;
            public UInt32 m_TextureDataOffset;

            public UInt32 m_ExclusionCount;
            public UInt32 m_ExclusionOffset;
        }

        public struct MaterialConst
        {
            public UInt16 m_BufferOffset;
            public UInt16 m_BufferSize;
            public UInt32 m_Hash;
        }

        public struct MaterialSamplerLight
        {
            public UInt32 m_SourcePathOffset;
            public UInt32 m_Hash;
        }



        public string Name = "default.material";
        public string Template = "required/materials/basic_normal_gloss.materialgraph";

        public void LoadMaterial(string AssetPath)
        {
            BinaryReader br = new BinaryReader(File.Open(AssetPath, FileMode.Open));

            Name = Path.GetFileName(AssetPath);

            DAT1 DAT1 = new DAT1();

            DAT1.m_DataFileId = br.ReadUInt32();
            DAT1.m_VersionNumber = br.ReadUInt32();
            DAT1.m_FileSize = br.ReadUInt32();
            DAT1.m_BlockCount = br.ReadUInt16();
            DAT1.m_FixupCount = br.ReadUInt16();

            DAT1.DataBlockHeader[] BlockHeaders = new DAT1.DataBlockHeader[DAT1.m_BlockCount];
            DAT1.DataFileFixup[] BlockFixups = new DAT1.DataFileFixup[DAT1.m_FixupCount];

            for (int i = 0; i < DAT1.m_BlockCount; i++)
            {
                BlockHeaders[i] = new DAT1.DataBlockHeader
                {
                    m_NameHash = br.ReadUInt32(),
                    m_Offset = br.ReadUInt32(),
                    m_Size = br.ReadUInt32()
                };
            }

            for (int i = 0; i < DAT1.m_FixupCount; i++)
            {
                BlockFixups[i] = new DAT1.DataFileFixup
                {
                    m_SourceOffset = br.ReadUInt32(),
                    m_TargetOffset = br.ReadUInt32()
                };
            }

            br.BaseStream.Seek(BlockFixups[0].m_TargetOffset, SeekOrigin.Begin);
            Template = DAT1.ReadNullTermString(br);
            var headerBlock = DAT1.GetBlockById(BlockHeaders, (UInt32)Hashes.MaterialHashes.MaterialHeader);

            br.BaseStream.Seek(headerBlock.m_Offset, SeekOrigin.Begin);
            MaterialHeader header = new MaterialHeader
            {
                m_TemplatePath = br.ReadUInt64(),
                m_Flags = br.ReadUInt64(),
                m_AVMaterialHash = new uint[4]
                {
                    br.ReadUInt32(),
                    br.ReadUInt32(),
                    br.ReadUInt32(),
                    br.ReadUInt32()
                },
                m_AudioMaterialHash = new uint[4]
                {
                    br.ReadUInt32(),
                    br.ReadUInt32(),
                    br.ReadUInt32(),
                    br.ReadUInt32()
                },
                m_Alpha = br.ReadSingle(),
                m_AlphaTest = br.ReadSingle(),
                m_LodDist = br.ReadSingle(),
                m_VoxelizationOrderBias = br.ReadByte(),
                m_LGCaptureBehavior = br.ReadSByte(),
                m_ColorVolLayerMask = br.ReadByte(),
                m_PadA = br.ReadByte(),
                m_SsrMinGlossUNorm8 = br.ReadByte(),
                m_SsrMaxGlossUNorm8 = br.ReadByte(),
                m_ShadowBiasI16 = br.ReadInt16(),
                m_AVMaterialColorVolumeMask = br.ReadUInt32()
            };

            var serializedDataBlock = DAT1.GetBlockById(BlockHeaders, (UInt32)Hashes.MaterialHashes.MaterialSerializedData);
            br.BaseStream.Seek(serializedDataBlock.m_Offset, SeekOrigin.Begin);
            MaterialSerializedData materialSerialized = new MaterialSerializedData
            {
                m_TotalSize = br.ReadUInt32(),
                m_ConstantCount = br.ReadUInt32(),
                m_ConstantDataOffset = br.ReadUInt32(),
                m_VariationCount = br.ReadUInt32(),
                m_VariationOffset = br.ReadUInt32(),
                m_TextureCount = br.ReadUInt32(),
                m_TextureOffset = br.ReadUInt32(),
                m_TextureDataOffset = br.ReadUInt32(),
                m_ExclusionCount = br.ReadUInt32(),
                m_ExclusionOffset = br.ReadUInt32()
            };

            var m_Consts = new MaterialConst[materialSerialized.m_ConstantCount];
            for (int i = 0; i < m_Consts.Length; i++)
            {
                m_Consts[i] = new MaterialConst
                {
                    m_BufferOffset = br.ReadUInt16(),
                    m_BufferSize = br.ReadUInt16(),
                    m_Hash = br.ReadUInt32()
                };
            }

            float[][] constants = new float[m_Consts.Length][];
            for (int i = 0; i < m_Consts.Length; i++)
            {
                switch ((m_Consts[i].m_BufferSize / 4))
                {
                    case 0:
                        constants[i] = Array.Empty<float>();
                        break;
                    case 1: // Single
                        constants[i] = new float[1]
                        {
                            br.ReadSingle()
                        };
                        break;
                    case 2: // Vector2
                        constants[i] = new float[2]
                        {
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 3: // Vector3 / Color
                        constants[i] = new float[3]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 4: 
                        constants[i] = new float[4]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 5: 
                        constants[i] = new float[5]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 6: 
                        constants[i] = new float[6]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 7: 
                        constants[i] = new float[7]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 8:
                        constants[i] = new float[8]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                        case 9:
                        constants[i] = new float[9]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 10:
                        constants[i] = new float[10]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 11:
                        constants[i] = new float[11]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    case 12:
                        constants[i] = new float[12]
                        {
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle(),
                            br.ReadSingle()
                        };
                        break;
                    default:
                        throw new Exception("Unsupported material constant size");
                }
            }
            var m_Samplers = new MaterialSamplerLight[materialSerialized.m_TextureCount];
            for (int i = 0; i < m_Samplers.Length; i++)
            {
                m_Samplers[i] = new MaterialSamplerLight
                {
                    m_SourcePathOffset = br.ReadUInt32(),
                    m_Hash = br.ReadUInt32()
                };
            }

            string[] texturePaths = new string[m_Samplers.Length];
            for (int i = 0; i < texturePaths.Length; i++)
            {
                br.BaseStream.Seek(serializedDataBlock.m_Offset + materialSerialized.m_TextureDataOffset + m_Samplers[i].m_SourcePathOffset, SeekOrigin.Begin);
                texturePaths[i] = DAT1.ReadNullTermString(br);
                MessageBox.Show($"{m_Samplers[i].m_Hash}: {texturePaths[i]}");
            }
        }
    }
}
