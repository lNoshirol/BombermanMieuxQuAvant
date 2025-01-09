using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    PlayerJoinManager playerJoinManager;

    private void Awake()
    {
        playerJoinManager = PlayerJoinManager.instance;
        transform.position = playerJoinManager.playerSpawnPoint[playerJoinManager.playerThatJoined-1].position; 
        GetComponent<MeshRenderer>().material = playerJoinManager.playersMat[playerJoinManager.playerThatJoined - 1];
    }
}