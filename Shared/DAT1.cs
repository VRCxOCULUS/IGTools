using System;
using System.Collections.Generic;
using System.IO;

namespace Shared
{
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

        public struct DataFileFixup
        {
            public UInt32 m_SourceOffset;
            public UInt32 m_TargetOffset;
        };

        public static int IndexOfBlock(DataBlockHeader[] BlockHeaders, UInt32 BlockName)
        {
            for (int i = 0; i < BlockHeaders.Length; i++)
            {
                if (BlockHeaders[i].m_NameHash == BlockName)
                    return (int)BlockHeaders[i].m_Offset;
            }
            return -1;
        }

        public static DataBlockHeader GetBlockById(DataBlockHeader[] BlockHeaders, UInt32 BlockName)
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
    }

    public static class Hashes
    {
        public enum ModelHash : UInt32
        {
            ModelBuilt = 0x283d0383, // Model Built
            CategoryModelAnimVertMorph2 = 0x12add55b, // Model-AnimVertMorph2
            ModelSubset = 0x78d9cbde, // Model Subset
            ModelSubsetGeomData = 0xf4ea9e18, // Model Subset Geom Data
            ModelSubsetSkinData = 0xf508d8c3, // Model Subset Skin Data
            ModelLook = 0x06eb7efc, // Model Look
            ModelLookGroup = 0x4ccea4ad, // Model Look Group
            ModelLookBuilt = 0x811902d7, // Model Look Built
            ModelLookBVHData = 0xb9e51d26, // Model Look BVH Data
            ModelLookBVHInfo = 0xdf9fdf12, // Model Look BVH Info
            ModelLookBVHAdditionalLodInfo = 0xaf739ebf, // Model Look BVH Lod Info
            ModelMaterial = 0x3250bb80, // Model Material
            ModelJointHierarchy = 0x90cdb60c, // Model Joint Hierarchy
            ModelJoint = 0x15df9d3b, // Model Joint
            ModelMirrorIds = 0xc5354b60, // Model Mirror Ids
            ModelLeafIds = 0xb7380e8c, // Model Leaf Ids
            ModelSplineRadii = 0x7496d749, // Model Spline Radii
            ModelJointBspheres = 0x0ad3a708, // Model Joint Bspheres
            ModelJointLookup = 0xee31971c, // Model Joint Lookup
            ModelBindPose = 0xdcc88a19, // Model Bind Pose
            ModelInvBindPose = 0x2c2ca205, // Model Inv Bind Pose
            ModelLocator = 0x9f614fab, // Model Locator
            ModelLocatorLookup = 0x731cbc2e, // Model Locator Lookup
            ModelCollisionIndexData = 0x5796fef6, // Model Collision Index Data
            ModelCollisionVertexData = 0xf4cb2f37, // Model Collision Vertex Data
            ModelCollisionComplexity = 0xd2b0cb7d, // Model Collision Complexity
            ModelHavokData = 0xefd92e68, // Model Physics Data
            ModelRagDollMetaData = 0x707f1b58, // Model's Ragdoll meta data
            ModelDestructibleMetaData = 0x1112df22, // Model's Destructible meta data
            ModelClothMetaData = 0x5a39fab7, // Model's Cloth meta data
            ModelIKSetupData = 0x9a434b29, // Model's ik setup data
            ModelAnimVertInfo2 = 0x06e24ed4, // Model Anim Vert Info2
            ModelAnimMorph2Info = 0x948c38cd, // Model Anim Morph2 Info
            ModelAnimMorph2Batches = 0x5e03cefe, // Model Anim Morph2 Batches
            ModelAnimMorph2ValidMasks = 0xde8d9dcf, // Model Anim Morph2 Valid Masks
            ModelAnimMorph2Deltas = 0x573c714b, // Model Anim Morph2 Deltas
            ModelAnimGeomInfo = 0xcd903318, // Model Anim Geom Info
            ModelAnimGeomParticles = 0x3f70f60d, // Model Anim Geom Particles
            ModelAnimGeomMeshInfo = 0x92123a05, // Model Anim Geom Mesh Info
            ModelAnimZiva2Info = 0x6694064e, // Model Anim Ziva2 Info
            ModelAnimVertNormalInfo = 0x855275d7, // Model Anim Vert Normal Info
            ModelAnimVertNormalStitches = 0x8a84e4d6, // Model Anim Vert Normal Stitches
            ModelAnimVertSmoothInfo = 0x55187f02, // Model Anim Vert Smooth Info
            ModelBVol = 0x53ea4273, // Model BVol
            ModelVGroup = 0xfb7f6a48, // Model VGroup
            ModelMeshNames = 0xb32d7671, // Model Mesh Names
            ModelContentAnimVertInfos = 0x14a9959c, // Model Content Anim Vert Infos
            ModelTextureOverrides = 0x00823787, // Model Texture Overrides
            ModelRenderOverrides = 0xbce86b01, // Model Render Overrides
            ModelAmbientShadowPrims = 0x7ca37da0, // Ambient Shadow Prims
            ModelAnimDynamicsDef = 0xadd1cbd3, // Model Anim Dynamics Def
            ModelStrandSubsets = 0x3c9dabdf, // Model Spline Subsets
            ModelStrands = 0x27ca5246, // Model Splines
            ModelStrandCVs = 0xb25b3163, // Model Splines CVs
            ModelStrandSBs = 0xbb7303d5, // Model Spline Skin Binding
            ModelStrandJBs = 0x14d8b13c, // Model Spline Joint Binding
            ModelStrandCVWeights = 0x5d5cf541, // Model Spline Joint Weights
            ModelRayTracingParameters = 0x0ba45069, // Model Ray-Tracing Parameters
            ModelRayTracingAdditionalParameters = 0x4f26e619, // Model Ray-Tracing Additional Parameters
            ModelDefaultLook = 0xe2b463e7, // Default
            ModelDefaultLookLower = 0x2d095a7b, // default
        }

        public enum MaterialHashes : UInt32
        {
            ShaderGPUProgramData = 0xbbfc8900, // Shader GPU Program Data
            MaterialTemplateHeader = 0x1a915d8d, // Material Template Header
            MaterialTemplateATestSource = 0xfd113362, // Material Template Source Texture for alpha testing
            MaterialTemplateSamplers = 0x1cafe804, // Material Template Samplers
            MaterialTemplateGlobals = 0xc24b19d9, // Material Template Globals
            MaterialTemplateConsts = 0x45c4f4c0, // Material Template Constants
            MaterialTemplateConstsContent = 0xa59f667b, // Material Template Constants Content
            MaterialTemplateLoDDescriptors = 0xbc93fb5e, // Material Template LoD Descriptors
            MaterialTemplateModelSlotExclusion = 0xc32e7230, // Material Template Model Slot Exclusion List
            MaterialTemplateVBufferData = 0xbb85a495, // Material Template VBuffer Data
            MaterialTemplateLoDShader0 = 0x7a856e76, // Material Template LoD Shader 0
            MaterialTemplateLoDShader1 = 0x0d825ee0, // Material Template LoD Shader 1
            MaterialTemplateLoDShaderData = 0xea7e5efe, // Material Template LoD Shader Data
            MaterialTemplateLoDShaderInfo = 0x8c049cca, // Material Template LoD Shader Info
            MaterialTemplateHitShaderInfo = 0xf9c35f30, // Material Template Hit Shader Info
            MaterialTemplateVolumetricShaderInfo = 0x2427efc4, // Material Template Volumetric Shader Info
            MaterialTemplateCloudShaderInfo = 0x6b98165f, // Material Template Clouds Shader Info
            MaterialTemplateApplyLightingShader = 0x3974047d, // Material Template Apply Lighting Shader
            MaterialHeader = 0xe1275683, // Material Header
            MaterialFurInfo = 0xd9b12454, // Material Fur Info
            MaterialWaterInfo = 0x958f7b33, // Material Water Info
            MaterialVariationInfo = 0x3e45aa13, // Material Variation Info
            MaterialSerializedData = 0xf5260180, // Material Serialized Data
            MaterialVBufferData = 0x77d77983, // Material VBuffer Data
        }
    }
}
