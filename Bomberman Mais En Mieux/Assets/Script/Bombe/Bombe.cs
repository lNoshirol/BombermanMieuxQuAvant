using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    [SerializeField] GameObject bombeExploRadius;
    public int bombeDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartExplosion()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        for(int i = 3;  i > 0; i--)
        {
            Debug.Log($"Explosion dans {i}...");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Boom");
        bombeExploRadius.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
