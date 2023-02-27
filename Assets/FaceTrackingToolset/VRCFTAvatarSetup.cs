using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Handlers.FTParameterHandler;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRCFaceTracking.Tools.Avatar_Setup.Handlers;

namespace VRCFaceTracking.Tools.Avatar_Setup
{
    public class VRCFTAvatarSetup : MonoBehaviour
    {
        public UserConversionConfig customParametersContainer;
        public FTParameterHandler parameterHandler = new FTParameterHandler();

        public SkinnedMeshRenderer _mesh;
        public SkinnedMeshRenderer[] _meshes;

        public List<string> _meshesNames;
        public List<string> _blendshapesFromMesh;
        public int[] _selectBlendshapes = new int[1024];
        public Vector2 scrollPos;
        public bool showBlendshapes;
        public int _selectMesh;
        public List<FTParameter> allUserParameters = new List<FTParameter>();

        public bool advancedSuggest;

        public int minRange = 32;
        public int maxRange = VRCExpressionParameters.MAX_PARAMETER_COST;

        public int availableBits = VRCExpressionParameters.MAX_PARAMETER_COST;
        public int qualityThreshold;
        public int importanceThreshold;
        public int importanceBias;

        public List<FTParameter> _recParameters = new List<FTParameter>();
        public Vector2 scrollMsgPos = Vector2.zero;
    }
}
