using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
    public Material mat;
    public float noiseAmount;
    public float glitchStrenght;
    public float scanLinesStrenght;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("NoiseAmount", noiseAmount);
        mat.SetFloat("GlitchStrenght", glitchStrenght);
        mat.SetFloat("ScanLinesStrenght", scanLinesStrenght);
    }
}
