using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRCFaceTracking.Tools.Avatar_Setup.Containers;
using static VRCFaceTracking.Tools.Avatar_Setup.Containers.VRCFTParameterContainers;
using static VRCFaceTracking.Tools.Avatar_Setup.FaceTrackingToolsetWindow;
using System;

public static class UnifiedConversionConfigurator
{
    private static bool _initializedParameters = false;

    private static GUIStyle arrowStyle = new GUIStyle(EditorStyles.miniButton);
    private static Vector2 _scrollPos = Vector2.zero;

    private static bool showGazeDrawer = true;
    private static bool showShapeDrawer = true;

    public static void DrawConversionConfigurator()
    {
        arrowStyle.richText = true;
        arrowStyle.fixedWidth = 18f;
        arrowStyle.normal.textColor = Color.white;
        arrowStyle.fontSize = 16;

        showGazeDrawer = EditorGUILayout.Foldout(showGazeDrawer, "Eye Gaze Shapes");
        if (showGazeDrawer)
        {
            EditorGUI.indentLevel = 1;
            DrawEyeDrawer("Right", ref customParametersContainer.RightEye, true);
            DrawEyeDrawer("Left", ref customParametersContainer.LeftEye, false);
            EditorGUI.indentLevel = 0;
        }

        showShapeDrawer = EditorGUILayout.Foldout(showShapeDrawer, "Unified Shape Conversions");
        if (showShapeDrawer)
        {
            DrawBaseShapes();
        }
    }

    private static void DrawEyeDrawer(string eyeName, ref UserEyeParameter eye, bool rightHandCoord = true ) 
    {
        EditorGUI.indentLevel = 1;
        EditorGUILayout.LabelField(eyeName + " Eye Shapes");

        eye.showGazes = EditorGUILayout.Foldout(eye.showGazes, "Gaze");
        if (eye.showGazes)
        {
            EditorGUI.indentLevel = 2;

            eye.lookUpShape = EditorGUILayout.TextField(eyeName + " Eye Look Up", eye.lookUpShape);
            eye.lookDownShape = EditorGUILayout.TextField(eyeName + " Eye Look Down", eye.lookUpShape);
            eye.lookRightShape =
                rightHandCoord ?
                EditorGUILayout.TextField(eyeName + " Eye Look Out", eye.lookRightShape) :
                EditorGUILayout.TextField(eyeName + " Eye Look In", eye.lookLeftShape);
            eye.lookLeftShape =
                rightHandCoord ?
                EditorGUILayout.TextField(eyeName + " Eye Look In", eye.lookLeftShape) :
                EditorGUILayout.TextField(eyeName + " Eye Look Out", eye.lookRightShape);
        }
        EditorGUILayout.Space(0);

        EditorGUI.indentLevel = 1;

        eye.openness = EditorGUILayout.TextField(eyeName + " Closed", eye.openness);
        eye.pupilDiameter = EditorGUILayout.TextField(eyeName + " Pupil Diameter", eye.pupilDiameter);

        EditorGUI.indentLevel = 0;

        EditorGUILayout.Space(20);
    }

    private static void DrawBaseShapes()
    {
        EditorGUI.indentLevel = 1;
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        foreach (var parameter in customParametersContainer.Parameters)
        {
            string connectionsMsg = "Unified Expressions used:";
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            if (GUILayout.Button(
                new GUIContent(
                    "Remove",
                    "Removes parameter."),
                GUILayout.MaxWidth(60f)
                ))
            {
                customParametersContainer.Parameters.Remove(parameter);
                break;
            }

            if (GUILayout.Button(
                new GUIContent(
                    "↿",
                    "Move Parameter upwards."),
                arrowStyle
                ))
            {
                var _parameter = parameter;
                customParametersContainer.Parameters.Remove(parameter);
                var i = customParametersContainer.Parameters.IndexOf(parameter);
                customParametersContainer.Parameters.Insert(i - 1 < 0 ? i : i - 1, _parameter);
                break;
            }

            if (GUILayout.Button(
                new GUIContent(
                    "⇂",
                    "Move Parameter downwards."),
                arrowStyle
                ))
            {
                var _parameter = parameter;
                customParametersContainer.Parameters.Remove(parameter);
                var i = customParametersContainer.Parameters.IndexOf(parameter);
                customParametersContainer.Parameters.Insert(i + 1 > customParametersContainer.Parameters.Count ? customParametersContainer.Parameters.Count : i + 1, _parameter);
                break;
            }
            parameter.Name = EditorGUILayout.TextField(parameter.Name);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button(
                new GUIContent(
                    "Unified Connections",
                    "Opens a menu to select connections to Unified.")))
            {
                GenericMenu menu = new GenericMenu();

                for (int j = 0; j < (int)UnifiedExpressions.Max; j++)
                {
                    bool selected = parameter.UnifiedConnections.Contains((UnifiedExpressions)j);
                    var index = j; // distinguished value per anon func.

                    string appendString =
                        ((UnifiedExpressions)j).ToString().Contains("Jaw") ? "Jaw/" :
                        ((UnifiedExpressions)j).ToString().Contains("Brow") ? "Brow/" :
                        ((UnifiedExpressions)j).ToString().Contains("Lip") ? "Lip/" :
                        ((UnifiedExpressions)j).ToString().Contains("Mouth") ? "Mouth/" :
                        ((UnifiedExpressions)j).ToString().Contains("Tongue") ? "Tongue/" :
                        ((UnifiedExpressions)j).ToString().Contains("Eye") ? "Eye/" :
                        ((UnifiedExpressions)j).ToString().Contains("Nasal") ? "Nasal/" :
                        ((UnifiedExpressions)j).ToString().Contains("Cheek") ? "Cheek/" :
                        ((UnifiedExpressions)j).ToString().Contains("Nose") ? "Nose/" :
                        ((UnifiedExpressions)j).ToString().Contains("Neck") ? "Neck/" : "";

                    menu.AddItem(new GUIContent((
                        appendString + (UnifiedExpressions)j).ToString()),
                        selected,
                        () => 
                        {
                            if (!parameter.UnifiedConnections.Contains((UnifiedExpressions)index))
                                parameter.UnifiedConnections.Add((UnifiedExpressions)index);
                            else parameter.UnifiedConnections.Remove((UnifiedExpressions)index);
                        }
                        );
                }

                menu.ShowAsContext();
            }

            foreach (UnifiedExpressions e in parameter.UnifiedConnections) 
                connectionsMsg += "\n" + e.ToString();

            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(155);

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);

            if (GUILayout.Button(
                new GUIContent(
                    "Add Parameter",
                    "Adds parameter in front of the above listed parameter.")))
            {
                customParametersContainer.Parameters.Insert(customParametersContainer.Parameters.IndexOf(parameter) + 1, new UserConversionParameter { Name = "New Shape", UnifiedConnections = new List<UnifiedExpressions>() });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.HelpBox(connectionsMsg, MessageType.None);
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15);

        if (customParametersContainer.Parameters.Count == 0)
            if (GUILayout.Button(
                new GUIContent(
                    "Add Parameter",
                    "Adds parameter in front of the above listed parameter.")))
            {
                customParametersContainer.Parameters.Add(new UserConversionParameter { Name = "New Shape", UnifiedConnections = new List<UnifiedExpressions>() });
            }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        EditorGUI.indentLevel = 0;
    }
}
