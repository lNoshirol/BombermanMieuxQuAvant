using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int pv;
    [SerializeField] TextMeshProUGUI healthUI;
    public int damageMultiplier;
    Collider playerCollider;
    Renderer playerRenderer;
    Color baseColor;
    bool invincible = false;

    public event Action<int> OnDamageTakePv;
    public event Action<GameObject, int> OnDamageTakeGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius" && !invincible)
        {
            takeDamage();
            updateHealthUI();

        }
    }

    private void Start()
    {
        playerCollider = gameObject.GetComponent<Collider>();
        playerRenderer = gameObject.GetComponent<Renderer>();
        baseColor = playerRenderer.material.color;
    }
    public void takeDamage()
    {
        pv=pv-damageMultiplier;

        OnDamageTakePv?.Invoke(pv);
        OnDamageTakeGameObject?.Invoke(gameObject, pv);

        StartCoroutine(Invicibility());
    }

    private void updateHealthUI()
    {
        healthUI.text = ($"{pv}");
    }

    private IEnumerator Invicibility()
    {
        invincible = true;
        playerRenderer.material.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = baseColor;
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = baseColor;
        invincible = false;
    }
}
