using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup.Handlers
{
    public static class VRCFTAnimationHandlers
    {
        public static AnimatorController animatorController;
        public static List<UnityEngine.Object> animatorAssets;
        public static string directory = "Assets/FaceTrackingToolset/Generated/Anims/";

        // Limit calling these asset database calls as much as possible, make any edits to the assets after animator is finalized.
        public static void LoadAnimator(AnimatorController _animatorController)
        {
            animatorAssets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(_animatorController)).ToList();
            animatorController = _animatorController;
        }
        public static void SaveAssets()
        {
            var _animatorAssets = animatorAssets.Except(AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(animatorController)));
            foreach (var _asset in _animatorAssets)
            {
                AssetDatabase.AddObjectToAsset(_asset, animatorController);
            }
        }
        private static void ResolveAssets(AnimationClip clip, string name, string suffix)
        {
            string[] guid = (AssetDatabase.FindAssets(name + suffix));

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (guid.Length == 0)
            {
                AssetDatabase.CreateAsset(clip, directory + name + suffix);
                AssetDatabase.SaveAssets();
                return;
            }

            AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(guid[0]));
            AssetDatabase.CreateAsset(clip, directory + name + suffix);
            AssetDatabase.SaveAssets();
        }
        public static void SaveAnimationClip(AnimationClip clip, string name) => ResolveAssets(clip, name, ".anim");
        public static void CreateDirectBlendTreeLayer(List<FTParameter> parameters)
        {
            foreach (var parameter in parameters)
            {

            }

        }
    }
}
