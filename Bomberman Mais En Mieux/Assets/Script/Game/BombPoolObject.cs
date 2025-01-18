using System;
using System.Collections;
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
    public List<GameObject> bombInUse;

    public event Action<GameObject> onBombSpawn;

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
            newBomb.GetComponent<Bombe>().OnKaboom += Oskour;
            newBomb.name = $"bombe {i}";
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
        Transform spawnPoint = bombSpawnPointList[UnityEngine.Random.Range(0, bombSpawnPointList.Count)];
        while (spawnPoint.childCount != 0)
        {
            spawnPoint = bombSpawnPointList[UnityEngine.Random.Range(0, bombSpawnPointList.Count)];
        }
        theBomb.transform.position = spawnPoint.position;
        theBomb.transform.parent = spawnPoint;
        onBombSpawn?.Invoke(theBomb);
        theBomb.SetActive(true);
    }

    public void ResetBomb(GameObject theBomb)
    {
        theBomb.SetActive(false);
        theBomb.transform.position = Vector3.zero;
        theBomb.transform.rotation = Quaternion.identity;
        theBomb.transform.parent = transform;
        theBomb.GetComponent<Bombe>().ResetOwnValue();
        bombInUse.Remove(theBomb);
        bombInPool.Add(theBomb);
    }

    public void Oskour(GameObject theBomb)
    {
        Debug.Log("ET LA C BON");
        StartCoroutine(BombExplode(theBomb));
    }

    IEnumerator BombExplode(GameObject theBomb)
    {
        ResetBomb(theBomb);
        //await Task.Delay(1500);
        yield return new WaitForSeconds(1.5f);
        SpawnBomb(bombInPool[0]);
    }
}