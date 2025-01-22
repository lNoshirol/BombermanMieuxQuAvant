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

    public event Action<GameObject> OnDropBomb;
    public event Action<GameObject> OnPickBomb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            other.transform.position = bombeStock.position;
            other.gameObject.transform.SetParent(bombeStock);
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false;
            bombe.GetComponent<BoxCollider>().enabled = false;
            


            OnPickBomb?.Invoke(bombe);

            updateBombeUI();
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
                updateBombeUI();
                OnDropBomb?.Invoke(bombeToDrop.gameObject);
            }
        }
    }

    private void updateBombeUI()
    {
        bombeUI.text = ($"{bombeStock.childCount}/3");
    }

    public int GetBombNumber()
    {
        return bombeStock.childCount;
    }
}
