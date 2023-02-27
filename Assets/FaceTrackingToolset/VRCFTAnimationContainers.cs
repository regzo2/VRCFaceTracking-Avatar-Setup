using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Animations;
using UnityEngine;
using VRCFaceTracking.Tools.Avatar_Setup.Handlers;
using Vector2 = UnityEngine.Vector2;

namespace VRCFaceTracking.Tools.Avatar_Setup.Containers
{
    public class FTAnimationProperty
    {
        public string Name = ""; // Name of property to animate; usually correlates to a parameter or blendshape (in the case of face tracking)
        public string Heirarchy = ""; // heirarchy of the avatar gameObject that we want to animate (most likely to be a Body mesh)
        public float Value = new float();  // value to animate this property's value (blendshapes are usually 100 in this case)
        public Type Type { get; set; } // type of the gameObject that is being animated (SkinnedMeshRenderer for blendshapes.)
    }

    public class FTBlendChild
    {
        public float Position = new float();
        public float PositionY = new float();
        public FTAnimation Animation = new FTAnimation(); // Properties that correlate to Properties in an animation clip (or Clip Curves);
        public ChildMotion ToChildMotion()
        {
            return new ChildMotion
            {
                position = new Vector2(Position, PositionY),
                motion = Animation.ToAnimationClip(),
                timeScale = 1.0f
            };
        }
    }
    public class FTAnimation
    {
        public string Name = "";
        public List<FTAnimationProperty> Properties = new List<FTAnimationProperty>(); // Properties that correlate to Properties in an animation clip (or Clip Curves);
        public AnimationClip ToAnimationClip()
        {
            AnimationClip _clip = new AnimationClip();

            foreach(var _prop in Properties)
                _clip.SetCurve(
                    _prop.Heirarchy, _prop.Type, _prop.Name, 
                    new AnimationCurve(
                        new Keyframe[] { new Keyframe { 
                            value = _prop.Value, 
                            time = 0.0f 
                        }}));

            VRCFTAnimationHandlers.SaveAnimationClip(_clip, Name);
            return _clip;
        }
    }

    public enum FTBlendType
    {
        Direct, // Direct Blend
        Blend, // 1D/2D Blend
    }

    public class FTBlend // this is what FTParameters will generate and populate.
    {
        public string Name = "";
        public string Parameter = ""; // Name of the X parameter that will drive this animation. Usually used as a direct driver.
        public FTBlendType Type = new FTBlendType();
        public List<FTBlendChild> Children = new List<FTBlendChild>(); // All animation clips in this specific blend
        public void AddChild(FTBlendChild child) => Children.Add(child);
        public void AddChild(List<FTBlendChild> childs) => childs.ForEach(c => Children.Add(c));

        private List<FTBlendChild> GetStableChildren() // This will ensure that Write Defaults on or off is fully cross compatible when converting to a Unity BlendTree.
        {
            List<FTBlendChild> _children = Children;
            List<FTAnimationProperty> _allProperties = new List<FTAnimationProperty>();

            foreach (FTBlendChild child in _children)
                _allProperties.AddRange(child.Animation.Properties); // Grab all properties and stuff them into a list.

            foreach (FTBlendChild child in _children)
            {
                foreach (var _prop in _allProperties)
                {
                    if (child.Animation.Properties.Contains(_prop))
                        continue;
                    var _propZero = _prop;
                    _propZero.Value = 0.0f;
                    child.Animation.Properties.Add(_propZero);
                }
            }

            return _children;
        }

        public BlendTree ToBlendTree() // Finalized Blend bake
        {
            List<FTBlendChild> _children = GetStableChildren();
            List<ChildMotion> _childMotions = new List<ChildMotion>();

            foreach (FTBlendChild _child in _children)
                _childMotions.Add(_child.ToChildMotion());

            BlendTree _tree = new BlendTree();

            _tree.blendType = Type == FTBlendType.Direct ? BlendTreeType.Direct : BlendTreeType.Simple1D;
            _tree.blendParameter = Parameter;
            _tree.children = _childMotions.ToArray();

            return _tree;

        }
    }


}
