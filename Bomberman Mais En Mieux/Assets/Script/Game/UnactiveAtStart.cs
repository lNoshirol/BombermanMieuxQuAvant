using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
