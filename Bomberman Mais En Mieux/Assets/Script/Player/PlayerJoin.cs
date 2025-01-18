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

            GetComponent<PlayerPickDrop>().OnDropBomb += playerJoinManager.theBot.GetComponent<BotBRAIN>().ABombHasBeenPlanted;
            GetComponent<PlayerPickDrop>().OnPickBomb += playerJoinManager.theBot.GetComponent<BotBRAIN>().BombHasBeenTake;
        }

        transform.position = playerJoinManager.playerSpawnPoint[playerJoinManager.playerThatJoined-1].position; 
        GetComponent<MeshRenderer>().material = playerJoinManager.playersMat[playerJoinManager.playerThatJoined - 1];

        

        BotStateMachine botStateMachine = playerJoinManager.theBot.GetComponent<BotStateMachine>();
        botStateMachine.ChangeState(botStateMachine.searchBombState);
    }
}