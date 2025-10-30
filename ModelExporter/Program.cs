using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Schema2;

using static ModelExporter.Model;

namespace ModelExporter
{
    using VERTEX = SharpGLTF.Geometry.VertexTypes.VertexPosition;
    public static class GlobalJson
    {
        private static readonly string _path = "model_blocks.json";

        public static void WriteObject<T>(string key, T obj)
        {
            JObject root;

            // Load or create a root JSON object
            if (File.Exists(_path))
            {
                root = JObject.Parse(File.ReadAllText(_path));
            }
            else
            {
                root = new JObject();
            }

            // Serialize your object into a JObject
            var serialized = JObject.FromObject(obj);
            root[key] = serialized;

            // Write back to file
            File.WriteAllText(_path, root.ToString(Formatting.Indented));
        }

        public static T ReadObject<T>(string key)
        {
            if (!File.Exists(_path))
                return default;

            var root = JObject.Parse(File.ReadAllText(_path));
            if (root[key] == null)
                return default;

            return root[key].ToObject<T>();
        }
    }

    public static class Hashes
    {
        public static Dictionary<uint, string> ModelHashes = new Dictionary<uint, string>
        {
            { 675087235, "Model Built" },
            { 2027539422, "Model Subset" },
            { 4109016600, "Model Subset Geom Data" },
            { 3, "Model Subset Skin Data" },
            { 116096764, "Model Look" },
            { 1288610989, "Model Look Group" },
            { 2165899991, "Model Look Built" },
            { 7, "Model Look BVH Data" },
            { 3751796498, "Model Look BVH Info" },
            { 9, "Model Look BVH Lod Info" },
            { 844151680, "Model Material" },
            { 2429400588, "Model Joint Hierarchy" },
            { 366976315, "Model Joint" },
            { 3308604256, "Model Mirror Ids" },
            { 3073904268, "Model Leaf Ids" },
            { 15, "Model Spline Radii" },
            { 181643016, "Model Joint Bspheres" },
            { 3996227356, "Model Joint Lookup" },
            { 3704130073, "Model Bind Pose" },
            { 741122565, "Model Inv Bind Pose" },
            { 2673954731, "Model Locator" },
            { 1931263022, "Model Locator Lookup" },
            { 22, "Model Collision Index Data" },
            { 23, "Model Collision Vertex Data" },
            { 24, "Model Collision Complexity" },
            { 25, "Model Physics Data" },
            { 26, "Model's Ragdoll meta data" },
            { 27, "Model's Destructible meta data" },
            { 28, "Model's Cloth meta data" },
            { 2588101417, "Model's ik setup data" },
            { 115494612, "Model Anim Vert Info2" },
            { 31, "Model Anim Morph2 Info" },
            { 32, "Model Anim Morph2 Batches" },
            { 33, "Model Anim Morph2 Valid Masks" },
            { 34, "Model Anim Morph2 Deltas" },
            { 35, "Model Anim Geom Info" },
            { 36, "Model Anim Geom Particles" },
            { 37, "Model Anim Geom Mesh Info" },
            { 1720976974, "Model Anim Ziva2 Info" },
            { 39, "Model Anim Vert Normal Info" },
            { 40, "Model Anim Vert Normal Stitches" },
            { 1427668738, "Model Anim Vert Smooth Info" },
            { 42, "Model BVol" },
            { 43, "Model VGroup" },
            { 44, "Model Mesh Names" },
            { 45, "Model Content Anim Vert Infos" },
            { 46, "Model Texture Overrides" },
            { 47, "Model Render Overrides" },
            { 2091089312, "Ambient Shadow Prims" },
            { 49, "Model Anim Dynamics Def" },
            { 50, "Model Spline Subsets" },
            { 51, "Model Splines" },
            { 52, "Model Splines CVs" },
            { 53, "Model Spline Skin Binding" },
            { 54, "Model Spline Joint Binding" },
            { 55, "Model Spline Joint Weights" },
            { 56, "Model Ray-Tracing Parameters" },
            { 57, "Model Ray-Tracing Additional Parameters" },
            { 58, "Default" },
            { 59, "default" },

            { 60, "Model Lod Info Built" },
            { 61, "Model Lod Pool Built" },
            { 62, "Model Lod Geometry" },
            { 63, "Model Lod Material Names" },
            { 64, "Model Lod Bone Names" },
            { 65, "Model Lod Joint Names" },
            { 66, "Model Lod Anim Vert Key" },
        };
    }

    public class Utils
    {
        public static float ConvertUInt16ToFloat(ushort value)
        {
            // Handle special cases (zero, infinity, NaN)
            if (value == 0x0000) return 0.0f; // Positive zero
            if (value == 0x8000) return -0.0f; // Negative zero
            if (value == 0x7C00) return float.PositiveInfinity;
            if (value == 0xFC00) return float.NegativeInfinity;
            if ((value & 0x7C00) == 0x7C00 && (value & 0x03FF) != 0) return float.NaN; // NaN

            // Extract sign, exponent, and mantissa
            int sign = (value >> 15) & 0x01;
            int exponent = (value >> 10) & 0x1F;
            int mantissa = value & 0x3FF;

            // Adjust exponent for single-precision (bias of 127 vs 15)
            // Handle subnormal numbers
            if (exponent == 0)
            {
                // Subnormal half-precision number
                // Convert to normal single-precision representation
                exponent = 127 - 14; // Equivalent single-precision exponent for smallest normal half
                while ((mantissa & 0x400) == 0)
                {
                    mantissa <<= 1;
                    exponent--;
                }
                mantissa &= 0x3FF; // Remove implicit leading 1
            }
            else
            {
                exponent = exponent - 15 + 127; // Adjust bias
            }

            // Reconstruct single-precision float bits
            uint floatBits = (uint)(sign << 31 | exponent << 23 | mantissa << 13);
            return BitConverter.ToSingle(BitConverter.GetBytes(floatBits), 0);
        }

        public static int IndexOfBlock(DAT1.DataBlockHeader[] BlockHeaders, Dictionary<uint, string> Hashes, string BlockName)
        {
            for (int i = 0; i < BlockHeaders.Length; i++)
            {
                if (Hashes.TryGetValue(BlockHeaders[i].m_NameHash, out var name) && name == BlockName)
                    return i;
            }
            return -1;
        }

        /*
         Vec3 NormalDecodeAzimuthal( const Vec3& azim )
        {
            // The encoded values are from 0..1 and we're scaling and biasing them to -sqrt(2) to sqrt(2)
            Vec2 enc;
            enc.x            = azim.x * (4.0f / 1.41421356f) - (2.0f / 1.41421356f);
            enc.y            = azim.y * (4.0f / 1.41421356f) - (2.0f / 1.41421356f);
            float f          = VecDot( enc, enc );
            
            Vec3 vec;
            vec.AsVec2() = enc * Sqrtf( 1.0f - f * 0.25f );
            vec.z        = Absf( 1.0f - f * 0.5f );
            vec.z        = (azim.z < 0.5f) ? -vec.z : vec.z;
            
            return vec;
        }
         */

        public static Vector4 VecFromR10G10B10A2_UNORM(uint v)
        {
            uint x = (v) & 0x3FF;
            uint y = (v >> 10) & 0x3FF;
            uint z = (v >> 20) & 0x3FF;
            uint w = (v >> 30) & 0x3;

            return new Vector4(
                x * (1.0f / 1023.0f),
                y * (1.0f / 1023.0f),
                z * (1.0f / 1023.0f),
                w * (1.0f / 3.0f)
            );
        }

        public static void DecodeNormalTangent(float[] Position, uint NormalTangent, out Vector3 normal, out Vector3 tangent)
        {
            Vector4 normalTanEnc = VecFromR10G10B10A2_UNORM(NormalTangent);
            int posWAbs = Math.Abs((int)Position[3]);

            float nt_zs = normalTanEnc.W * 3.0f;
            float normalZ = Saturate(nt_zs - 1.0f);
            float tangentZ = nt_zs - (normalZ * 2.0f);

            // Normal decode
            normal = NormalDecodeAzimuthal(new Vector3(normalTanEnc.X, normalTanEnc.Y, normalZ));

            // Tangent decode (using low 10 bits from position.w)
            float tangentYEnc = (posWAbs & 0x3FF) * (1.0f / 1023.0f);
            tangent = NormalDecodeAzimuthal(new Vector3(normalTanEnc.Z, tangentYEnc, tangentZ));
        }

        public static Vector3 NormalDecodeAzimuthal(Vector3 azim)
        {
            // Scale and bias from [0,1] → [-√2, √2]
            float scale = 4.0f / 1.41421356f;
            float bias = 2.0f / 1.41421356f;

            Vector2 enc = new Vector2(
                azim.X * scale - bias,
                azim.Y * scale - bias
            );

            float f = Vector2.Dot(enc, enc);

            Vector3 vec;
            float s = (float)Math.Sqrt(Math.Max(0.0f, 1.0f - f * 0.25f));

            vec = new Vector3(
                enc.X * s,
                enc.Y * s,
                Math.Abs(1.0f - f * 0.5f)
            );

            if (azim.Z < 0.5f)
                vec.Z = -vec.Z;

            return vec;
        }
        public static float Saturate(float x)
        {
            return Math.Min(Math.Max(x, 0.0f), 1.0f);
        }


    }
    public class Model
    {
        public struct Vec3
        {
            public float x;
            public float y;
            public float z;
        }

        public struct Vec2
        {
            public float x;
            public float y;
        }

        public struct IVec3
        {
            public int x;
            public int y;
            public int z;
        }

        public enum GBufferObjectType : byte
        {
            None,
            Hero,
            Npc,
            Terrain,
            Impostor,
            Misc1,
            Misc2,
        };

        public struct BSphere
        {
            public Vec3 m_Center;
            public float m_Radius;
        }
        public struct ModelBuilt
        {
            public UInt64 m_Flags;
            public UInt32 m_AvMaterialHash;
            public UInt32 m_AudioMaterialHash;

            public Int16 m_GeomLodBiasI16;
            public Int16 m_ShadowBiasI16;
            public Int16 m_ZBiasI16;
            public Int16 m_AlphaSortBiasI16;
            public UInt16 m_FadeOutDistU16;
            public UInt16 m_ShadowFadeDistU16;
            public GBufferObjectType m_GBufferObjectType;
            public byte m_GeomLodGenType;
            public Int16 m_AutoGeomLodBias;

            public BSphere m_BSphere;                      // The bsphere of the model in object space
            public Vec3 m_AABBExtents;                     // The extents of the model aabb in object space (using the bsphere as a center point)
            public float m_CommonMetersPerUnit;            // The scalar to convert the 16-bit fixed point coordinates to floating point (note: subsets may use higher precision than this in special cases like facial models or destruction models)
            public float m_VertexMetersPerUnit;            // Generally the same as m_CommonMetersPerUnit, but can be higher precision for special cases like facial models and destruction models (leveraging subset vertex offsets)
            public UInt32 m_CustomStreamCount;             // The number of unique vertices in the model, used for allocating a custom stream for a model instance
            public UInt16 m_ContentModelFlags;             // see ContentModel::ContentFlags (used during ContentModel::Serialise / DeSerialize)
            public UInt16 m_SubsetLodMaskCount;            // The count of bits set in all subset lod masks, which is the upper bound of how many subset ids could be in a model look
            public UInt16 m_LocatorCount;
            public sbyte m_StrandSubsetCount;
            public sbyte m_LGCaptureBehavior;

            public UInt32[] m_Pad;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct ModelSubset
        {
            public UInt32 m_IndexCount;
            public UInt32 m_VertexCount;
            public UInt32 m_GeometryDataOffset;
            public UInt32 m_IndexDataOffset;

            public UInt32 m_GpuRegistryId;
            public UInt16 m_Flags;
            public UInt16 m_UVLogScale;
            public float m_MetersPerUnit;
            public UInt16 m_MaterialIndex;
            public Int16 m_ZBiasI16;

            public float m_SurfaceArea;
            public float m_UVArea;
            public float m_FadeOutDist;
            public float m_MaterialLodDist;

            public IVec3 m_ObjSpaceCenter;
            public UInt32 m_ObjSpaceExtent;

            public UInt32 m_VertexStdOffset;
            public UInt32 m_VertexUV12Offset;
            public UInt32 m_VertexColorOffset;
            public UInt32 m_VertexSkinOffset;
            public UInt32 m_SkinClusterOffset;
            public UInt32 m_CustomStreamIndex;

            public UInt32 m_GeometryBuiltOffset;
            public UInt32 m_GeometryBuiltSize;

            public UInt32 m_AnimVertCount;
            public UInt16 m_AnimVertClusterCount;
            public UInt16 m_LodMask;
            public UInt32 m_LGCaptureBehavior;
            public float m_LodExtrusionScale;
            public UInt32 m_OriginJointNameHash;
            public byte m_SkipShadowBias;
            public byte m_PadA;
            public UInt16 m_LongestEdgeLength;
            public float m_CurvatureRadius;
            public UInt32 m_PadB;
        }

        public struct ModelStdVertex
        {
            public Int16 m_Position_x;
            public Int16 m_Position_y;
            public Int16 m_Position_z;
            public Int16 m_Position_w;
            public Int32 m_NormalTangent;
            public Int16 m_UV0_u;
            public Int16 m_UV0_v;
        }

        public struct RuntimeVertex
        {
            public float[] m_Position;
            public float[] m_Normal;
            public float[] m_UV0;
        }
    }

    public class DAT1
    {
        public UInt32 m_DataFileId;
        public UInt32 m_VersionNumber;
        public UInt32 m_FileSize;
        public UInt16 m_BlockCount;
        public UInt16 m_FixupCount;

        public struct DataBlockHeader
        {
            public UInt32 m_NameHash;
            public UInt32 m_Offset;
            public UInt32 m_Size;
        }

        public DAT1(BinaryReader br)
        {
            m_DataFileId = br.ReadUInt32();
            m_VersionNumber = br.ReadUInt32();
            m_FileSize = br.ReadUInt32();
            m_BlockCount = br.ReadUInt16();
            m_FixupCount = br.ReadUInt16();

            DataBlockHeader[] blockHeaders = new DataBlockHeader[m_BlockCount];
            for (int i = 0; i < m_BlockCount; i++)
            {
                blockHeaders[i].m_NameHash = br.ReadUInt32();
                blockHeaders[i].m_Offset = br.ReadUInt32();
                blockHeaders[i].m_Size = br.ReadUInt32();

                //Console.WriteLine(blockHeaders[i].m_NameHash);
            }

            //-----Model Built-----\\

            int modelbuilt_index = Utils.IndexOfBlock(blockHeaders, Hashes.ModelHashes, "Model Built");
            if (modelbuilt_index != -1)
            {
                ModelBuilt model_built = new ModelBuilt();
                br.BaseStream.Seek(blockHeaders[modelbuilt_index].m_Offset, SeekOrigin.Begin);

                model_built.m_Flags = br.ReadUInt64();
                model_built.m_AvMaterialHash = br.ReadUInt32();
                model_built.m_AudioMaterialHash = br.ReadUInt32();

                model_built.m_GeomLodBiasI16 = br.ReadInt16();
                model_built.m_ShadowBiasI16 = br.ReadInt16();
                model_built.m_ZBiasI16 = br.ReadInt16();
                model_built.m_AlphaSortBiasI16 = br.ReadInt16();
                model_built.m_FadeOutDistU16 = br.ReadUInt16();
                model_built.m_ShadowFadeDistU16 = br.ReadUInt16();
                model_built.m_GBufferObjectType = (GBufferObjectType)br.ReadByte();
                model_built.m_GeomLodGenType = br.ReadByte();
                model_built.m_AutoGeomLodBias = br.ReadInt16();

                model_built.m_BSphere = new BSphere()
                {
                    m_Center = new Vec3()
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle(),
                    },
                    m_Radius = br.ReadSingle(),
                };
                model_built.m_AABBExtents = new Vec3()
                {
                    x = br.ReadSingle(),
                    y = br.ReadSingle(),
                    z = br.ReadSingle(),
                };
                model_built.m_CommonMetersPerUnit = br.ReadSingle();
                model_built.m_VertexMetersPerUnit = br.ReadSingle();
                model_built.m_CustomStreamCount = br.ReadUInt32();
                model_built.m_ContentModelFlags = br.ReadUInt16();
                model_built.m_SubsetLodMaskCount = br.ReadUInt16();
                model_built.m_LocatorCount = br.ReadUInt16();
                model_built.m_StrandSubsetCount = br.ReadSByte();
                model_built.m_LGCaptureBehavior = br.ReadSByte();

                model_built.m_Pad = new uint[2];
                model_built.m_Pad[0] = br.ReadUInt32();
                model_built.m_Pad[1] = br.ReadUInt32();

                //-----Model Subset-----\\

                int modelsubset_index = Utils.IndexOfBlock(blockHeaders, Hashes.ModelHashes, "Model Subset");
                if (modelsubset_index != -1)
                {
                    ModelSubset[] model_subset = new ModelSubset[blockHeaders[modelsubset_index].m_Size / Marshal.SizeOf(typeof(ModelSubset))];
                    br.BaseStream.Seek(blockHeaders[modelsubset_index].m_Offset, SeekOrigin.Begin);
                    for (int i = 0; i < model_subset.Length; i++)
                    {
                        Console.WriteLine($"Subset {i + 1}");
                        model_subset[i].m_IndexCount = br.ReadUInt32();

                        model_subset[i].m_VertexCount = br.ReadUInt32();
                        model_subset[i].m_GeometryDataOffset = br.ReadUInt32();
                        model_subset[i].m_IndexDataOffset = br.ReadUInt32();

                        model_subset[i].m_GpuRegistryId = br.ReadUInt32();
                        model_subset[i].m_Flags = br.ReadUInt16();
                        model_subset[i].m_UVLogScale = br.ReadUInt16();
                        model_subset[i].m_MetersPerUnit = br.ReadSingle();
                        model_subset[i].m_MaterialIndex = br.ReadUInt16();
                        model_subset[i].m_ZBiasI16 = br.ReadInt16();

                        model_subset[i].m_SurfaceArea = br.ReadSingle();
                        model_subset[i].m_UVArea = br.ReadSingle();
                        model_subset[i].m_FadeOutDist = br.ReadSingle();
                        model_subset[i].m_MaterialLodDist = br.ReadSingle();

                        model_subset[i].m_ObjSpaceCenter = new IVec3()
                        {
                            x = br.ReadInt32(),
                            y = br.ReadInt32(),
                            z = br.ReadInt32(),
                        };
                        model_subset[i].m_ObjSpaceExtent = br.ReadUInt32();

                        model_subset[i].m_VertexStdOffset = br.ReadUInt32();
                        model_subset[i].m_VertexUV12Offset = br.ReadUInt32();
                        model_subset[i].m_VertexColorOffset = br.ReadUInt32();
                        model_subset[i].m_VertexSkinOffset = br.ReadUInt32();
                        model_subset[i].m_SkinClusterOffset = br.ReadUInt32();
                        model_subset[i].m_CustomStreamIndex = br.ReadUInt32();

                        model_subset[i].m_GeometryBuiltOffset = br.ReadUInt32();
                        model_subset[i].m_GeometryBuiltSize = br.ReadUInt32();

                        model_subset[i].m_AnimVertCount = br.ReadUInt32();
                        model_subset[i].m_AnimVertClusterCount = br.ReadUInt16();
                        model_subset[i].m_LodMask = br.ReadUInt16();
                        model_subset[i].m_LGCaptureBehavior = br.ReadUInt32();
                        model_subset[i].m_LodExtrusionScale = br.ReadSingle();
                        model_subset[i].m_OriginJointNameHash = br.ReadUInt32();
                        model_subset[i].m_SkipShadowBias = br.ReadByte();
                        model_subset[i].m_PadA = br.ReadByte();
                        model_subset[i].m_LongestEdgeLength = br.ReadUInt16();
                        model_subset[i].m_CurvatureRadius = br.ReadSingle();
                        model_subset[i].m_PadB = br.ReadUInt32();

                        if (model_subset[i].m_LodMask == 1)
                        {
                            //Write Subset
                        }
                    }

                    //-----Model Subset Geom Data-----\\

                    int modelsubsetgeomdata_index = Utils.IndexOfBlock(blockHeaders, Hashes.ModelHashes, "Model Subset Geom Data");
                    if (modelsubsetgeomdata_index != -1)
                    {
                        for (int s = 0; s < model_subset.Length; s++)
                        {
                            if (model_subset[s].m_LodMask != 1)
                            {
                                continue;
                            }
                            var dataOffset = blockHeaders[modelsubsetgeomdata_index].m_Offset + model_subset[s].m_GeometryBuiltOffset;

                            br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);

                            br.BaseStream.Seek(model_subset[s].m_GeometryDataOffset, SeekOrigin.Current);


                            br.BaseStream.Seek(dataOffset, SeekOrigin.Begin);

                            br.BaseStream.Seek(model_subset[s].m_IndexDataOffset, SeekOrigin.Current);
                            List<UInt16> Indices = new List<UInt16>();
                            for (int i = 0; i < model_subset[s].m_IndexCount; i++)
                            {
                                Indices.Add(br.ReadUInt16());
                                Console.WriteLine("Subset " + s + " Index " + i);
                            }
                            JsonConvert.SerializeObject(Indices);
                        }
                    }
                    else
                    {
                        throw new Exception("Model Subset Geom Data not found");
                    }
                }
                else
                {
                    throw new Exception("Model Subset not found");
                }
            }
            else
            {
                throw new Exception("Model Built not found");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DAT1 model = new DAT1(new BinaryReader(File.Open(@"C:\Users\27alexander.smith_ca\Desktop\Personal\DAT1\DAT1\GameFiles\hero_spiderman_advanced.model", FileMode.Open)));

            ModelBuilt modelBuilt = JsonConvert.DeserializeObject<ModelBuilt>(File.ReadAllText("model_blocks.json"));

            Console.WriteLine(modelBuilt.m_GBufferObjectType);



            //// create two materials

            //var material1 = new MaterialBuilder()
            //    .WithDoubleSide(true)
            //    .WithMetallicRoughnessShader()
            //    .WithChannelParam(KnownChannel.BaseColor, KnownProperty.RGBA, new Vector4(1, 0, 0, 1));

            //var material2 = new MaterialBuilder()
            //    .WithDoubleSide(true)
            //    .WithMetallicRoughnessShader()
            //    .WithChannelParam(KnownChannel.BaseColor, KnownProperty.RGBA, new Vector4(1, 0, 1, 1));

            //// create a mesh with two primitives, one for each material

            //var mesh = new MeshBuilder<VERTEX>("mesh");

            //var prim = mesh.UsePrimitive(material1);

            //prim = mesh.UsePrimitive(material2);
            //prim.AddQuadrangle(new VERTEX(-5, 0, 3), new VERTEX(0, -5, 3), new VERTEX(5, 0, 3), new VERTEX(0, 5, 3));

            //// create a scene

            //var scene = new SharpGLTF.Scenes.SceneBuilder();

            //scene.AddRigidMesh(mesh, Matrix4x4.Identity);

            //// save the model in different formats

            //var model = scene.ToGltf2();
            //model.SaveAsWavefront("mesh.obj");
            //model.SaveGLB("mesh.glb");
            //model.SaveGLTF("mesh.gltf");
        }
    }
}
