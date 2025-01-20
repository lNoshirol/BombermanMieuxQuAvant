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

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            bombe = other.gameObject;
            bombe.GetComponent<Bombe>().canBeGrab = false;
            bombe.transform.position = bombeStock.position;
            bombe.transform.SetParent(bombeStock);
            bombe.GetComponent<SphereCollider>().enabled = false;
            Debug.Log("Attraper");

            OnBotPickBomb?.Invoke(bombe);

            //botFindBombe.UseBigBrainIA();
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
            OnBotDropBomb?.Invoke(bombeToDrop.gameObject);
            //bombeToDrop.position = new Vector3(Mathf.RoundToInt(transform.position.x) + 0.5f, transform.position.y, Mathf.RoundToInt(transform.position.z) + 0.5f);
        }
    }

}
