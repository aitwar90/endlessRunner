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
        if(GUILayout.Button("Generuj błyskawice"))
        {
            myScript.GenerateVisualObject(TypeVisualBase.Błyskawica);
        }
        if(GUILayout.Button("Generuj Kulę lawy"))
        {
            myScript.GenerateVisualObject(TypeVisualBase.KulaLawy);
        }
        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
    }
}
