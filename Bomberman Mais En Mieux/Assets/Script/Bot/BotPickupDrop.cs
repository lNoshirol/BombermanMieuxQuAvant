using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotPickup : MonoBehaviour
{
    [SerializeField] Transform bombeStock;
    private GameObject bombe;
    [SerializeField] BotBRAIN botFindBombe;

    [SerializeField] TextMeshProUGUI bombeUI;

    public event Action<GameObject> OndropBomb;


    // Start is called before the first frame update
    void Start()
    {
        OndropBomb += GetComponent<BotBRAIN>().ABombHasBeenPlanted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false;
            bombe.transform.position = bombeStock.position;
            bombe.transform.SetParent(bombeStock);
            bombe.GetComponent<Collider>().isTrigger = false;
            Debug.Log("Attraper");
            botFindBombe.UseBigBrainIA();
            updateBombeUI();
        }
        else if (other.gameObject.tag == "Bombe" && bombeStock.childCount >= 3)
        {
            Debug.Log("Plus de place dans le sac mon coco");
        }
    }

    private void updateBombeUI()
    {
        bombeUI.text = ($"{bombeStock.childCount}/3");
    }

    public void BotDropBomb()
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
            OndropBomb?.Invoke(bombeToDrop.gameObject);
        }
    }

}
