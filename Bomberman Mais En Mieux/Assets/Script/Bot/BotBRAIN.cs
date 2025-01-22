using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotBRAIN : MonoBehaviour
{
    [Foldout("List")]
    public List<GameObject> AllBombesList;
    [Foldout("List")]
    public List<GameObject> thingsToRunAway = new();

    [Foldout("GameObject")]
    public GameObject NearestBombe;
    [Foldout("GameObject")]
    public GameObject player;

    [Foldout("NavMesh")]
    public NavMeshAgent navigation;

    [Foldout("Int")]
    [MinValue(0), MaxValue(10)]
    public float dangerZone;


    [Foldout("Transform")]
    [SerializeField] Transform bombeStock;

    [Foldout("Debug")]
    [SerializeField] float distance;

    [Foldout("Debug")]
    [SerializeField] float nearestDistance;

    [Foldout("Debug")]
    [SerializeField] Vector3 direction;

    [Foldout("Debug")]
    [SerializeField] Vector3 FleePosition;

    [Foldout("Debug")]
    [SerializeField] GameObject bombToCatch;

    [Foldout("Debug")]
    [SerializeField] bool suicideMod;



    // Start is called before the first frame update
    void Start()
    {
        BombPoolObject.instance.onBombSpawn += BombSpawn;
        GetComponent<BotPickup>().OnBotDropBomb += ABombHasBeenPlanted;
        GetComponent<BotPickup>().OnBotPickBomb += BombHasBeenTake;
        WinManager.instance.OnPlayerJoin(gameObject);
    }

    private void Update()
    {
        suicideMod = SuicidalMod();
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
        if (AllBombesList.Count > 0)
        {
            nearestDistance = Vector2.Distance(this.transform.position, AllBombesList[0].transform.position);

            for (int i = 0; i < AllBombesList.Count; i++)
            {
                //if (i > AllBombesList.Count - 1) i = 0;

                distance = Vector2.Distance(this.transform.position, AllBombesList[i].transform.position);

                if (distance <= nearestDistance && AllBombesList.Contains(AllBombesList[i]))
                {
                    NearestBombe = AllBombesList[i];
                    bombToCatch = NearestBombe;
                    nearestDistance = distance;
                }
            }
            navigation.destination = NearestBombe.transform.position;
        }
        else
        {
        }



    }

    public void FindPlayer()
    {
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
        direction = (transform.position - ClosestMenace().transform.position).normalized;

        FleePosition = transform.position + direction * 2;

        /*NavMeshHit hit;

        if (NavMesh.SamplePosition(fleePosition, out hit, 2, NavMesh.AllAreas))
        {
            fleePosition = hit.position;
        }*/

        navigation.SetDestination(FleePosition);
    }

    public void AttackPlayer()
    {
        navigation.SetDestination(player.transform.position);
        if (DistanceBetweenBotAndTarget(player) < 2)
        {
            GetComponent<BotPickup>().BotDropBomb();
        }
    }

    public void BombHasBeenTake(GameObject bomb)
    {
        AllBombesList.Remove(bomb);
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

    public void BombSpawn(GameObject bomb)
    {
        AllBombesList.Add(bomb);
    }

    public bool SuicidalMod()
    {
        return (AllBombesList.Count == 0 && GetBombNumber() < player.GetComponent<PlayerPickDrop>().GetBombNumber());
    }
}
