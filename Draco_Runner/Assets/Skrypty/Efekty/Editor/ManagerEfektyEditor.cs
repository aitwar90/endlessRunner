using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ManagerEfekty))]
public class ManagerEfektyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ManagerEfekty myScript = (ManagerEfekty)target;
        if(GUILayout.Button("Update błyskawice"))
        {
            myScript.GenerateAnotherOne();
        }
        if(GUILayout.Button("Kasuj błyskawice"))
        {
            myScript.KasujBłyskawice();
        }
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
    }
}
