using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Handlers.FTParameterHandler;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup
{
    public class VRCFTSetupConfiguratorWindow : EditorWindow
    {
        int tab = 0;

        private VRCAvatarDescriptor _avdescriptor;
        public static UserConversionConfig customParametersContainer;
        private AnimationClip customAnimationTest;

        private SkinnedMeshRenderer _mesh;
        private SkinnedMeshRenderer[] _meshes;

        private List<string> _meshesNames;
        private List<string> _blendshapesFromMesh;
        private int[] _selectBlendshapes = new int[1024];
        private Vector2 scrollPos;
        private bool showBlendshapes;
        private int _selectMesh;
        private List<FTParameter> allUserParameters = new List<FTParameter>();

        private bool advancedSuggest = true;

        private int minRange = 24; // Set as it includes vital FT parameters.
        private int maxRange = VRCExpressionParameters.MAX_PARAMETER_COST;

        private List<FTParameter> _recParameters = new List<FTParameter>();
        private Vector2 scrollMsgPos = Vector2.zero;

        [MenuItem("VRCFaceTracking/Setup Avatar")]
        public static void ShowWindow()
        {
            AssetDatabase.Refresh();
            var window = EditorWindow.GetWindow<VRCFTSetupConfiguratorWindow>("Avatar Setup");
            window.maxSize = new Vector2(512, 1024);
            window.minSize = new Vector2(480, 480);
            window.Show();
        }

        private void OnGUI()
        {
            if (customParametersContainer == null)
                customParametersContainer = ScriptableObject.CreateInstance<UserConversionConfig>();
            customParametersContainer =
            (UserConversionConfig)EditorGUILayout.ObjectField(
                new GUIContent(
                    "Config",
                    "A preset configuration that stores Parameter Configuration data. " +
                    "This is intended for saving configurations for use later or sharing."),
                customParametersContainer,
                typeof(UserConversionConfig),
                false
            );

            EditorGUILayout.Space(10);

            if (GUILayout.Button
                    (
                        new GUIContent
                        (
                            "Save Config",
                            "Saves Parameter configuration into an asset file."
                        ),
                        GUILayout.MaxWidth((float)Screen.width)
                    ))
            {
                if (AssetDatabase.GetAssetPath(customParametersContainer) == string.Empty)
                    AssetDatabase.CreateAsset(customParametersContainer, EditorUtility.SaveFilePanelInProject("Save Unified Conversion Configuration", "UserConversionConfig", "asset", ""));
                EditorUtility.SetDirty(customParametersContainer);

                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.Space(5);

            if (GUILayout.Button(
                new GUIContent(
                    "Use Unified Expressions",
                    "Adds translation parameters that are 1:1 with Unified Expressions.")))
            {
                customParametersContainer.RightEye.lookUpShape = "EyeLookUpRight";
                customParametersContainer.RightEye.lookDownShape = "EyeLookDownRight";
                customParametersContainer.RightEye.lookRightShape = "EyeLookOutRight";
                customParametersContainer.RightEye.lookLeftShape = "EyeLookInRight";
                customParametersContainer.RightEye.openness = "EyeClosedRight";
                customParametersContainer.RightEye.pupilDiameter = "PupilDiameterRight";

                customParametersContainer.LeftEye.lookUpShape = "EyeLookUpLeft";
                customParametersContainer.LeftEye.lookDownShape = "EyeLookDownLeft";
                customParametersContainer.LeftEye.lookRightShape = "EyeLookInLeft";
                customParametersContainer.LeftEye.lookLeftShape = "EyeLookOutLeft";
                customParametersContainer.LeftEye.openness = "EyeClosedLeft";
                customParametersContainer.LeftEye.pupilDiameter = "PupilDiameterLeft";

                customParametersContainer.Parameters.Clear();
                for (int i = 0; i < (int)UnifiedExpressions.Max; i++)
                    customParametersContainer.Parameters.Add(new UserConversionParameter
                    {
                        Name = ((UnifiedExpressions)i).ToString(),
                        UnifiedConnections = new List<UnifiedExpressions> { (UnifiedExpressions)i }
                    });
            }

            EditorGUILayout.Space(20);

            allUserParameters.Clear();
            customParametersContainer.GetAllParameters().ForEach(p => allUserParameters.AddRange(p.ToUnifiedParameters()));


            UnifiedConversionConfigurator.DrawConversionConfigurator();
        }

        private void DrawShapeSetup()
        {
            _avdescriptor = (VRCAvatarDescriptor)EditorGUILayout.ObjectField("Avatar Descriptor", _avdescriptor, typeof(VRCAvatarDescriptor), true);

            if (_avdescriptor != null)
            {
                if (_avdescriptor == null)
                    return;

                _meshes = _avdescriptor.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                _meshesNames = new List<string>();

                foreach (SkinnedMeshRenderer m in _meshes)
                    _meshesNames.Add(m.name);

                _selectMesh = EditorGUILayout.Popup(_selectMesh, _meshesNames.ToArray());

                if (_meshesNames.Count == 0)
                    return;

                //Rect r = GetWindow(typeof(VRCFTManager)).position;
                _mesh = _meshes[_selectMesh];

                Debug.Log(_mesh.gameObject.GetHeirarchy());

                _blendshapesFromMesh = GenerateBlendshapeList(_mesh);

                if (GUILayout.Button
                    (
                        new GUIContent
                        (
                            "Suggest Parameters From Blendshapes",
                            "Sets a blendshape for each parameter to a best-matched keyword."
                        )))
                {
                    Parallel.ForEach(customParametersContainer.Parameters, s =>
                    {
                        int i = SuggestStringToIndex(s.Name, _blendshapesFromMesh); // blendshape index
                    });
                }

                showBlendshapes = EditorGUILayout.Toggle
                (
                    "Show Shapes",
                    showBlendshapes,
                    new GUILayoutOption[]
                        {
                            GUILayout.MaxWidth(256)
                        }
                );
                int iter = 0;

                if (showBlendshapes)
                {
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(512));

                    foreach (var parameter in customParametersContainer.Parameters)
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

        private static void CreateVRCFTAnimationLayer()
        {

        }

        public static int LevenshteinDistance(string s, string t)
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
