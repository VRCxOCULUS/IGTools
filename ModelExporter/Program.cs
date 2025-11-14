using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpGLTF.Geometry;
using SharpGLTF.Transforms;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Scenes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using static ModelExporter.Model;
using System.Runtime.InteropServices;
using System.Linq;
using SharpGLTF.Schema2;

namespace ModelExporter
{
    public static class GlobalJson
    {
        static readonly string _path = "Output/model_desc.json";

        public static void WriteObject<T>(string key, T obj)
        {
            JObject root;

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory("Output");
            }

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

        public static int IndexOfBlock(DataBlockHeader[] BlockHeaders, ModelHash BlockName)
        {
            for (int i = 0; i < BlockHeaders.Length; i++)
            {
                if (BlockHeaders[i].m_NameHash == BlockName)
                    return (int)BlockHeaders[i].m_Offset;
            }
            return -1;
        }

        public static DataBlockHeader GetBlockById(DataBlockHeader[] BlockHeaders, ModelHash BlockName)
        {
            for (int i = 0; i < BlockHeaders.Length; i++)
            {
                if (BlockHeaders[i].m_NameHash == BlockName)
                {
                    return BlockHeaders[i];
                }
            }
            throw new Exception("Block not found");
        }

        public static string ReadNullTermString(BinaryReader br)
        {
            bool end = false;
            List<char> chars = new List<char>();
            while (!end)
            {
                if (br.PeekChar() == '\0')
                {
                    end = true;
                }
                chars.Add(br.ReadChar());
            }
            return new string(chars.ToArray());
        }

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

        public static void DecodeNormalTangent(ModelStdVertex stdVertex, out Vector3 normal, out Vector3 tangent)
        {
            Vector4 normalTanEnc = VecFromR10G10B10A2_UNORM((uint)stdVertex.m_NormalTangent);
            int posWAbs = Math.Abs((int)stdVertex.m_Position_W);

            float nt_zs = normalTanEnc.W * 3.0f;
            float normalZ = Saturate(nt_zs - 1.0f);
            float tangentZ = nt_zs - (normalZ * 2.0f);

            // Normal decode
            normal = NormalDecodeAzimuthal(new Vector3(normalTanEnc.X, normalTanEnc.Y, normalZ));

            // Tangent decode (using low 10 bits from position.w)
            float tangentYEnc = (posWAbs & 0x3FF) * (1.0f / 1023.0f);
            tangent = NormalDecodeAzimuthal(new Vector3(normalTanEnc.Z, tangentYEnc, tangentZ));
        }

        public static Vector3 DecodeNormal(ModelStdVertex stdVertex)
        {
            // Decode the 10:10:10:2 packed normal/tangent
            Vector4 normalTanEnc = VecFromR10G10B10A2_UNORM((uint)stdVertex.m_NormalTangent);

            // Compute Z using the official formula
            float nt_zs = normalTanEnc.W * 3.0f;
            float normalZ = Saturate(nt_zs - 1.0f);

            // Decode azimuthal normal
            return NormalDecodeAzimuthal(new Vector3(normalTanEnc.X, normalTanEnc.Y, normalZ));
        }


        public static Vector3 NormalDecodeAzimuthal(Vector3 azim)
        {
            // scale and bias from [0,1] → [-√2, √2]
            float scale = 4.0f / 1.41421356f; // 4 / sqrt(2)
            float bias = 2.0f / 1.41421356f;

            float ex = azim.X * scale - bias;
            float ey = azim.Y * scale - bias;

            float f = ex * ex + ey * ey;

            float s = (float)Math.Sqrt(Math.Max(0.0, 1.0 - f * 0.25));

            Vector3 vec = new Vector3(
                ex * s,
                ey * s,
                (float)Math.Abs(1.0 - f * 0.5)
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
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ModelSkinCluster
    {
        private uint _flags; // Make the backing field 'readonly' if desired for absolute immutability

        // Public constructor to set the initial raw 32-bit value upon creation
        public ModelSkinCluster(uint rawValue)
        {
            _flags = rawValue;
        }

        // Public read-only property for the raw flags value (optional)
        public uint RawFlags => _flags;

        // All the bit-specific properties only need 'get' accessors:

        // 20 bits (0-19)
        public uint m_SkinDataOffset4 => _flags & 0x000FFFFFU;

        // 1 bit (bit 20)
        public uint m_16BitIndices => (_flags >> 20) & 0x1U;

        // 4 bits (bits 21-24)
        public uint m_InfluenceCountMinus1 => (_flags >> 21) & 0xFU;

        // 4 bits (bits 25-28)
        public uint m_JointOffset256 => (_flags >> 25) & 0xFU;

        // 1 bit (bit 29)
        public uint m_IsAnimVert => (_flags >> 29) & 0x1U;

        // 2 bits (bits 30-31)
        public uint m_Unused => (_flags >> 30) & 0x3U;
    }


    enum SubsetDataSizes
        {
            // The maximum number of joint influences
            kModelSkinJointInfluenceMax = 12,

            // This many consecutive verts use the same number of joint influences.
            kModelSkinVertClusterSize = 32,

            // The maximum number of morph subsets / targets
            kModelMorphSubsetMax = 255,
            kModelMorphTargetMax = 2048,

            // The maximum number of verts in an anim vert batch
            kModelAnimVertBatchMax = 2560,
        };

        public struct ModelStdVertex
        {
            public Int16 m_Position_X;
            public Int16 m_Position_Y;
            public Int16 m_Position_Z;
            public Int16 m_Position_W;
            public Int32 m_NormalTangent;
            public Int16 m_UV0_U;
            public Int16 m_UV0_V;
        }

        public struct Index
        {
            public UInt16 m_VertexA;
            public UInt16 m_VertexB;
            public UInt16 m_VertexC;
        }

        public enum ModelHash : UInt32
        {
            kDaeFileNameMemberHash = 0xca6eaa13, // buildHash32("m_DaeFileName")

            kModelBuiltHash = 0x283d0383, // buildHash32("Model Built")
            kCategoryModelAnimVertMorph2Hash = 0x12add55b, // buildHash32("Model-AnimVertMorph2")
            kModelSubsetHash = 0x78d9cbde, // buildHash32("Model Subset")
            kModelSubsetGeomDataHash = 0xf4ea9e18, // buildHash32("Model Subset Geom Data")
            kModelSubsetSkinDataHash = 0xf508d8c3, // buildHash32("Model Subset Skin Data")
            kModelLookHash = 0x06eb7efc, // buildHash32("Model Look")
            kModelLookGroupHash = 0x4ccea4ad, // buildHash32("Model Look Group")
            kModelLookBuiltHash = 0x811902d7, // buildHash32("Model Look Built")
            kModelLookBVHDataHash = 0xb9e51d26, // buildHash32("Model Look BVH Data")
            kModelLookBVHInfoHash = 0xdf9fdf12, // buildHash32("Model Look BVH Info")
            kModelLookBVHAdditionalLodInfoHash = 0xaf739ebf, // buildHash32("Model Look BVH Lod Info")
            kModelMaterialHash = 0x3250bb80, // buildHash32("Model Material")
            kModelJointHierarchyHash = 0x90cdb60c, // buildHash32("Model Joint Hierarchy")
            kModelJointHash = 0x15df9d3b, // buildHash32("Model Joint")
            kModelMirrorIdsHash = 0xc5354b60, // buildHash32("Model Mirror Ids")
            kModelLeafIdsHash = 0xb7380e8c, // buildHash32("Model Leaf Ids")
            kModelSplineRadiiHash = 0x7496d749, // buildHash32("Model Spline Radii")
            kModelJointBspheresHash = 0x0ad3a708, // buildHash32("Model Joint Bspheres")
            kModelJointLookupHash = 0xee31971c, // buildHash32("Model Joint Lookup")
            kModelBindPoseHash = 0xdcc88a19, // buildHash32("Model Bind Pose")
            kModelInvBindPoseHash = 0x2c2ca205, // buildHash32("Model Inv Bind Pose")
            kModelLocatorHash = 0x9f614fab, // buildHash32("Model Locator")
            kModelLocatorLookupHash = 0x731cbc2e, // buildHash32("Model Locator Lookup")
            kModelCollisionIndexDataHash = 0x5796fef6, // buildHash32("Model Collision Index Data")
            kModelCollisionVertexDataHash = 0xf4cb2f37, // buildHash32("Model Collision Vertex Data")
            kModelCollisionComplexityHash = 0xd2b0cb7d, // buildHash32("Model Collision Complexity")
            kModelHavokDataHash = 0xefd92e68, // buildHash32("Model Physics Data")
            kModelRagDollMetaDataHash = 0x707f1b58, // buildHash32("Model's Ragdoll meta data")
            kModelDestructibleMetaDataHash = 0x1112df22, // buildHash32("Model's Destructible meta data")
            kModelClothMetaDataHash = 0x5a39fab7, // buildHash32("Model's Cloth meta data")
            kModelIKSetupDataHash = 0x9a434b29, // buildHash32("Model's ik setup data")

            kModelAnimVertInfo2Hash = 0x06e24ed4, // buildHash32("Model Anim Vert Info2")

            kModelAnimMorph2InfoHash = 0x948c38cd, // buildHash32("Model Anim Morph2 Info")
            kModelAnimMorph2BatchesHash = 0x5e03cefe, // buildHash32("Model Anim Morph2 Batches")
            kModelAnimMorph2ValidMasksHash = 0xde8d9dcf, // buildHash32("Model Anim Morph2 Valid Masks")
            kModelAnimMorph2DeltasHash = 0x573c714b, // buildHash32("Model Anim Morph2 Deltas")

            kModelAnimGeomInfoHash = 0xcd903318, // buildHash32("Model Anim Geom Info")
            kModelAnimGeomParticlesHash = 0x3f70f60d, // buildHash32("Model Anim Geom Particles")
            kModelAnimGeomMeshInfoHash = 0x92123a05, // buildHash32("Model Anim Geom Mesh Info")

            kModelAnimZiva2InfoHash = 0x6694064e, // buildHash32("Model Anim Ziva2 Info")

            kModelAnimVertNormalInfoHash = 0x855275d7, // buildHash32("Model Anim Vert Normal Info")
            kModelAnimVertNormalStitchesHash = 0x8a84e4d6, // buildHash32("Model Anim Vert Normal Stitches")

            kModelAnimVertSmoothInfoHash = 0x55187f02, // buildHash32("Model Anim Vert Smooth Info")

            kModelBVolHash = 0x53ea4273, // buildHash32("Model BVol")
            kModelVGroupHash = 0xfb7f6a48, // buildHash32("Model VGroup")
            kModelMeshNamesHash = 0xb32d7671, // buildHash32("Model Mesh Names")
            kModelContentAnimVertInfosHash = 0x14a9959c, // buildHash32("Model Content Anim Vert Infos")
            kModelTextureOverridesHash = 0x00823787, // buildHash32("Model Texture Overrides")
            kModelRenderOverridesHash = 0xbce86b01, // buildHash32("Model Render Overrides")
            kModelAmbientShadowPrimsHash = 0x7ca37da0, // buildHash32("Ambient Shadow Prims")
            kModelAnimDynamicsDefHash = 0xadd1cbd3, // buildHash32("Model Anim Dynamics Def")
            kModelStrandSubsetsHash = 0x3c9dabdf, // buildHash32("Model Spline Subsets")
            kModelStrandsHash = 0x27ca5246, // buildHash32("Model Splines")
            kModelStrandCVsHash = 0xb25b3163, // buildHash32("Model Splines CVs")
            kModelStrandSBsHash = 0xbb7303d5, // buildHash32("Model Spline Skin Binding")
            kModelStrandJBsHash = 0x14d8b13c, // buildHash32("Model Spline Joint Binding")
            kModelStrandCVWeightsHash = 0x5d5cf541, // buildHash32("Model Spline Joint Weights")
            kModelRayTracingParametersHash = 0x0ba45069, // buildHash32("Model Ray-Tracing Parameters")
            kModelRayTracingAdditionalParametersHash = 0x4f26e619, // buildHash32("Model Ray-Tracing Additional Parameters")
            kModelDefaultLookHash = 0xe2b463e7, // buildHash32("Default")
            kModelDefaultLookLowerHash = 0x2d095a7b, // buildHash32("default")


            // Data file categories
            kCategoryModelMeshHash = 0x342e5419, // buildHash32("Model-Mesh")
            kCategoryModelBVHHash = 0x0eb12e28, // buildHash32("Model-BVH")
            kCategoryModelStrandsHash = 0x242135cc, // buildHash32("Model-Strands")
            kCategoryModelStructsHash = 0xb9407406, // buildHash32("Model-Structs")
            kCategoryModelSkinHash = 0xd7380651, // buildHash32("Model-Skin")
            kCategoryModelDebugMeshHash = 0x392f2456, // buildHash32("Model-DebugMesh")
            kCategoryModelSkeletonHash = 0x62798068, // buildHash32("Model-Skeleton")
            kCategoryModelLooksHash = 0xc19f6611, // buildHash32("Model-Looks")
            kCategoryModelAnimMorphDeltasHash = 0x9753cc6b, // buildHash32("Model-AnimMorphDeltas")
            kCategoryModelAnimMorphIndicesHash = 0x95f4ffa3, // buildHash32("Model-AnimMorphIndices")
            kCategoryModelAnimVertNormalsHash = 0x8dd5dba5, // buildHash32("Model-AnimVertNormals")

            kCategoryModelAnimVertHash = 0xb9097b3c, // buildHash32("Model-AnimVert")
            kCategoryModelAnimGeomHash = 0xcaab8f1a, // buildHash32("Model-AnimGeom")
            kCategoryModelAnimZivaHash = 0xf3741c0f, // buildHash32("Model-AnimZiva")
            kCategoryModelAnimVertSmoothHash = 0x89716b68, // buildHash32("Model-AnimVertSmooth")
            kCategoryModelDestructionHash = 0x2106db30, // buildHash32("Model-Destruction")
            kCategoryModelPhysicsHash = 0xa613c3f6, // buildHash32("Model-Physics")
            kCategoryModelClothHash = 0x1645995c, // buildHash32("Model-Cloth")


            // Model Lod Serialization
            kModelLodInfoBuiltHash = 0x36ac9e04, // buildHash32("Model Lod Info Built")
            kModelLodPoolBuiltHash = 0xd4cb21d3, // buildHash32("Model Lod Pool Built")
            kModelLodGeometryHash = 0x4a71612b, // buildHash32("Model Lod Geometry")
            kModelLodMaterialNamesHash = 0x1f235224, // buildHash32("Model Lod Material Names")
            kModelLodBoneNamesHash = 0xfc869eb4, // buildHash32("Model Lod Bone Names")
            kModelLodJointNamesHash = 0x789c01db, // buildHash32("Model Lod Joint Names")
            kModelLodAnimVertKeyHash = 0x55f83681 // buildHash32("Model Lod Anim Vert Key")
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

        public struct JointHierarchy
        {
            public UInt16 m_Flags;
            public UInt16 m_JointCount;
            public UInt16 m_MirrorCount;
            public UInt16 m_LeafCount;

            public AnimJoint[] m_Joints;
            public HashLookupElem[] m_JointLookup;
            public AnimJointPose[] m_JointLocalBindPoses;
            public Mat4[] m_JointInvBindMats;
            public JointBSpheres m_JointBSpheres;
            public JointMirrorId[] m_MirrorIds;
            public UInt16[] m_LeafIds;

            public UInt64 m_AnimSplineMeshRadius;

            public UInt64 m_JointDataHash;
        }

        public struct JointHierarchyBuilt
        {
            public UInt16 m_Flags;
            public UInt16 m_JointCount;
            public UInt16 m_MirrorCount;
            public UInt16 m_LeafCount;


            public byte[] m_Pad1; // 8

            public UInt64 m_JointDataHash;

            public byte[] m_Pad; // 56
        };

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

        public struct Vec3
        {
            public float x;
            public float y;
            public float z;
        }

        public struct Vec4
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }

        public struct Mat4
        {
            public Vec3 x;
            public Vec3 y;
            public Vec3 z;
            public Vec3 w;
        }

        public struct Quat
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }

        public struct Vec2
        {
            public float x;
            public float y;
        }

        public struct IVec3
        {
            public UInt32 x;
            public UInt32 y;
            public UInt32 z;
        }
        public struct BSphere
        {
            public Vec3 m_Center;
            public float m_Radius;
        }

        public enum AnimJointFlags : UInt16
        {
            kSegmentScaleCompensate = 0x0001,
            kMirrorPairedLeft = 0x0002,
            kMirrorPairedRight = 0x0004,
            kEndJoint = 0x0008,
            kClothJoint = 0x0010,
            kHasNextSibling = 0x0020,         // indicates next sibling = current_joint_index + m_TreeJointCount
            kAnimChunk = 0x0040,         // Indicates if this joint is auto created from igAnimChunk tagging inside Geoemtry tree in maya
            kIsSkinned = 0x0080,         // Indicates the joint is referenced in skinning data
        }

        public struct AnimJoint
        {
            public Int16 m_ParentIndex;
            public UInt16 m_JointIndex;
            public UInt16 m_SubTreeJointCount;
            public AnimJointFlags m_Flags;
            public UInt32 m_NameHash;
            public UInt32 m_NameOffset;
        }

        public struct HashLookupElem
        {
            public UInt32 m_Hash;
            public UInt32 m_Value;
        }

        public struct AnimJointPose
        {
            public Vec4 m_s;              // sx sy sz --
            public Quat m_q;              // qx qy qz qw
            public Vec4 m_t;              // tx ty tz --
        }

        public struct JointBSphereElem
        {
            public BSphere m_BSphere;
            public UInt16 m_Id;
            public UInt16 m_Pad;
        }

        public struct JointBSpheres
        {
            public UInt32 m_BSphereCount;
            public UInt32[] m_Pad; // 3
            public JointBSphereElem[] m_BSpheres; // m_BSphereCount
        }

        public struct JointMirrorId
        {
            public UInt16 m_IdA;
            public UInt16 m_IdB;
        }

        public struct ModelLookLod
        {
            public UInt16 m_SubsetIdStart;                       // The index of the first subset in this lod, in the Look's m_SubsetIds array
            public UInt16 m_SubsetCount;                         // The number of subsets in this lod
        };

        public struct ModelLook
        {
            public ModelLookLod[] m_Lods;  //kModelGeomLodMax | 8
            public UInt32 m_LodCount;
            public UInt32 m_MaxSubsetSkinCount;
            public UInt64 m_SubsetIdsPointer; // UInt16
            public UInt64 m_RemapPointer; // ModelLookRemap
            public UInt64 m_Pad;
        }

        public struct ModelMaterialInfo
        {
            public UInt32 m_AssetNameOffset;
            public UInt32 m_Pad1;
            public UInt32 m_MaterialMappingNameOffset;
            public UInt32 m_Pad2;
        }

        public struct ModelMaterial
        {
            public UInt64 m_MaterialId;
            public UInt32 m_MaterialMappingNameHash;
            public UInt32 m_Flags;
        }

        public struct AnimVertInfo2
        {
            public UInt64 m_MorphInfoPointer;   // AnimVertMorph2Info
            public UInt64 m_GeomInfoPointer;          // AnimGeomInfo
            public UInt64 m_GeomMeshInfoPointer;      // AnimGeomInfo
            public UInt64 m_ZivaInfoPointer;     // AnimVertZivaInfo2
            public UInt64 m_SmoothInfoPointer; // AnimVertSmoothInfo2
        };

        public struct AnimRenderBuffer
        {
            public UInt32 m_Format;//      : 8;      // DXGI_FORMAT
            public UInt32 m_ElemSize;//    : 24;

            public UInt32 m_ElemCount;//   : 31;
            public UInt32 m_Initialized;// : 1;

            public UInt64 m_DataPointer;
            public UInt64 m_SRVPointer;
            public UInt64 m_UAVPointer;
            public UInt64 m_OwnerNamePointer;
        }

        public struct AnimZiva2Info
        {
            public byte m_Flags;
            public byte m_LodLevel;
            public byte m_ElemCount;
            public byte m_Pad;

            public UInt16 m_SliderCount;
            public UInt16 m_SliderMorphCount;
            public UInt16 m_JointCountTotal;                  // total joint count

            public byte m_GpuStageCount;
            public byte m_GpuBufferCount;

            public float m_LodBlendOutFactor;

            public UInt32 m_VertCountMax;                     // across all elems
            public UInt32 m_VertLookupCount;

            public UInt64 m_Elems;              //AnimVertZivaElem2
            public UInt64 m_JointLookup;           //HashLookupElem           | map from ig joint  index to ziva joint index
            public UInt64 m_Sliders;      //AnimVertZivaSliderInfo2  | sliders followed by morph sliders (terminated by 0xffffffff)
            public UInt64 m_SliderLookup;   //AnimVertZivaSliderId2    | map sliders (global) to elem (ziva) sliders
            public UInt64 m_SubsetElems;  //AnimVertZivaSubsetElem2  | one per subset
            public UInt64 m_VertLookup;                  //uint32_t                 | map from ig vertex index to ziva vertex index

            public UInt64 m_ZivaBuffersGpu;  //AnimRenderBuffer

            public UInt64 m_GpuStages;  //AnimVertZivaGpuStage

            public AnimRenderBuffer m_VertLookupBufferGpu;
        }



        public UInt32 m_DataFileId;
        public UInt32 m_VersionNumber;
        public UInt32 m_FileSize;
        public UInt16 m_BlockCount;
        public UInt16 m_FixupCount;

        public struct DataBlockHeader
        {
            public ModelHash m_NameHash;
            public UInt32 m_Offset;
            public UInt32 m_Size;
        }

        public Model(FileStream Asset)
        {
            BinaryReader br = new BinaryReader(Asset);
            string m_ModelName = Path.GetFileNameWithoutExtension(Asset.Name);

            m_DataFileId = br.ReadUInt32();
            m_VersionNumber = br.ReadUInt32();
            m_FileSize = br.ReadUInt32();
            m_BlockCount = br.ReadUInt16();
            m_FixupCount = br.ReadUInt16();

            DataBlockHeader[] blockHeaders = new DataBlockHeader[m_BlockCount];

            for (int i = 0; i < m_BlockCount; i++)
            {
                blockHeaders[i].m_NameHash = (ModelHash)br.ReadUInt32();
                blockHeaders[i].m_Offset = br.ReadUInt32();
                blockHeaders[i].m_Size = br.ReadUInt32();
            }


            int BuiltOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelBuiltHash);
            if (BuiltOffset == -1)
                throw new Exception("Model Built block not found.");

            br.BaseStream.Seek(BuiltOffset, SeekOrigin.Begin);
            ModelBuilt model_built = new ModelBuilt()
            {
                m_Flags = br.ReadUInt64(),
                m_AvMaterialHash = br.ReadUInt32(),
                m_AudioMaterialHash = br.ReadUInt32(),

                m_GeomLodBiasI16 = br.ReadInt16(),
                m_ShadowBiasI16 = br.ReadInt16(),
                m_ZBiasI16 = br.ReadInt16(),
                m_AlphaSortBiasI16 = br.ReadInt16(),
                m_FadeOutDistU16 = br.ReadUInt16(),
                m_ShadowFadeDistU16 = br.ReadUInt16(),
                m_GBufferObjectType = (GBufferObjectType)br.ReadByte(),
                m_GeomLodGenType = br.ReadByte(),
                m_AutoGeomLodBias = br.ReadInt16(),

                m_BSphere = new BSphere
                {
                    m_Center = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                    m_Radius = br.ReadSingle()
                },
                m_AABBExtents = new Vec3
                {
                    x = br.ReadSingle(),
                    y = br.ReadSingle(),
                    z = br.ReadSingle()
                },
                m_CommonMetersPerUnit = br.ReadSingle(),
                m_VertexMetersPerUnit = br.ReadSingle(),
                m_CustomStreamCount = br.ReadUInt32(),
                m_ContentModelFlags = br.ReadUInt16(),
                m_SubsetLodMaskCount = br.ReadUInt16(),
                m_LocatorCount = br.ReadUInt16(),
                m_StrandSubsetCount = br.ReadSByte(),
                m_LGCaptureBehavior = br.ReadSByte(),

                m_Pad = new uint[4]
                {
                        br.ReadUInt32(),
                        br.ReadUInt32(),
                        br.ReadUInt32(),
                        br.ReadUInt32()
                }
            };


            int JointHierarchyOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelJointHierarchyHash);
            if (JointHierarchyOffset == -1)
                throw new Exception("Model Joint Hierarchy block not found.");

            int JointBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelJointHash);
            int JointLookupBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelJointLookupHash);
            int BindPoseBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelBindPoseHash);
            int InvBindPoseBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelInvBindPoseHash);
            int JointBspheresBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelJointBspheresHash);
            int MirrorIdsBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelMirrorIdsHash);
            int LeafIdsBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelLeafIdsHash);
            int SplineRadiiBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelSplineRadiiHash);

            if (BindPoseBlockOffset == -1 || JointLookupBlockOffset == -1)
                throw new Exception("Essential Joint Hierarchy Blocks not found.");

            br.BaseStream.Seek(JointHierarchyOffset, SeekOrigin.Begin);

            JointHierarchyBuilt joint_hierarchy_built = new JointHierarchyBuilt
            {
                m_Flags = br.ReadUInt16(),
                m_JointCount = br.ReadUInt16(),
                m_MirrorCount = br.ReadUInt16(),
                m_LeafCount = br.ReadUInt16(),
                m_Pad1 = br.ReadBytes(8),
                m_JointDataHash = br.ReadUInt64(),
                m_Pad = br.ReadBytes(56)
            };

            JointHierarchy joint_hierarchy = new JointHierarchy
            {
                m_Flags = joint_hierarchy_built.m_Flags,
                m_JointCount = joint_hierarchy_built.m_JointCount,
                m_MirrorCount = joint_hierarchy_built.m_MirrorCount,
                m_LeafCount = joint_hierarchy_built.m_LeafCount,

                m_AnimSplineMeshRadius = (UInt64)(SplineRadiiBlockOffset == -1 ? 0 : SplineRadiiBlockOffset),

                m_JointDataHash = joint_hierarchy_built.m_JointDataHash
            };

            AnimJoint[] m_Joints = new AnimJoint[joint_hierarchy.m_JointCount];
            br.BaseStream.Seek(JointBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                m_Joints[i].m_ParentIndex = br.ReadInt16();
                m_Joints[i].m_JointIndex = br.ReadUInt16();
                m_Joints[i].m_SubTreeJointCount = br.ReadUInt16();
                m_Joints[i].m_Flags = (AnimJointFlags)br.ReadUInt16();
                m_Joints[i].m_NameHash = br.ReadUInt32();
                m_Joints[i].m_NameOffset = br.ReadUInt32();
            }

            joint_hierarchy.m_Joints = m_Joints;

            List<string> boneNames = new List<string>();
            for (int i = 0; i < m_Joints.Length; i++)
            {
                br.BaseStream.Seek(JointBlockOffset + (16 * i) + m_Joints[i].m_NameOffset, SeekOrigin.Begin);
                boneNames.Add(Utils.ReadNullTermString(br));
            }
            HashLookupElem[] JointLookup = new HashLookupElem[joint_hierarchy.m_JointCount];
            br.BaseStream.Seek(JointLookupBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                JointLookup[i].m_Hash = br.ReadUInt32();
                JointLookup[i].m_Value = br.ReadUInt32();
            }
            joint_hierarchy.m_JointLookup = JointLookup;


            AnimJointPose[] BindPose = new AnimJointPose[joint_hierarchy.m_JointCount];
            br.BaseStream.Seek(BindPoseBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                BindPose[i].m_s = new Vec4
                {
                    x = br.ReadSingle(),
                    y = br.ReadSingle(),
                    z = br.ReadSingle(),
                    w = br.ReadSingle(),
                };
                BindPose[i].m_q = new Quat
                {
                    x = br.ReadSingle(),
                    y = br.ReadSingle(),
                    z = br.ReadSingle(),
                    w = br.ReadSingle(),
                };
                BindPose[i].m_t = new Vec4
                {
                    x = br.ReadSingle(),
                    y = br.ReadSingle(),
                    z = br.ReadSingle(),
                    w = br.ReadSingle(),
                };
            }
            joint_hierarchy.m_JointLocalBindPoses = BindPose;

            Mat4[] InvBindPose = new Mat4[joint_hierarchy.m_JointCount];
            br.BaseStream.Seek(InvBindPoseBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                InvBindPose[i] = new Mat4
                {
                    x = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                    y = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                    z = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                    w = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                };
            }
            joint_hierarchy.m_JointInvBindMats = InvBindPose;


            br.BaseStream.Seek(JointBspheresBlockOffset, SeekOrigin.Begin);
            JointBSpheres jointBSpheres = new JointBSpheres();
            jointBSpheres.m_BSphereCount = br.ReadUInt32();
            jointBSpheres.m_Pad = new UInt32[3]
            {
                    br.ReadUInt32(),
                    br.ReadUInt32(),
                    br.ReadUInt32()
            };
            jointBSpheres.m_BSpheres = new JointBSphereElem[jointBSpheres.m_BSphereCount];
            for (int i = 0; i < jointBSpheres.m_BSphereCount; i++)
            {
                jointBSpheres.m_BSpheres[i].m_BSphere = new BSphere
                {
                    m_Center = new Vec3
                    {
                        x = br.ReadSingle(),
                        y = br.ReadSingle(),
                        z = br.ReadSingle()
                    },
                    m_Radius = br.ReadSingle()
                };
                jointBSpheres.m_BSpheres[i].m_Id = br.ReadUInt16();
                jointBSpheres.m_BSpheres[i].m_Pad = br.ReadUInt16();
            }
            joint_hierarchy.m_JointBSpheres = jointBSpheres;

            JointMirrorId[] jointMirrorId = new JointMirrorId[joint_hierarchy.m_MirrorCount];
            br.BaseStream.Seek(MirrorIdsBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_MirrorCount; i++)
            {
                jointMirrorId[i].m_IdA = br.ReadUInt16();
                jointMirrorId[i].m_IdB = br.ReadUInt16();
            }

            joint_hierarchy.m_MirrorIds = jointMirrorId;

            UInt16[] jointLeafIds = new UInt16[joint_hierarchy.m_LeafCount];
            br.BaseStream.Seek(LeafIdsBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < joint_hierarchy.m_LeafCount; i++)
            {
                jointLeafIds[i] = br.ReadUInt16();
            }

            joint_hierarchy.m_LeafIds = jointLeafIds;

            int SubsetBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelSubsetHash);
            if (SubsetBlockOffset == -1)
                throw new Exception("Model Subset block not found.");

            int m_SubsetCount = (int)(Utils.GetBlockById(blockHeaders, ModelHash.kModelSubsetHash).m_Size / 128);
            ModelSubset[] m_Subsets = new ModelSubset[m_SubsetCount];
            br.BaseStream.Seek(SubsetBlockOffset, SeekOrigin.Begin);
            for (int i = 0; i < m_SubsetCount; i++)
            {
                m_Subsets[i].m_IndexCount = br.ReadUInt32();
                m_Subsets[i].m_VertexCount = br.ReadUInt32();
                m_Subsets[i].m_GeometryDataOffset = br.ReadUInt32();
                m_Subsets[i].m_IndexDataOffset = br.ReadUInt32();

                m_Subsets[i].m_GpuRegistryId = br.ReadUInt32();
                m_Subsets[i].m_Flags = br.ReadUInt16();
                m_Subsets[i].m_UVLogScale = br.ReadUInt16();
                m_Subsets[i].m_MetersPerUnit = br.ReadSingle();
                m_Subsets[i].m_MaterialIndex = br.ReadUInt16();
                m_Subsets[i].m_ZBiasI16 = br.ReadInt16();

                m_Subsets[i].m_SurfaceArea = br.ReadSingle();
                m_Subsets[i].m_UVArea = br.ReadSingle();
                m_Subsets[i].m_FadeOutDist = br.ReadSingle();
                m_Subsets[i].m_MaterialLodDist = br.ReadSingle();

                m_Subsets[i].m_ObjSpaceCenter = new IVec3
                {
                    x = br.ReadUInt32(),
                    y = br.ReadUInt32(),
                    z = br.ReadUInt32()
                };
                m_Subsets[i].m_ObjSpaceExtent = br.ReadUInt32();

                m_Subsets[i].m_VertexStdOffset = br.ReadUInt32();
                m_Subsets[i].m_VertexUV12Offset = br.ReadUInt32();
                m_Subsets[i].m_VertexColorOffset = br.ReadUInt32();
                m_Subsets[i].m_VertexSkinOffset = br.ReadUInt32();
                m_Subsets[i].m_SkinClusterOffset = br.ReadUInt32();
                m_Subsets[i].m_CustomStreamIndex = br.ReadUInt32();

                m_Subsets[i].m_GeometryBuiltOffset = br.ReadUInt32();
                m_Subsets[i].m_GeometryBuiltSize = br.ReadUInt32();

                m_Subsets[i].m_AnimVertCount = br.ReadUInt32();
                m_Subsets[i].m_AnimVertClusterCount = br.ReadUInt16();
                m_Subsets[i].m_LodMask = br.ReadUInt16();
                m_Subsets[i].m_LGCaptureBehavior = br.ReadUInt32();
                m_Subsets[i].m_LodExtrusionScale = br.ReadSingle();
                m_Subsets[i].m_OriginJointNameHash = br.ReadUInt32();
                m_Subsets[i].m_SkipShadowBias = br.ReadByte();
                m_Subsets[i].m_PadA = br.ReadByte();
                m_Subsets[i].m_LongestEdgeLength = br.ReadUInt16();
                m_Subsets[i].m_CurvatureRadius = br.ReadSingle();
                m_Subsets[i].m_PadB = br.ReadUInt32();
            }


            //Materials
            int MaterialBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelMaterialHash);
            if (MaterialBlockOffset == -1)
                throw new Exception("Model Material block not found.");

            br.BaseStream.Seek(MaterialBlockOffset, SeekOrigin.Begin);
            var m_MaterialCount = (Utils.GetBlockById(blockHeaders, ModelHash.kModelMaterialHash).m_Size / 32);
            ModelMaterialInfo[] MaterialInfoArray = new ModelMaterialInfo[m_MaterialCount];
            List<string> materialPaths = new List<string>();
            List<string> materialSlots = new List<string>();
            for (int i = 0; i < m_MaterialCount; i++)
            {
                MaterialInfoArray[i].m_AssetNameOffset = br.ReadUInt32();
                MaterialInfoArray[i].m_Pad1 = br.ReadUInt32();
                MaterialInfoArray[i].m_MaterialMappingNameOffset = br.ReadUInt32();
                MaterialInfoArray[i].m_Pad2 = br.ReadUInt32();
            }
            for (int i = 0; i < m_MaterialCount; i++)
            {
                br.BaseStream.Seek(MaterialInfoArray[i].m_AssetNameOffset, SeekOrigin.Begin);
                materialPaths.Add(Utils.ReadNullTermString(br));
            }
            for (int i = 0; i < m_MaterialCount; i++)
            {
                br.BaseStream.Seek(MaterialInfoArray[i].m_MaterialMappingNameOffset, SeekOrigin.Begin);
                materialSlots.Add(Utils.ReadNullTermString(br));
            }

            int AnimVertInfoBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelAnimVertInfo2Hash);
            if (AnimVertInfoBlockOffset == -1)
                throw new Exception("Models without skinning not supported.");

            br.BaseStream.Seek(AnimVertInfoBlockOffset, SeekOrigin.Begin);
            AnimVertInfo2 AnimVertInfo = new AnimVertInfo2
            {
                m_MorphInfoPointer = br.ReadUInt64(),
                m_GeomInfoPointer = br.ReadUInt64(),
                m_GeomMeshInfoPointer = br.ReadUInt64(),
                m_ZivaInfoPointer = br.ReadUInt64(),
                m_SmoothInfoPointer = br.ReadUInt64()
            };
            int AnimZiva2InfoBlockOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelAnimZiva2InfoHash);
            if (AnimZiva2InfoBlockOffset == -1)
                throw new Exception("Model Anim Ziva2 Info block not found.");

            AnimZiva2Info animZiva2Info = new AnimZiva2Info
            {
                m_Flags = br.ReadByte(),
                m_LodLevel = br.ReadByte(),
                m_ElemCount = br.ReadByte(),
                m_Pad = br.ReadByte(),
                m_SliderCount = br.ReadUInt16(),
                m_SliderMorphCount = br.ReadUInt16(),
                m_JointCountTotal = br.ReadUInt16(),
                m_GpuStageCount = br.ReadByte(),
                m_GpuBufferCount = br.ReadByte(),
                m_LodBlendOutFactor = br.ReadSingle(),
                m_VertCountMax = br.ReadUInt32(),
                m_VertLookupCount = br.ReadUInt32(),
                m_Elems = br.ReadUInt64(),
                m_JointLookup = br.ReadUInt64(),
                m_Sliders = br.ReadUInt64(),
                m_SliderLookup = br.ReadUInt64(),
                m_SubsetElems = br.ReadUInt64(),
                m_VertLookup = br.ReadUInt64(),
                m_ZivaBuffersGpu = br.ReadUInt64(),
                m_GpuStages = br.ReadUInt64(),
                m_VertLookupBufferGpu = new AnimRenderBuffer
                {
                    m_Format = br.ReadUInt32(),
                    m_ElemSize = br.ReadUInt32(),
                    m_ElemCount = br.ReadUInt32(),
                    m_Initialized = br.ReadUInt32(),
                    m_DataPointer = br.ReadUInt64(),
                    m_SRVPointer = br.ReadUInt64(),
                    m_UAVPointer = br.ReadUInt64(),
                    m_OwnerNamePointer = br.ReadUInt64()
                }
            };


            //Write to Mesh
            var scene = new SceneBuilder();

            
            NodeBuilder[] boneNodes = new NodeBuilder[joint_hierarchy.m_JointCount];

            // Create nodes
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                var pose = joint_hierarchy.m_JointLocalBindPoses[i];

                var translation = new Vector3(
                    pose.m_t.x,
                    pose.m_t.y,
                    pose.m_t.z
                ) * model_built.m_CommonMetersPerUnit;

                var rotation = new Quaternion(
                    pose.m_q.x,
                    pose.m_q.y,
                    pose.m_q.z,
                    pose.m_q.w
                );

                var scale = new Vector3(
                    pose.m_s.x,
                    pose.m_s.y,
                    pose.m_s.z
                );

                boneNodes[i] = new NodeBuilder(boneNames[i])
                    .WithLocalTranslation(translation)
                    .WithLocalRotation(rotation)
                    .WithLocalScale(scale);
            }

            // Link hierarchy (build tree)
            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                int parentIndex = joint_hierarchy.m_Joints[i].m_ParentIndex;

                if (parentIndex >= 0 && parentIndex < joint_hierarchy.m_JointCount)
                    boneNodes[parentIndex].AddNode(boneNodes[i]);
            }


            Matrix4x4[] invBindMatrices = new Matrix4x4[joint_hierarchy.m_JointCount];

            for (int i = 0; i < joint_hierarchy.m_JointCount; i++)
            {
                var inv = joint_hierarchy.m_JointInvBindMats[i];

                invBindMatrices[i] = new Matrix4x4(
                    inv.x.x, inv.x.y, inv.x.z, 0,
                    inv.y.x, inv.y.y, inv.y.z, 0,
                    inv.z.x, inv.z.y, inv.z.z, 0,
                    inv.w.x, inv.w.y, inv.w.z, 1
                );
            }

            
            var skinJoints = new (NodeBuilder Joint, Matrix4x4 InverseBindMatrix)[boneNodes.Length];

            for (int i = 0; i < boneNodes.Length; i++)
            {
                skinJoints[i] = (boneNodes[i], invBindMatrices[i]);
            }




            for (int isubset = 0; isubset < m_SubsetCount; isubset++)
            {
                if (m_Subsets[isubset].m_LodMask != 1)
                    continue;

                Console.WriteLine($"Subset {isubset}");

                var subset = m_Subsets[isubset];
                var vertices = new ModelStdVertex[subset.m_VertexCount];
                var indices = new Index[subset.m_IndexCount / 3];

                int subsetGeomOffset = Utils.IndexOfBlock(blockHeaders, ModelHash.kModelSubsetGeomDataHash);
                if (subsetGeomOffset == -1)
                    throw new Exception("Subset Geometry Data not found.");

                br.BaseStream.Seek(subsetGeomOffset + subset.m_GeometryBuiltOffset + subset.m_GeometryDataOffset + subset.m_VertexStdOffset, SeekOrigin.Begin);
                for (int v = 0; v < subset.m_VertexCount; v++)
                {
                    vertices[v].m_Position_X = br.ReadInt16();
                    vertices[v].m_Position_Y = br.ReadInt16();
                    vertices[v].m_Position_Z = br.ReadInt16();
                    vertices[v].m_Position_W = br.ReadInt16();
                    vertices[v].m_NormalTangent = br.ReadInt32();
                    vertices[v].m_UV0_U = br.ReadInt16();
                    vertices[v].m_UV0_V = br.ReadInt16();
                }

                br.BaseStream.Seek(subsetGeomOffset + subset.m_GeometryBuiltOffset + subset.m_IndexDataOffset, SeekOrigin.Begin);
                for (int i = 0; i < subset.m_IndexCount / 3; i++)
                {
                    indices[i].m_VertexA = br.ReadUInt16();
                    indices[i].m_VertexB = br.ReadUInt16();
                    indices[i].m_VertexC = br.ReadUInt16();
                }

                var material = new MaterialBuilder(materialSlots[m_Subsets[isubset].m_MaterialIndex]);
                var meshBuilder = new MeshBuilder<VertexPositionNormal, VertexTexture1>($"{isubset}_{m_ModelName}");
                var meshBuilderUV1 = new MeshBuilder<VertexPositionNormal, VertexTexture2>($"{isubset}_{m_ModelName}");
                var mesh = meshBuilder.UsePrimitive(material);
                var meshUV1 = meshBuilderUV1.UsePrimitive(material);

                int vertexCount = vertices.Length;
                Vector3[] normals = new Vector3[vertexCount];
                Vector2[] uv0 = new Vector2[vertexCount];
                Vector2[] uv1 = new Vector2[vertexCount];
                VertexTexture2[] UVs = new VertexTexture2[vertexCount];

                br.BaseStream.Seek(subsetGeomOffset + subset.m_GeometryBuiltOffset + subset.m_GeometryDataOffset + subset.m_VertexUV12Offset, SeekOrigin.Begin);
                for (int i = 0; i < vertexCount; i++)
                {
                    uv1[i] = new Vector2(br.ReadInt16() * (1 / 16384.0f), br.ReadInt16() * (1 / 16384.0f));
                }

                //Weights
                br.BaseStream.Seek(subsetGeomOffset + subset.m_GeometryBuiltOffset + subset.m_GeometryDataOffset + subset.m_SkinClusterOffset, SeekOrigin.Begin);
                var clusters = new List<ModelSkinCluster>();
                for (int i = 0; i < subset.m_AnimVertClusterCount; i++)
                {
                    clusters.Add(new ModelSkinCluster(br.ReadUInt32()));
                }

                var SkinDataOffset = subset.m_GeometryBuiltOffset + subset.m_GeometryDataOffset + subset.m_VertexSkinOffset;
                br.BaseStream.Seek(subsetGeomOffset + SkinDataOffset, SeekOrigin.Begin);
                byte[] subsetSkinData = br.ReadBytes((int)(Utils.GetBlockById(blockHeaders, ModelHash.kModelSubsetGeomDataHash).m_Size - SkinDataOffset));

                var jointIndicesPerVertex = new List<int[]>((int)subset.m_VertexCount);
                var jointWeightsPerVertex = new List<float[]>((int)subset.m_VertexCount);
                for (int i = 0; i < subset.m_VertexCount; i++)
                {
                    jointIndicesPerVertex.Add(new int[(int)SubsetDataSizes.kModelSkinJointInfluenceMax]);
                    jointWeightsPerVertex.Add(new float[(int)SubsetDataSizes.kModelSkinJointInfluenceMax]);
                }

                foreach (var cluster in clusters)
                {
                    bool is16 = (cluster.m_16BitIndices == 1) ? true : false;
                    int jointOffset = (int)cluster.m_JointOffset256 * 256;
                    int jointCount = (int)cluster.m_InfluenceCountMinus1 + 1;
                    int bytesPerVertex = jointCount * (is16 ? 2 : 1) + (jointCount == 1 ? 0 : 1);

                    int clusterVertexStart = clusters.IndexOf(cluster) * (int)SubsetDataSizes.kModelSkinVertClusterSize;
                    int clusterVertexCount = Math.Min((int)SubsetDataSizes.kModelSkinVertClusterSize, (int)subset.m_VertexCount - clusterVertexStart);

                    int clusterDataOffset = (int)(cluster.m_SkinDataOffset4 * 4);
                    int cursor = clusterDataOffset;

                    for (int i = 0; i < clusterVertexCount; i++)
                    {
                        int[] jointIndices = new int[(int)SubsetDataSizes.kModelSkinJointInfluenceMax];
                        float[] jointWeights = new float[(int)SubsetDataSizes.kModelSkinJointInfluenceMax];

                        int joint = jointOffset;
                        for (int j = 0; j < jointCount; j++)
                        {
                            float weight = 1.0f;
                            if (is16)
                            {
                                joint = subsetSkinData[cursor] | (subsetSkinData[cursor + 1] << 8);
                                cursor += 2;
                            }
                            else
                            {
                                joint += subsetSkinData[cursor++];
                            }

                            if (jointCount > 1)
                            {
                                weight = subsetSkinData[cursor++] * (1.0f / 255.0f);
                            }

                            jointIndices[j] = joint;
                            jointWeights[j] = weight;
                        }

                        float totalWeight = jointWeights.Take(jointCount).Sum();
                        if (totalWeight > 0)
                        {
                            for (int j = 0; j < jointCount; j++)
                                jointWeights[j] /= totalWeight;
                        }

                        jointIndicesPerVertex[clusterVertexStart + i] = jointIndices;
                        jointWeightsPerVertex[clusterVertexStart + i] = jointWeights;
                    }
                }


                for (int j = 0; j < vertexCount; j++)
                {
                    normals[j] = Utils.DecodeNormal(vertices[j]);
                    uv0[j] = new Vector2(vertices[j].m_UV0_U * (1 / 16384.0f), vertices[j].m_UV0_V * (1 / 16384.0f));
                    UVs[j] = new VertexTexture2(uv0[j], uv1[j]);
                }

                //var skinJoints = new (NodeBuilder Joint, Matrix4x4 InverseBindMatrix)[boneNodes.Length];

                //for (int i = 0; i < boneNodes.Length; i++)
                //{
                //    skinJoints[i] = (boneNodes[i], invBindMatrices[i]);
                //}

                VertexJoints4[] skinVerts = new VertexJoints4[subset.m_VertexCount];

                for (int v = 0; v < subset.m_VertexCount; v++)
                {
                    var joints = jointIndicesPerVertex[v];
                    var weights = jointWeightsPerVertex[v];

                    skinVerts[v] = new VertexJoints4((joints[0], weights[0]), (joints[1], weights[1]), (joints[2], weights[2]), (joints[3], weights[3]));
                }


                for (int i = 0; i < (subset.m_IndexCount / 3); i++)
                {
                    //Console.WriteLine($"Index {i}");
                    int ia = indices[i].m_VertexA;
                    int ib = indices[i].m_VertexB;
                    int ic = indices[i].m_VertexC;

                    VertexPositionNormal p0 = new VertexPositionNormal(new Vector3(vertices[ia].m_Position_X, vertices[ia].m_Position_Y, vertices[ia].m_Position_Z) * subset.m_MetersPerUnit, normals[ia]);
                    VertexPositionNormal p1 = new VertexPositionNormal(new Vector3(vertices[ib].m_Position_X, vertices[ib].m_Position_Y, vertices[ib].m_Position_Z) * subset.m_MetersPerUnit, normals[ib]);
                    VertexPositionNormal p2 = new VertexPositionNormal(new Vector3(vertices[ic].m_Position_X, vertices[ic].m_Position_Y, vertices[ic].m_Position_Z) * subset.m_MetersPerUnit, normals[ic]);

                    var vA = ia;
                    var vB = ib;
                    var vC = ic;

                    var jA = skinVerts[vA];
                    var jB = skinVerts[vB];
                    var jC = skinVerts[vC];

                    if (subset.m_VertexUV12Offset == 0)
                    {
                        VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4> vert1 = new VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4>(p0, uv0[ia], jA);
                        VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4> vert2 = new VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4>(p1, uv0[ib], jB);
                        VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4> vert3 = new VertexBuilder<VertexPositionNormal, VertexTexture1, VertexJoints4>(p2, uv0[ic], jC);
                        mesh.AddTriangle(vert1, vert2, vert3);
                    }
                    else
                    {
                        VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4> vert1 = new VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4>(p0, UVs[ia], jA);
                        VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4> vert2 = new VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4>(p1, UVs[ib], jB);
                        VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4> vert3 = new VertexBuilder<VertexPositionNormal, VertexTexture2, VertexJoints4>(p2, UVs[ic], jC);
                        meshUV1.AddTriangle(vert1, vert2, vert3);
                    }
                }

                if (subset.m_VertexUV12Offset == 0)
                {
                    scene.AddSkinnedMesh(meshBuilder, skinJoints);
                }
                else
                {
                    scene.AddSkinnedMesh(meshBuilderUV1, skinJoints);
                }
            }
            var model = scene.ToGltf2();

            if(!Directory.Exists("Exported"))
            {
                Directory.CreateDirectory("Exported");
            }
            model.SaveGLB($"Exported/{m_ModelName}.glb");
            Console.WriteLine($"Exported to \"Exported/{m_ModelName}.glb\"");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //if(args.Length < 1)
            //{
            //    Console.WriteLine("Usage: ModelExporter.exe <path to model asset>");
            //    return;
            //}
            //FileStream AssetPath = File.Open(args[0], FileMode.Open);

            FileStream AssetPath = File.Open(@"D:\hero_spiderman_advanced.model", FileMode.Open);

            new Model(AssetPath);
        }
    }
}