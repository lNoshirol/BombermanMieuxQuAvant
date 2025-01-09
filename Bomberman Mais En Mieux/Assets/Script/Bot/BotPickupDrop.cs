using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BotPickup : MonoBehaviour
{
    [SerializeField] Transform bombeStock;
    private GameObject bombe;
    [SerializeField] BotFindBombe botFindBombe;

    [SerializeField] TextMeshProUGUI bombeUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe" && bombeStock.childCount < 3)
        {
            other.transform.position = bombeStock.position;
            other.gameObject.transform.SetParent(bombeStock);
            bombe = other.gameObject;
            Debug.Log("Attraper");
            botFindBombe.FindNearestBombe();

        }
        else if (other.gameObject.tag == "Bombe" && bombeStock.childCount >= 3)
        {
            Debug.Log("Plus de place dans le sac mon coco");
        }
    }
}
