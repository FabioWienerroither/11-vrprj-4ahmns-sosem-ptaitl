using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    [Range(0,9)]
    public int digit;
    public Material neutralDigitMat;

    private void Start()
    {
        GetComponent<Renderer>().material = neutralDigitMat;
        transform.GetChild(0).GetComponent<Renderer>().material = neutralDigitMat;
    }
}
