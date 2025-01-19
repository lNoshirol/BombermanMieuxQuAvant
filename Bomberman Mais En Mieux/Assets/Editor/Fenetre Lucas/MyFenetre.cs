using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using UnityEditor.SceneManagement;

public class MyFenetre : EditorWindow
{
    public Color color;

    public Vector3 objectPosition;

    public float positionX;

    public float positionZ;

    public GameObject theBot;



    [MenuItem("Window/SpawnRunAwayPoint")]
    public static void ShowWindow()
    {
        GetWindow<MyFenetre>("PointGenerator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Génère un point à fuir pour l'ia.", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        theBot = EditorGUILayout.ObjectField("Le bot de la scene ", theBot, typeof(GameObject), true) as GameObject;
        
        EditorGUILayout.Space();

        objectPosition = EditorGUILayout.Vector3Field("Run Away Point Position", objectPosition);
        objectPosition.x = Mathf.Clamp(objectPosition.x, -19, 19);
        objectPosition.z = Mathf.Clamp(objectPosition.z, -19, 19);
        objectPosition.y = 0;

        EditorGUILayout.Space();

        if (GUILayout.Button("SpawnEmpty"))
        {
            GameObject newRunAwayPoint = new("RunAwayPoint");

            newRunAwayPoint.transform.position = objectPosition;

            if (Selection.gameObjects.Length == 1)
            {
                newRunAwayPoint.transform.parent = Selection.gameObjects[0].transform;
            }

            if (theBot != null)
            {
                theBot.GetComponent<BotBRAIN>().thingsToRunAway.Add(newRunAwayPoint);
            }

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }
}