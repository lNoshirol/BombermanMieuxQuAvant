using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    PlayerJoinManager playerJoinManager;

    private void Awake()
    {
        playerJoinManager = PlayerJoinManager.instance;

        if (playerJoinManager.theBot != null)
        {
            playerJoinManager.theBot.GetComponent<BotBRAIN>().player = gameObject;
        }

        transform.position = playerJoinManager.playerSpawnPoint[playerJoinManager.playerThatJoined-1].position; 
        GetComponent<MeshRenderer>().material = playerJoinManager.playersMat[playerJoinManager.playerThatJoined - 1];
    }
}