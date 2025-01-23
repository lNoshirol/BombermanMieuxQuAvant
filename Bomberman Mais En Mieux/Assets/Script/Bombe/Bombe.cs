using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.VFX;

public class Bombe : MonoBehaviour
{
    [SerializeField] List<Transform> radius = new List<Transform>();
    public bool canBeGrab = true;
    Renderer _renderer;
    private Color _baseColor;

    public event Action<GameObject> OnKaboom;
    [SerializeField] GameObject bombeRadius;

    [SerializeField] Animator bombeAnime;

    [SerializeField] VisualEffect eletricity;
    [SerializeField] VisualEffect beam;
    [SerializeField] Material elec;

    [SerializeField] AudioSource bombePickup;


    [SerializeField] float powerAmount;




    void Awake()
    {
        foreach (Transform child in transform)
        radius.Add(child);
        _renderer = gameObject.GetComponent<Renderer>();
        _baseColor = _renderer.material.color;

    }

    private void Update()
    {
        eletricity.SetVector4("ElecColor", elec.color);
        beam.SetVector4("CylinderColor", elec.color);
    }
    public void StartExplosion()
    {
        StartCoroutine(WaitAndExplosion());
    }

    private IEnumerator WaitAndExplosion()
    {
        transform.rotation = Quaternion.identity;
        bombeAnime.Play("BombeExplode");
        yield return new WaitForSeconds(1f);
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.2f);
        bombeRadius.SetActive(true);
        yield return new WaitForSeconds(1f);
        OnKaboom?.Invoke(gameObject);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius" && other.transform.root.gameObject.GetComponent<Bombe>().canBeGrab)
        {
            StartCoroutine(Explosion());
        }
        bombePickup.Play();
    }

    public void ResetOwnValue()
    {
        canBeGrab = true;
        _renderer.enabled = true;
        _renderer.material.color = _baseColor;
        bombeRadius.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnEnable()
    {
        bombeAnime.Play("BombeIdle");
    }

        
}
