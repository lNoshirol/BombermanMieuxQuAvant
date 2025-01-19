using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Bombe : MonoBehaviour
{
    [SerializeField] List<Transform> radius = new List<Transform>();
    public bool canBeGrab = true;
    Renderer _renderer;
    private Color _baseColor;

    public event Action<GameObject> OnKaboom;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        radius.Add(child);
        _renderer = gameObject.GetComponent<Renderer>();
        _baseColor = _renderer.material.color;

        //StartCoroutine(StartExplosion());
    }

    public void StartExplosion()
    {
        StartCoroutine(WaitAndExplosion());
    }

    private IEnumerator WaitAndExplosion()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        for (int i = 3; i > 0; i--)
        {
            Debug.Log($"Explosion dans {i}...");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Boom");
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        _renderer.enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.10f);
        }
        yield return new WaitForSeconds(0.5f);

        OnKaboom?.Invoke(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius" && other.transform.root.gameObject.GetComponent<Bombe>().canBeGrab)
        {
            Debug.Log("HAHAIJD");
            StartCoroutine(Explosion());
        }
    }

    public void ResetOwnValue()
    {
        canBeGrab = true;
        _renderer.enabled = true;
        _renderer.material.color = _baseColor;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GetComponent<SphereCollider>().enabled = true;
    }
}
