using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinManager : MonoBehaviour
{
    public static PlayerJoinManager instance;

    public int playerThatJoined;

    public List<Transform> playerSpawnPoint;
    public List<Material> playersMat;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnJoined()
    {
        playerThatJoined++;
    }
}