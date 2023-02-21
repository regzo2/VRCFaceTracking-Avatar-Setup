using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public static class FTAnimationContainers
    {
        public interface IFTAnimation
        {
            string Name { get; set; }
            Animation CreateAnimation();
        }

        public interface IFTBlend
        {
            string Name { get; set; }
            Animation CreateBlendTree();
        }

        public class IFTPosNegBlend : IFTBlend
        {
            public string Name { get; set; }
            public IFTAnimation PositiveAnimation { get; set; }
            public IFTAnimation NegativeAnimation { get; set; }
            public float PositiveRange { get; set; }
            public float NegativeRange { get; set; }
            public Animation CreateBlendTree()
            {
                throw new System.NotImplementedException();
            }
        }

        public class FTDirectAnimation : IFTAnimation
        {
            public string Name { get; set; }
            public Animation Animation { get; set; }

            public Animation CreateAnimation()
            {
                return Animation;
            }
        }

        public class FTBlendShape
        {
            public string name; // Name of blendshape. Used in animation.
            public string meshPath; // Mesh to base the animations on. Used in animation
            public float minRange; // Min range to drive this blendshape.
            public float maxRange = 100; // Max range to drive this blendshape. Default is 100 which is the maximum power of a blendshape.
            public int blendshapeIndex = -1; // Index of blendshape on associated mesh in-case we need to get a reference to it again.
        }

        public class FTBlendShapeAnimation : IFTAnimation
        {
            public string Name { get; set; }
            public List<FTBlendShape> Shapes { get; set; } // All shapes we are driving from the avatar.

            public Animation CreateAnimation()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
