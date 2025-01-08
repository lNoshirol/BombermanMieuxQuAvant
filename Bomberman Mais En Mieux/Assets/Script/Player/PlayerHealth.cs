using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float pv;
    [SerializeField] TextMeshProUGUI healthUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius")
        {
            takeDamage();
            updateHealthUI();
            Debug.Log($"Take damage, health={pv}");

        }
    }


    private void takeDamage()
    {
        pv--;
    }

    private void updateHealthUI()
    {
        healthUI.text = ($"{pv}pv");
    }
}
