using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuultiplayerManager : MonoBehaviour
{
    public static MuultiplayerManager instance;

    public int playerThatJoined;

    public List<Transform> playerSpawnPoint;
    public List<Material> playersMat;

    public List<GameObject> playerAliveList;
    public List<GameObject> playerDeadList;

    public GameObject theBot;

    public GameObject winPanel;
    public TextMeshProUGUI winText;

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
        go.GetComponent<PlayerHealth>().OnDamageTakeGameObject += CheckPlayerHealth;
    }

    public void CheckPlayerHealth(GameObject go, int pv)
    {
        if (pv <= 0)
        {
            SwitchAliveToDead(go);
        }

        CheckWinConditionHogRider();
    }

    public void CheckWinConditionHogRider()
    {
        if (playerAliveList.Count == 1)
        {
            Win(playerAliveList[0]);
        }
    }

    public void Win(GameObject goWinner)
    {
        if (winPanel != null) {
            Time.timeScale = 0;
            winPanel.SetActive(true);
            winText.text += " " + goWinner.name;
            winPanel.GetComponent<Image>().color = goWinner.GetComponent<MeshRenderer>().material.color;
        }

    }

    public void SwitchAliveToDead(GameObject go)
    {
        playerAliveList.Remove(go);
        playerDeadList.Add(go);
        go.GetComponent<PlayerMove>().enabled = false;
        go.GetComponent<PlayerPickDrop>().playerCanvas.SetActive(false);
        go.GetComponent<PlayerPickDrop>().enabled = false;
    }

    public void OnJoined()
    {
        playerThatJoined++;
    }
}