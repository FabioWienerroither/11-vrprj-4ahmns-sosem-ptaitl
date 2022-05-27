using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    #region Fields
    [Range(0,9)]
    public int digit;
    public Material neutralDigitMat;
    #endregion

    void Start()
    {
        // Am Anfang sollen die Zahlen alle neutral gefärbt sein

        GetComponent<Renderer>().material = neutralDigitMat;
        transform.GetChild(0).GetComponent<Renderer>().material = neutralDigitMat;
    }
}
