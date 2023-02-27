using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Handlers.FTParameterHandler;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace VRCFaceTracking.Tools.Avatar_Setup
{
    [CustomEditor(typeof(VRCFTAvatarSetup))]
    [CanEditMultipleObjects]
    public class VRCFTAvatarSetupEditor : Editor
    {
        VRCFTAvatarSetup _setup;
        VRCAvatarDescriptor _avdescriptor;

        public override void OnInspectorGUI()
        {
            _setup = (VRCFTAvatarSetup)target;
            _avdescriptor = _setup.gameObject.GetComponent<VRCAvatarDescriptor>();

            if (_setup.customParametersContainer == null)
                _setup.customParametersContainer = ScriptableObject.CreateInstance<UserConversionConfig>();

            EditorGUI.BeginChangeCheck();
            _setup.customParametersContainer =
            (UserConversionConfig)EditorGUILayout.ObjectField(
                new GUIContent(
                    "Config",
                    "A preset configuration that stores Parameter Configuration data. " +
                    "This is intended for saving configurations for use later or sharing."),
                _setup.customParametersContainer,
                typeof(UserConversionConfig),
                false
            );
            if (EditorGUI.EndChangeCheck())
            {
                _setup.allUserParameters.Clear();
                _setup.customParametersContainer.GetAllParameters().ForEach(p => _setup.allUserParameters.AddRange(p.ToUnifiedParameters()));
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button(
                new GUIContent(
                    "Use Unified Expressions",
                    "Adds translation parameters that are 1:1 with Unified Expressions.")))
            {
                _setup.customParametersContainer.RightEye.lookUpShape = "EyeLookUpRight";
                _setup.customParametersContainer.RightEye.lookDownShape = "EyeLookDownRight";
                _setup.customParametersContainer.RightEye.lookRightShape = "EyeLookOutRight";
                _setup.customParametersContainer.RightEye.lookLeftShape = "EyeLookInRight";
                _setup.customParametersContainer.RightEye.openness = "EyeClosedRight";
                _setup.customParametersContainer.RightEye.pupilDiameter = "PupilDiameterRight";

                _setup.customParametersContainer.LeftEye.lookUpShape = "EyeLookUpLeft";
                _setup.customParametersContainer.LeftEye.lookDownShape = "EyeLookDownLeft";
                _setup.customParametersContainer.LeftEye.lookRightShape = "EyeLookInLeft";
                _setup.customParametersContainer.LeftEye.lookLeftShape = "EyeLookOutLeft";
                _setup.customParametersContainer.LeftEye.openness = "EyeClosedLeft";
                _setup.customParametersContainer.LeftEye.pupilDiameter = "PupilDiameterLeft";

                _setup.customParametersContainer.Parameters.Clear();
                for (int i = 0; i < (int)UnifiedExpressions.Max; i++)
                    _setup.customParametersContainer.Parameters.Add(new UserConversionParameter
                    {
                        Name = ((UnifiedExpressions)i).ToString(),
                        UnifiedConnections = new List<UnifiedExpressions> { (UnifiedExpressions)i }
                    });
            }

            DrawShapeSetup();
            DrawAvatarSetup();
        }

        private void DrawAvatarSetup()
        {
            _setup.advancedSuggest = EditorGUILayout.Toggle(
                new GUIContent(
                    "Advanced",
                    "Adds more sliders for FT suggestions."),
                _setup.advancedSuggest
            );

            if (_avdescriptor != null)
                _setup.maxRange = VRCExpressionParameters.MAX_PARAMETER_COST - _avdescriptor.expressionParameters.CalcTotalCost();
            else _setup.maxRange = VRCExpressionParameters.MAX_PARAMETER_COST;

            _setup.availableBits = (int)EditorGUILayout.Slider(
                new GUIContent(
                    "Bits",
                    "Tries to fit parameters into set space."),
                _setup.availableBits,
                _setup.minRange,
                _setup.maxRange
            );

            if (!_setup.advancedSuggest)
            {
                _setup.importanceBias = 1;
                _setup.importanceThreshold = 1;
                switch (_setup.availableBits)
                {
                    case int i when i <= 40:
                        _setup.qualityThreshold = 0;
                        break;
                    case int i when i <= 50:
                        _setup.qualityThreshold = 1;
                        break;
                    case int i when i <= 60:
                        _setup.qualityThreshold = 2;
                        break;
                    case int i when i <= 70:
                        _setup.qualityThreshold = 3;
                        break;
                    case int i when i <= 90:
                        _setup.qualityThreshold = 4;
                        break;
                    case int i when i <= 128:
                        _setup.qualityThreshold = 5;
                        break;
                    case int i when i <= 192:
                        _setup.qualityThreshold = 6;
                        break;
                    case int i when i <= 256:
                        _setup.qualityThreshold = 7;
                        break;
                }
            }
            else
            {
                _setup.qualityThreshold = (int)EditorGUILayout.Slider(
                    new GUIContent(
                        "Quality",
                        "Quality 11 removes all combination parameters"),
                    _setup.qualityThreshold,
                    0,
                    11
                );

                _setup.importanceBias = (int)EditorGUILayout.Slider(
                    new GUIContent(
                        "Bias",
                        "0 removes all importance bias"),
                    _setup.importanceBias,
                    0,
                    4
                );

                _setup.importanceThreshold = (int)EditorGUILayout.Slider(
                    new GUIContent(
                        "Parameter Threshold",
                        "1 removes currently unused shapes from all interfaces"),
                    _setup.importanceThreshold,
                    0,
                    11
                );
            }

            EditorGUILayout.HelpBox(
                "Quality: " + _setup.qualityThreshold +
                "\nParameter Importance Cutoff: " + _setup.importanceThreshold +
                "\nImportance Bias: " + _setup.importanceBias,
                MessageType.None);

            _setup.parameterHandler.qualityThreshold = _setup.qualityThreshold;
            _setup.parameterHandler.importanceThreshold = _setup.importanceThreshold;
            _setup.parameterHandler.availableBits = _setup.availableBits;
            _setup.parameterHandler.importanceBias = _setup.importanceBias;

            _setup._recParameters = _setup.parameterHandler.RecommendParameters(_setup.allUserParameters, AllUnifiedCombinedExpressions);

            string totalMsg = "";
            int totalSize = 0;

            foreach (FTParameter parameter in _setup._recParameters)
            {
                totalMsg += parameter.Name + " Size: " + parameter.Size + "\n";
                totalSize += parameter.Size;

            }

            if (GUILayout.Button(
                new GUIContent(
                    "TestFX",
                    "Test Animation Setup on FX layer.")))
            {
                foreach (FTParameter parameter in _setup._recParameters)
                    parameter.Clip = new FTAnimation
                    {
                        Name = "JawOpen",
                        Properties = new List<FTAnimationProperty>
                        {
                            new FTAnimationProperty
                            {
                                Heirarchy = "Body",
                                Name = "Blendshape.JawOpen",
                                Type = typeof(SkinnedMeshRenderer),
                                Value = 100f
                            }
                        }
                    };

                foreach (FTParameter parameter in _setup._recParameters)
                {
                    FTBlend _blend = parameter.CreateFTBlend();
                    Debug.Log(
                        "Parameter Driving: " + _blend.Parameter +
                        "\nBlend Name: " + _blend.Name
                        );
                    foreach (var _child in _blend.Children)
                    {
                        Debug.Log("Animation Name: " + _child.Animation.Name);

                        foreach (var _prop in _child.Animation.Properties)
                        {
                            Debug.Log("Prop Name: " + _prop.Name + "\nProp Value: " + _prop.Value);
                        }
                    }
                    _blend.ToBlendTree();
                }
            }

            totalMsg += "\nTotal Size: " + totalSize;

            _setup.scrollMsgPos = EditorGUILayout.BeginScrollView(_setup.scrollMsgPos);
            EditorGUILayout.HelpBox(totalMsg, MessageType.None);
            EditorGUILayout.EndScrollView();
        }
        private void DrawShapeSetup()
        {
            if (_avdescriptor != null)
            {
                if (_avdescriptor == null)
                    return;

                _setup._meshes = _avdescriptor.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                _setup._meshesNames = new List<string>();

                foreach (SkinnedMeshRenderer m in _setup._meshes)
                    _setup._meshesNames.Add(m.name);

                _setup._selectMesh = EditorGUILayout.Popup(_setup._selectMesh, _setup._meshesNames.ToArray());

                if (_setup._meshesNames.Count == 0)
                    return;

                //Rect r = GetWindow(typeof(VRCFTManager)).position;
                _setup._mesh = _setup._meshes[_setup._selectMesh];

                Debug.Log(_setup._mesh.gameObject.GetHeirarchy());

                _setup._blendshapesFromMesh = GenerateBlendshapeList(_setup._mesh);

                if (GUILayout.Button
                    (
                        new GUIContent
                        (
                            "Suggest Parameters From Blendshapes",
                            "Sets a blendshape for each parameter to a best-matched keyword."
                        )))
                {
                    Parallel.ForEach(_setup.customParametersContainer.Parameters, s =>
                    {
                        int i = SuggestStringToIndex(s.Name, _setup._blendshapesFromMesh); // blendshape index
                    });
                }

                _setup.showBlendshapes = EditorGUILayout.Toggle
                (
                    "Show Shapes",
                    _setup.showBlendshapes,
                    new GUILayoutOption[]
                        {
                            GUILayout.MaxWidth(256)
                        }
                );
                int iter = 0;

                if (_setup.showBlendshapes)
                {
                    _setup.scrollPos = EditorGUILayout.BeginScrollView(_setup.scrollPos, GUILayout.Height(512));

                    foreach (var parameter in _setup.customParametersContainer.Parameters)
                    {
                        EditorGUILayout.LabelField(parameter.Name + " : " + iter, new GUILayoutOption[]
                        {
                            GUILayout.MaxWidth(256)
                        });
                        iter++;
                    }

                    EditorGUILayout.EndScrollView();
                }
            }
        }

        public static List<string> GenerateBlendshapeList(SkinnedMeshRenderer mesh)
        {
            List<string> blendshapesFromMesh = new List<string>();

            for (int i = 0; i < mesh.sharedMesh.blendShapeCount; i++)
            {
                blendshapesFromMesh.Add(mesh.sharedMesh.GetBlendShapeName(i));
            }

            return blendshapesFromMesh;
        }

        public static int SuggestStringToIndex(string s, List<string> s2)
        {

            string sCleaned = Regex.Replace(s, "[^a-zA-Z0-9]", "");
            Debug.Log(sCleaned);

            int i = -1;

            int maxEdit = 5;

            for (int j = 0; j < s2.Count; j++)
            {
                string s2Cleaned = Regex.Replace(s2[j], "[^a-zA-Z0-9]", "");
                Debug.Log(s2Cleaned);

                if (sCleaned == s2Cleaned)
                {
                    Debug.Log("Exact Match: " + sCleaned + " " + j);
                    return j;
                }

                int currentEdit = LevenshteinDistance(sCleaned.ToUpper(), s2Cleaned.ToUpper());
                if (currentEdit < maxEdit)
                {
                    maxEdit = currentEdit;
                    i = j;
                }
            }

            string s2Final = "";

            if (i != -1)
            {
                s2Final = Regex.Replace(s2[i], "[^a-zA-Z0-9]", "");
                Debug.Log("Closest Match: " + sCleaned.ToLower() + " " + s2Final.ToLower());
                return i;
            }

            Debug.Log("No Match within alloted edits: ");
            return -1;
        }

        private static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
}
