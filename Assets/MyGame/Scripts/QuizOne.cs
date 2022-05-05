using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizOne : MonoBehaviour
{
    int currentNumber;
    float timer;
    string tempNumber;
    bool puzzleOneCorrect;
    bool puzzleOneWrong;
    bool clickingIsAllowed;
    List<GameObject> digits = new List<GameObject>();

    [Header("Materials")]
    public Material neutralDigitMat;
    public Material highlightDigitMat;
    public Material wrongDigitMat;
    public Material correctDigitMat;

    [Header("Primitives")]
    public int targetNumber;

    private void Update()
    {
        try
        {
            currentNumber = Int32.Parse(tempNumber);

            if (tempNumber.Length >= 4)
            {
                clickingIsAllowed = false;

                if (currentNumber == targetNumber)
                {
                    puzzleOneCorrect = true;
                    foreach (GameObject i in digits)
                    {
                        i.GetComponent<Renderer>().material = correctDigitMat;
                        i.transform.GetChild(0).GetComponent<Renderer>().material = correctDigitMat;
                    }
                }
                else
                {
                    timer += Time.deltaTime;

                    foreach (GameObject i in digits)
                    {
                        i.GetComponent<Renderer>().material = wrongDigitMat;
                        i.transform.GetChild(0).GetComponent<Renderer>().material = wrongDigitMat;
                    }

                    if (timer >= 2)
                    {
                        foreach (GameObject i in digits)
                        {
                            i.GetComponent<Renderer>().material = neutralDigitMat;
                            i.transform.GetChild(0).GetComponent<Renderer>().material = neutralDigitMat;
                        }

                        digits.Clear();
                        timer = 0;
                        tempNumber = "";
                    }
                }
            }
            else
            {
                clickingIsAllowed = true;
            }
        }
        catch
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Digit" && clickingIsAllowed)
        {
            tempNumber += other.gameObject.GetComponent<Digit>().digit.ToString();
            digits.Add(other.gameObject);
            other.gameObject.GetComponent<Renderer>().material = highlightDigitMat;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = highlightDigitMat;
            if (puzzleOneCorrect)
            {
                AudioManager.instance.Play("correctSound");
            }
            else if(puzzleOneWrong)
            {
                AudioManager.instance.Play("wrongSound");
            }
            else
            {
                AudioManager.instance.Play("clickSound");
            }
        }
    }
}
