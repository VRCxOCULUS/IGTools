using System.Collections.Generic;
using System.IO;

namespace DAT1
{
    public static class Hashes
    {
        public static Dictionary<uint, string> ModelHashes = new Dictionary<uint, string>
        {
            { 0, "Model Built" },
            { 0, "Model Subset" },
            { 0, "Model Subset Geom Data" },
            { 0, "Model Subset Skin Data" },
            { 0, "Model Look" },
            { 0, "Model Look Group" },
            { 0, "Model Look Built" },
            { 0, "Model Look BVH Data" },
            { 0, "Model Look BVH Info" },
            { 0, "Model Look BVH Lod Info" },
            { 0, "Model Material" },
            { 0, "Model Joint Hierarchy" },
            { 0, "Model Joint" },
            { 0, "Model Mirror Ids" },
            { 0, "Model Leaf Ids" },
            { 0, "Model Spline Radii" },
            { 0, "Model Joint Bspheres" },
            { 0, "Model Joint Lookup" },
            { 0, "Model Bind Pose" },
            { 0, "Model Inv Bind Pose" },
            { 0, "Model Locator" },
            { 0, "Model Locator Lookup" },
            { 0, "Model Collision Index Data" },
            { 0, "Model Collision Vertex Data" },
            { 0, "Model Collision Complexity" },
            { 0, "Model Physics Data" },
            { 0, "Model's Ragdoll meta data" },
            { 0, "Model's Destructible meta data" },
            { 0, "Model's Cloth meta data" },
            { 0, "Model's ik setup data" },
            { 0, "Model Anim Vert Info2" },
            { 0, "Model Anim Morph2 Info" },
            { 0, "Model Anim Morph2 Batches" },
            { 0, "Model Anim Morph2 Valid Masks" },
            { 0, "Model Anim Morph2 Deltas" },
            { 0, "Model Anim Geom Info" },
            { 0, "Model Anim Geom Particles" },
            { 0, "Model Anim Geom Mesh Info" },
            { 0, "Model Anim Ziva2 Info" },
            { 0, "Model Anim Vert Normal Info" },
            { 0, "Model Anim Vert Normal Stitches" },
            { 0, "Model Anim Vert Smooth Info" },
            { 0, "Model BVol" },
            { 0, "Model VGroup" },
            { 0, "Model Mesh Names" },
            { 0, "Model Content Anim Vert Infos" },
            { 0, "Model Texture Overrides" },
            { 0, "Model Render Overrides" },
            { 0, "Ambient Shadow Prims" },
            { 0, "Model Anim Dynamics Def" },
            { 0, "Model Spline Subsets" },
            { 0, "Model Splines" },
            { 0, "Model Splines CVs" },
            { 0, "Model Spline Skin Binding" },
            { 0, "Model Spline Joint Binding" },
            { 0, "Model Spline Joint Weights" },
            { 0, "Model Ray-Tracing Parameters" },
            { 0, "Model Ray-Tracing Additional Parameters" },
            { 0, "Default" },
            { 0, "default" },

            { 0, "Model Lod Info Built" },
            { 0, "Model Lod Pool Built" },
            { 0, "Model Lod Geometry" },
            { 0, "Model Lod Material Names" },
            { 0, "Model Lod Bone Names" },
            { 0, "Model Lod Joint Names" },
            { 0, "Model Lod Anim Vert Key" },
        };
    }

    public class Model
    {
        public Model(BinaryReader br, DAT1 header)
        {

        }
    }
}
