using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;

[CustomEditor(typeof(PlayerPickDrop))]
public class PickupDropInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("How the player grabs and drops the bomb, with inventory location, UI and AI.", EditorStyles.largeLabel);
        GUI.color = Color.white;
        DrawDefaultInspector();
    }
}
