using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MuultiplayerManager : MonoBehaviour
{
    public static MuultiplayerManager instance;

    public int playerThatJoined = 0;

    [SerializeField] GameObject countDown;

    public List<Transform> playerSpawnPoint;
    public List<Material> playersMat;

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

    private void Start()
    {
        playerThatJoined = 0;
    }

    public void OnJoined()
    {
        playerThatJoined++;
        countDown.SetActive(true);
    }
}