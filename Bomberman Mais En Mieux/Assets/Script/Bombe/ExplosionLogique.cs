using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogique : MonoBehaviour
{
    [SerializeField] List<Transform> direction = new List<Transform> ();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            direction.Add(child);
        StartCoroutine(StartExplosion());
    }

    private IEnumerator StartExplosion()
    {
        for (int i = 3; i > 0; i--)
        {
            Debug.Log($"Explosion dans {i}...");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Boom");
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            child.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
    }

}
