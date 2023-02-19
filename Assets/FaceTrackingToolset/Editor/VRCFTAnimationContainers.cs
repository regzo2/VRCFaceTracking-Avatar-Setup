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
            Animation GetAnimation();
        }

        public class FTDirectAnimation : IFTAnimation
        {
            public string Name { get; set; }
            public Animation Animation { get; set; }

            public Animation GetAnimation()
            {
                return Animation;
            }
        }

        public class FTBlendShape
        {
            public string name; // Name of blendshape. Used in animation.
            public string meshPath; // Mesh to base the animations on.
            public float minRange; // Min range to drive this blendshape.
            public float maxRange; // Max range to drive this blendshape.
            public int blendshapeIndex = -1; // Index of blendshape on associated mesh in-case we need to get a reference to it again.
        }

        public class FTBlendShapeAnimation : IFTAnimation
        {
            public string Name { get; set; }
            public List<FTBlendShape> Shapes { get; set; } // All shapes we are driving from the avatar.

            public Animation GetAnimation()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
