using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float pv;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius")
        {
            pv--;
            Debug.Log($"Take damage, health={pv}");
        }
    }

}
