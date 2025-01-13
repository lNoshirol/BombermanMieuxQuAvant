using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotBRAIN : MonoBehaviour
{
    public List<GameObject> AllBombesList = new List<GameObject>();
    public GameObject NearestBombe;
    float distance;
    float nearestDistance;
    GameObject player;
    public NavMeshAgent navigation;
    [SerializeField] Transform bombeStock;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject bombe in GameObject.FindGameObjectsWithTag("Bombe"))
        {
            AllBombesList.Add(bombe);
        }
        UseBigBrainIA();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(bombeStock.childCount == 3)
        {
            FindPlayer();
        }
        else
        {
            BOTFindNearestBombe();
        }
    }
    public void UseBigBrainIA()
    {
        if (bombeStock.childCount < 3)
        {
            BOTFindNearestBombe();
        }
    }


    public void BOTFindNearestBombe()
    {
        Debug.Log("Je cherche");
        nearestDistance = float.MaxValue;
        for (int i = 0; i < AllBombesList.Count; i++) { 
        distance = Vector2.Distance(this.transform.position, AllBombesList[i].transform.position);

            if (distance < nearestDistance && AllBombesList[i].GetComponent<Bombe>().canBeGrab == true) {
                NearestBombe = AllBombesList[i];
                nearestDistance = distance;
                Debug.Log("BombeFind");
            }
        }
        navigation.destination = NearestBombe.transform.position;
        //RemoveBombFromList();


    }

    public void RemoveBombFromList()
    {
        AllBombesList.Remove(NearestBombe);
    }

    public void FindPlayer()
    {
        Debug.Log("J'attaque le joueur");
        navigation.destination = player.transform.position;
    }
}