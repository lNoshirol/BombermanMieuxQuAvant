using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombPoolObject : MonoBehaviour
{
    public static BombPoolObject instance;

    [SerializeField] private GameObject bombPrefab;
    private int maxBombOnMap = 5;

    [SerializeField] List<Transform> bombSpawnPointList;

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

    private void Start()
    {
        for (int i = 0; i < maxBombOnMap+3; i++) //we create more bombs just in case
        {
            GameObject newBomb = Instantiate(bombPrefab);
            ResetBomb(newBomb);
        }

        for (int i = 0;i < maxBombOnMap; i++)
        {
            //SpawnBomb();
            //soon
        }
    }

    public void SpawnBomb(GameObject theBomb)
    {
        theBomb.transform.position = bombSpawnPointList[Random.Range(0, bombSpawnPointList.Count)].position;
        theBomb.transform.parent = null;
        theBomb.SetActive(true);
    }

    public void ResetBomb(GameObject theBomb)
    {
        theBomb.SetActive(false);
        theBomb.transform.position = Vector3.zero;
        theBomb.transform.rotation = Quaternion.identity;
        theBomb.transform.parent = transform;
    }
}
