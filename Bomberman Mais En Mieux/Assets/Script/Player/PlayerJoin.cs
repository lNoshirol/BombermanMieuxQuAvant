using UnityEngine;

public class PlayerJoin : MonoBehaviour
{
    PlayerJoinManager playerJoinManager;

    private void Awake()
    {
        playerJoinManager = PlayerJoinManager.instance;

        if (playerJoinManager.theBot != null)
        {
            BotBRAIN bot = playerJoinManager.theBot.GetComponent<BotBRAIN>();
            bot.player = gameObject;
            bot.thingsToRunAway.Add(gameObject);
        }

        transform.position = playerJoinManager.playerSpawnPoint[playerJoinManager.playerThatJoined-1].position; 
        GetComponent<MeshRenderer>().material = playerJoinManager.playersMat[playerJoinManager.playerThatJoined - 1];

        GetComponent<PlayerPickDrop>().OndropBomb += playerJoinManager.theBot.GetComponent<BotBRAIN>().ABombHasBeenPlanted;

        BotStateMachine botStateMachine = playerJoinManager.theBot.GetComponent<BotStateMachine>();
        botStateMachine.ChangeState(botStateMachine.searchBombState);
    }
}