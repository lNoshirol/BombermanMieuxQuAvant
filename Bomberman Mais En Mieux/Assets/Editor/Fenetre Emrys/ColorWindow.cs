using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorWindow : EditorWindow
{
    [MenuItem("Window/RandomColorWindow")]
    public static void ShowWindow()
    {
        GetWindow<ColorWindow>("Randomize Color Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("Randomize the color of the wall", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Randomize"))
        {
            RandomizeWallColors();
        }
    }

    private void RandomizeWallColors()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        foreach (GameObject wall in walls)
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial.color = randomColor;
            }
        }
    }
}
