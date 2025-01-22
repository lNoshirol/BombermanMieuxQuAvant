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

    public event Action<GameObject> OnBotDropBomb;
    public event Action<GameObject> OnBotPickBomb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false;
            bombe.transform.position = bombeStock.position;
            bombe.transform.SetParent(bombeStock);
            bombe.GetComponent<BoxCollider>().enabled = false;

            OnBotPickBomb?.Invoke(bombe);

            updateBombeUI();
        }
    }

    private void updateBombeUI()
    {
        bombeUI.text = ($"{bombeStock.childCount}/3");
    }

    public void BotDropBomb()
    {
        if (bombeStock.childCount >= 1)
        {
            Transform bombeToDrop = bombeStock.GetChild(0);
            bombeToDrop.parent = null;
            bombeToDrop.GetComponent<BoxCollider>().enabled = false;
            bombeToDrop.GetComponent<Bombe>().StartExplosion();
            updateBombeUI();
            OnBotDropBomb?.Invoke(bombeToDrop.gameObject);
        }
    }

}
