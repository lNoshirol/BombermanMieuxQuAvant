using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    [SerializeField] GameObject exploFather;
    public bool wallHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall"){
            wallHit = true;
        }
    }

    private void Start()
    {
        if (exploFather.GetComponent<WallDetection>().wallHit) { 
            gameObject.SetActive(false);
            wallHit = true;
        }
    }


}
