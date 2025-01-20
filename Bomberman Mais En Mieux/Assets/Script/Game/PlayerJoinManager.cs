using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinManager : MonoBehaviour
{
    public static PlayerJoinManager instance;

    public int playerThatJoined;

    public List<Transform> playerSpawnPoint;
    public List<Material> playersMat;

    public List<GameObject> playerAliveList;
    public List<GameObject> playerDeadList;

    public GameObject theBot;

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

    public void OnPlayerJoin(GameObject go)
    {
        playerAliveList.Add(go);
    }

    public void SwitchAliveToDead(GameObject go)
    {
        playerAliveList.Remove(go);
        playerDeadList.Add(go);
        go.GetComponent<PlayerMove>().enabled = false;
        go.GetComponent<PlayerPickDrop>();
    }

    public void OnJoined()
    {
        playerThatJoined++;
    }
}