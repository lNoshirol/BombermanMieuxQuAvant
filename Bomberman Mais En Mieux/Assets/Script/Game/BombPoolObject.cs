using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BombPoolObject : MonoBehaviour
{
    public static BombPoolObject instance;

    [SerializeField] private GameObject bombPrefab;
    private int maxBombOnMap = 5;

    [SerializeField] List<Transform> bombSpawnPointList;

    [SerializeField] List<GameObject> bombInPool;
    [SerializeField] List<GameObject> bombInUse;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        for (int i = 0; i < maxBombOnMap+3; i++) //we create more bombs just in case
        {
            GameObject newBomb = Instantiate(bombPrefab);
            ResetBomb(newBomb);
        }

        for (int i = 0; i < maxBombOnMap; i++)
        {
            SpawnBomb(bombInPool[0]);
            await Task.Delay(1000);
        }
    }

    public void SpawnBomb(GameObject theBomb)
    {
        bombInPool.Remove(theBomb);
        bombInUse.Add(theBomb);
        Transform spawnPoint = bombSpawnPointList[Random.Range(0, bombSpawnPointList.Count)];
        while (spawnPoint.childCount != 0)
        {
            spawnPoint = bombSpawnPointList[Random.Range(0, bombSpawnPointList.Count)];
        }
        theBomb.transform.position = spawnPoint.position;
        theBomb.transform.parent = spawnPoint;
        theBomb.SetActive(true);
    }

    public void ResetBomb(GameObject theBomb)
    {
        theBomb.SetActive(false);
        theBomb.transform.position = Vector3.zero;
        theBomb.transform.rotation = Quaternion.identity;
        theBomb.transform.parent = transform;
        bombInPool.Add(theBomb);
    }
}