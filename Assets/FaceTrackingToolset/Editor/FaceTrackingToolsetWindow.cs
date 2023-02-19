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
using static VRC.Dynamics.CollisionScene;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.FTAnimationContainers;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;

namespace VRCFaceTracking.Tools.Avatar_Setup
{
    public class FaceTrackingToolsetWindow : EditorWindow
    {
        int tab = 0;

        private VRCAvatarDescriptor _avdescriptor;
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
            var window = EditorWindow.GetWindow<FaceTrackingToolsetWindow>("Avatar Setup");
            window.maxSize = new Vector2(512, 1024);
            window.minSize = new Vector2(480, 480);
            window.Show();
        }

        private void OnGUI()
        {
            tab = GUILayout.Toolbar(tab, new string[] { "Avatar Setup", "Conversions"});

            if (UnifiedConversionConfigurator.customParametersContainer.GetAllParameters() != null)
                UnifiedConversionConfigurator.customParametersContainer.GetAllParameters().ForEach(p => allUserParameters.AddRange(p.ToUnifiedParameters()));


            switch (tab)
            {
                case 0:
                    DrawShapeSetup();
                    DrawAvatarSetup();
                    break;
                case 1:
                    UnifiedConversionConfigurator.DrawConversionConfigurator();
                    break;
            }
        }

        private void DrawAvatarSetup()
        {
            advancedSuggest = EditorGUILayout.Toggle(
                new GUIContent(
                    "Advanced",
                    "Adds more sliders for FT suggestions."),
                advancedSuggest
            );

            if (_avdescriptor != null)
                maxRange = VRCExpressionParameters.MAX_PARAMETER_COST - _avdescriptor.expressionParameters.CalcTotalCost();
            else maxRange = VRCExpressionParameters.MAX_PARAMETER_COST;

            FTParameterHandlers.availableBits = (int)EditorGUILayout.Slider(
                new GUIContent(
                    "Bits",
                    "Tries to fit parameters into set space."),
                FTParameterHandlers.availableBits,
                minRange,
                maxRange
            );

            if (!advancedSuggest)
            {
                FTParameterHandlers.importanceBias = 1;
                switch (FTParameterHandlers.availableBits)
                {
                    case int i when i <= 40:
                        FTParameterHandlers.qualityThreshold = 0;
                        break;
                    case int i when i <= 50:
                        FTParameterHandlers.qualityThreshold = 1;
                        break;
                    case int i when i <= 60:
                        FTParameterHandlers.qualityThreshold = 2;
                        break;
                    case int i when i <= 70:
                        FTParameterHandlers.qualityThreshold = 3;
                        break;
                    case int i when i <= 90:
                        FTParameterHandlers.qualityThreshold = 4;
                        break;
                    case int i when i <= 128:
                        FTParameterHandlers.qualityThreshold = 5;
                        break;
                    case int i when i <= 192:
                        FTParameterHandlers.qualityThreshold = 6;
                        break;
                    case int i when i <= 256:
                        FTParameterHandlers.qualityThreshold = 7;
                        break;
                }
            }
            else
            {
                FTParameterHandlers.qualityThreshold = (int)EditorGUILayout.Slider(
                    new GUIContent(
                        "Quality",
                        "Quality 11 removes all combination parameters"),
                    FTParameterHandlers.qualityThreshold,
                    0,
                    11
                );

                FTParameterHandlers.importanceBias = (int)EditorGUILayout.Slider(
                    new GUIContent(
                        "Bias",
                        "0 removes all importance bias"),
                    FTParameterHandlers.importanceBias,
                    0,
                    4
                );
            }

            EditorGUILayout.HelpBox(
                "Quality: " + FTParameterHandlers.qualityThreshold +
                "\nParameter Importance Cutoff: " + FTParameterHandlers.importanceThreshold +
                "\nImportance Bias: " + FTParameterHandlers.importanceBias,
                MessageType.None);

            _recParameters = FTParameterHandlers.RecommendParameters(allUserParameters, AllUnifiedCombinedExpressions);

            string totalMsg = "";
            int totalSize = 0;

            foreach (FTParameter parameter in _recParameters)
            {
                totalMsg += parameter.Name + " Size: " + parameter.Size + "\n";
                totalSize += parameter.Size;

            }

            totalMsg += "\nTotal Size: " + totalSize;

            scrollMsgPos = EditorGUILayout.BeginScrollView(scrollMsgPos);
            EditorGUILayout.HelpBox(totalMsg, MessageType.None);
            EditorGUILayout.EndScrollView();
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

                var _parameters = allUserParameters;

                _mesh = _meshes[_selectMesh];

                _blendshapesFromMesh = GenerateBlendshapeList(_mesh);

                if (GUILayout.Button
                    (
                        new GUIContent
                        (
                            "Suggest Parameters From Blendshapes",
                            "Sets a blendshape for each parameter to a best-matched keyword."
                        )))
                {
                    Parallel.ForEach(_parameters, s =>
                    {
                        int i = SuggestStringToIndex(s.Name, _blendshapesFromMesh); // blendshape index

                        if (i != -1)
                            s.IsUsed = true;
                        else
                        {
                            s.IsUsed = false;
                        }
                        FTBlendShapeAnimation shape = (FTBlendShapeAnimation) s.Animations.First(a => a.GetType() == typeof(FTBlendShapeAnimation));
                        s.Animations.Clear();
                        s.Animations.Add(new FTBlendShapeAnimation 
                        { 
                            Shapes = new List<FTBlendShape>
                            {
                                new FTBlendShape
                                {
                                    name = s.Name,
                                    blendshapeIndex = i,
                                    meshPath = _meshes[_selectMesh].ToString()
                                }
                            }
                        });
                    });
                }

                showBlendshapes = EditorGUILayout.Toggle
                (
                    "Show Blendshapes",
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

                    foreach (var parameter in _parameters)
                    {
                        EditorGUILayout.BeginHorizontal();
                        parameter.IsUsed = EditorGUILayout.Toggle(parameter.IsUsed, new GUILayoutOption[]
                        {
                            GUILayout.MaxWidth(16)
                        });

                        EditorGUILayout.LabelField(parameter.Name + " : " + iter, new GUILayoutOption[]
                        {
                            GUILayout.MaxWidth(256)
                        });

                        if (parameter.IsUsed)
                        {
                            foreach (FTBlendShapeAnimation blendshapeAnim in parameter.Animations)
                                foreach (FTBlendShape shape in blendshapeAnim.Shapes)
                                    EditorGUILayout.Popup(shape.blendshapeIndex, _blendshapesFromMesh.ToArray());
                        }
                        else _selectBlendshapes[iter] = -1;
                        iter++;

                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();
                }
            }
        }

        private object MakeTex(int v1, int v2, Color color)
        {
            throw new NotImplementedException();
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
