using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizOne : MonoBehaviour
{
    int currentNumber;
    float timer;
    string previousNumber;
    bool soundHasPlayed;
    List<GameObject> digits = new List<GameObject>();

    [Header("Materials")]
    public Material neutralDigitMat;
    public Material highlightDigitMat;
    public Material wrongDigitMat;
    public Material correctDigitMat;

    [Header("Primitives")]
    public int targetNumber;
    [HideInInspector]
    public string tempNumber;

    private void Update()
    {
        try
        {
            currentNumber = Int32.Parse(tempNumber);
        }
        catch
        {
            currentNumber = 0;
        }

        if (tempNumber.Length >= 4)
        {
            if (currentNumber == targetNumber)
            {
                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play("correctSound");
                    soundHasPlayed = true;
                }

                foreach (GameObject i in digits)
                {
                    i.GetComponent<Renderer>().material = correctDigitMat;
                    i.transform.GetChild(0).GetComponent<Renderer>().material = correctDigitMat;
                }
            }
            else
            {
                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play("wrongSound");
                    soundHasPlayed = true;
                }

                timer += Time.deltaTime;

                foreach (GameObject i in digits)
                {
                    i.GetComponent<Renderer>().material = wrongDigitMat;
                    i.transform.GetChild(0).GetComponent<Renderer>().material = wrongDigitMat;
                }

                if (timer >= 1)
                {
                    foreach (GameObject i in digits)
                    {
                        i.GetComponent<Renderer>().material = neutralDigitMat;
                        i.transform.GetChild(0).GetComponent<Renderer>().material = neutralDigitMat;
                    }

                    digits.Clear();
                    timer = 0;
                    previousNumber = null;
                    tempNumber = "";
                }
            }
        }
        else
        {
            soundHasPlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Digit") && other.gameObject.GetComponent<Digit>().digit.ToString() != previousNumber && timer == 0)
        {
            tempNumber += other.gameObject.GetComponent<Digit>().digit.ToString();
            previousNumber = other.gameObject.GetComponent<Digit>().digit.ToString();
            digits.Add(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material = highlightDigitMat;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = highlightDigitMat;
            AudioManager.instance.Play("clickSound");
        }
    }
}
