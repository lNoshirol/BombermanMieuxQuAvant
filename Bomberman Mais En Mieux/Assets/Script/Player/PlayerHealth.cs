using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float pv;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] int damageMultiplier;
    Collider playerCollider;
    Renderer playerRenderer;
    Color baseColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius")
        {
            takeDamage();
            updateHealthUI();
            Debug.Log($"Take damage, health={pv}");

        }
    }

    private void Start()
    {
        playerCollider = gameObject.GetComponent<Collider>();
        playerRenderer = gameObject.GetComponent<Renderer>();
        baseColor = playerRenderer.material.color;
    }
    private void takeDamage()
    {
        pv=pv-damageMultiplier;
        StartCoroutine(Invicibility());
    }

    private void updateHealthUI()
    {
        healthUI.text = ($"{pv}pv");
    }

    private IEnumerator Invicibility()
    {
        playerRenderer.material.color = Color.red;
        Debug.Log("switchcolor");
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = baseColor;
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        playerRenderer.material.color = baseColor;
    }
}
