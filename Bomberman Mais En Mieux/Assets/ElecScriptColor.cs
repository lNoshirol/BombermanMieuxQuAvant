using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecScriptColor : MonoBehaviour
{
    [SerializeField] Material elec;
    // Start is called before the first frame update
    void Start()
    {
        elec.color = Color.cyan;
    }

}
