using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerPickDrop : MonoBehaviour
{
    [Header("Bombe")]
    [Tooltip("Location of the bombe inventory")]
    [SerializeField] Transform bombeStock;

    [Tooltip("number of bombe show in UI")]
    [SerializeField] TextMeshProUGUI bombeUI;

    public GameObject playerCanvas;


    [Header("IA")]
    [SerializeField] BotBRAIN botBRAIN;

    private PlayerActions _playerInput;
    private GameObject bombe;

    private GameObject bombeShow;

    public event Action<GameObject> OnDropBomb;
    public event Action<GameObject> OnPickBomb;

    [SerializeField] Renderer bombeShowStock;

    [SerializeField] Material elec;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            other.transform.position = bombeStock.position;
            other.gameObject.transform.SetParent(bombeStock);
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false;
            bombe.GetComponent<BoxCollider>().enabled = false;
            addBombeUI();
            OnPickBomb?.Invoke(bombe);

            
        }
    }

    private void Awake()
    {
        _playerInput = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnDrop(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.started)
        {
            if (bombeStock.childCount >= 1)
            {
                Transform bombeToDrop = bombeStock.GetChild(0);
                bombeToDrop.parent = null;
                bombeToDrop.GetComponent<Bombe>().StartExplosion();
                removeBombeUI();
                OnDropBomb?.Invoke(bombeToDrop.gameObject);
            }
        }
    }

    private void addBombeUI()
    {
        foreach (Transform child in gameObject.GetComponent<PlayerHealth>().playerBombeShow.transform)
        {
            if (child.GetComponent<Renderer>().material.color == Color.gray)
            {
                bombeShowStock = child.GetComponent<Renderer>();
                bombeShowStock.material = elec;
                bombeShowStock.transform.GetChild(0).gameObject.SetActive(true);
                return;
            }
        }

    }

    private void removeBombeUI()
    {
        foreach (Transform child in gameObject.GetComponent<PlayerHealth>().playerBombeShow.transform)
        {
            if (child.GetComponent<Renderer>().material.color != Color.gray)
            {
                bombeShowStock = child.GetComponent<Renderer>();
                bombeShowStock.material.color = Color.gray;
                bombeShowStock.transform.GetChild(0).gameObject.SetActive(false);
                return;
            }
        }
    }

    public int GetBombNumber()
    {
        return bombeStock.childCount;
    }
}
