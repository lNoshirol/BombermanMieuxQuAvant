using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class PlayerPickDrop : MonoBehaviour
{
    [SerializeField] Transform bombeStock;
    private PlayerActions _playerInput;
    private GameObject bombe;

    [SerializeField] TextMeshProUGUI bombeUI;
    [SerializeField] BotBRAIN botBRAIN;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            other.transform.position = bombeStock.position;
            other.gameObject.transform.SetParent(bombeStock);
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false; ;
            Debug.Log("Attraper");
            updateBombeUI();
            if(!botBRAIN == false)
            botBRAIN.UseBigBrainIA();
        }
        else if(other.gameObject.tag == "Bombe" && bombeStock.childCount >= 3)
        {
            Debug.Log("Plus de place dans le sac mon coco");
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
            if (bombeStock.childCount < 1)
            {
                Debug.Log("Pas de bombe à poser");
            }
            else
            {
                    Debug.Log("Je drop");
                    Transform bombeToDrop = bombeStock.GetChild(0);
                    Debug.Log(bombeToDrop);
                    bombeToDrop.parent = null;
                    bombeToDrop.GetComponent<SphereCollider>().enabled = false;
                    bombeToDrop.GetComponent<Bombe>().StartExplosion();
                    updateBombeUI();
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
