using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizOne : MonoBehaviour
{
    // Privates  - - - - - - - - - - - - 

    int currentNumber;
    public int tryCount;
    float timer;
    string tempNumber;
    string previousNumber;
    bool soundHasPlayed;
    List<GameObject> digits = new List<GameObject>();

    // Publics - - - - - - - - - - - - -

    [Header("Materials")]
    public Material neutralDigitMat;
    public Material highlightDigitMat;
    public Material wrongDigitMat;
    public Material correctDigitMat;

    [Header("QuizSettings")]
    [Tooltip("Darf keine zwei gleichen Ziffern hintereinander beinhalten.")]
    [Range(0,9999)]
    public int targetNumber;
    public bool quizOneDone;

    // Temporär
    public TextMeshProUGUI infoText;

    private void Start()
    {
        tempNumber = "";
    }

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
                quizOneDone = true;
             
                //Temporär
                infoText.text = "Gut gemacht, Rätsel Eins erfolgreich gelöst!";

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
                    tryCount += 1;
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

        if(tryCount >= 1 && !quizOneDone)
        {
            // Temporär
            infoText.text = "Ich merke schon, Mathe war nicht dein Lieblingsfach. Die Lösung für dieses Rätsel ist 2318";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Digit") && other.gameObject.GetComponent<Digit>().digit.ToString() != previousNumber && timer == 0 && !quizOneDone)
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
