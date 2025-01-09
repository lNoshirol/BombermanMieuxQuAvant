using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFindBombe : MonoBehaviour
{
    public List<GameObject> AllBombesList = new List<GameObject>();
    public GameObject NearestBombe;
    float distance;
    float nearestDistance = 100000;

    public NavMeshAgent navigation;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject bombe in GameObject.FindGameObjectsWithTag("Bombe"))
        {
            AllBombesList.Add(bombe);
        }
        FindNearestBombe();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindNearestBombe()
    {
        for (int i = 0; i < AllBombesList.Count; i++) { 
        distance = Vector3.Distance(this.transform.position, AllBombesList[i].transform.position);

            if (distance < nearestDistance) { 
            NearestBombe = AllBombesList[i];
            nearestDistance = distance;
            AllBombesList.Remove(NearestBombe);
            }
        }

        navigation.destination = NearestBombe.transform.position;
        
        Debug.Log("BombeFind");
    }
}