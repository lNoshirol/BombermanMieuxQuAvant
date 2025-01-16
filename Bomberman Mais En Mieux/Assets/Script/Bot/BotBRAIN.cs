using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotBRAIN : MonoBehaviour
{
    [Header("Public")]
    [Foldout("List")]
    public List<GameObject> AllBombesList = new();
    [Foldout("List")]
    public List<GameObject> thingsToRunAway = new();

    [Foldout("GameObject")]
    public GameObject NearestBombe;
    [Foldout("GameObject")]
    public GameObject player;

    [Foldout("NavMesh")]
    public NavMeshAgent navigation;

    [Foldout("Int")]
    public float dangerZone;


    [Header("Private")]
    [Foldout("Transform")]
    [SerializeField] Transform bombeStock;

    [Foldout("Debug")]
    [SerializeField] float distance;

    [Foldout("Debug")]
    [SerializeField] float nearestDistance;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject bombe in GameObject.FindGameObjectsWithTag("Bombe"))
        {
            AllBombesList.Add(bombe);
        }
    }

    private void Update()
    {
        

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
        nearestDistance = Vector2.Distance(this.transform.position, BombPoolObject.instance.bombInUse[0].transform.position);

        for (int i = 0; i < BombPoolObject.instance.bombInUse.Count; i++) 
        {
            distance = Vector2.Distance(this.transform.position, BombPoolObject.instance.bombInUse[i].transform.position);

            if (distance <= nearestDistance /*&& BombPoolObject.instance.bombInUse[i].GetComponent<Bombe>().canBeGrab == true*/) 
            {
                NearestBombe = BombPoolObject.instance.bombInUse[i];
                nearestDistance = distance;
                Debug.Log("BombeFind");
            }
        }
        navigation.destination = NearestBombe.transform.position;
        //RemoveBombFromList();


    }

    public void FindPlayer()
    {
        Debug.Log("J'attaque le joueur");
        navigation.destination = player.transform.position;
    }

    public int GetBombNumber()
    {
        return bombeStock.childCount;
    }

    public GameObject ClosestMenace()
    {
        GameObject closestDanger = thingsToRunAway[0];
        float distanceFromClosestDanger = DistanceBetweenBotAndTarget(closestDanger);
        float tempoDistance = distanceFromClosestDanger;

        foreach (GameObject go in thingsToRunAway)
        {
            tempoDistance = DistanceBetweenBotAndTarget(go);
            if (tempoDistance < distanceFromClosestDanger)
            {
                closestDanger = go;
                distanceFromClosestDanger = tempoDistance;
            }
        }

        return closestDanger;
    }

    public float DistanceBetweenBotAndTarget(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void FleeDanger()
    {
        Vector3 direction = (transform.position - ClosestMenace().transform.position).normalized;

        Vector3 fleePosition = transform.position + direction * 2;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(fleePosition, out hit, 2, NavMesh.AllAreas))
        {
            fleePosition = hit.position;
        }

        navigation.SetDestination(fleePosition);
        Debug.Log($"JE COURS VERS {fleePosition}");
    }

    public void AttackPlayer()
    {
        navigation.SetDestination(player.transform.position);
        if (DistanceBetweenBotAndTarget(player) < 2)
        {
            GetComponent<BotPickup>().BotDropBomb();
        }
    }

    public void ABombHasBeenPlanted(GameObject bomb)
    {
        thingsToRunAway.Add(bomb);
        bomb.GetComponent<Bombe>().OnKaboom += ABombHasExplod;
    }

    public void ABombHasExplod(GameObject bomb)
    {
        thingsToRunAway.Remove(bomb);
        bomb.GetComponent<Bombe>().OnKaboom -= ABombHasExplod;
    }

    
}
