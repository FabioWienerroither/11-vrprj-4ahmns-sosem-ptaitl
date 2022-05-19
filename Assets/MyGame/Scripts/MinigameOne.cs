using System;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOne : MonoBehaviour
{
    #region Privates
    int currentNumber;
    int tryCount;
    float timer;
    string tempNumber;
    string previousNumber;
    bool soundHasPlayed;
    List<GameObject> digits = new List<GameObject>();
    #endregion

    #region Fields
    [Header("Materials")]
    public Material neutralDigitMat;
    public Material highlightDigitMat;
    public Material wrongDigitMat;
    public Material correctDigitMat;
    [Header("QuizSettings")]
    [Tooltip("Darf keine zwei gleichen Ziffern hintereinander beinhalten und muss keiner als 9999 sein.")]
    public int targetNumber;
    [Range(0,10)]
    public int timeoutAfterTry;
    public static bool quizOneDone;
    #endregion

    private void Start()
    {
        // Leeren String setzten, sonst kann die Länge des Strinngs nicht ausgelesen werden

        tempNumber = "";
    }

    private void Update()
    {
        // Wenn keine Zahl gedrückt wird, kann der String nicht geparst werden und die akutelle Zahl soll 0 sein

        try
        {
            currentNumber = Int32.Parse(tempNumber);
        }
        catch
        {
            currentNumber = 0;
        }

        // Wenn vier Zahlen gderückt wurden soll die Kombination überprüft werden

        if (tempNumber.Length >= 4)
        {
            if (currentNumber == targetNumber)
            {
                quizOneDone = true;

                // Der 'correctSound' soll nur einmal abgespielt werden

                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play("correctSound");
                    soundHasPlayed = true;
                }

                // Alle gedrückten Zahlen sollen grün werden

                foreach (GameObject i in digits)
                {
                    i.GetComponent<Renderer>().material = correctDigitMat;
                    i.transform.GetChild(0).GetComponent<Renderer>().material = correctDigitMat;
                }
            }
            else
            {
                // Der 'wrongSound' soll nur einmal abgespielt werden

                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play("wrongSound");
                    soundHasPlayed = true;

                    // Der Try Counter soll nur einmal nach oben zählen

                    tryCount += 1;
                }

                // Der Timer regelt den Timeout nach einem Eingabeversuch

                timer += Time.deltaTime;

                // Alle gedrückten Zahlen sollen rot werden

                foreach (GameObject i in digits)
                {
                    i.GetComponent<Renderer>().material = wrongDigitMat;
                    i.transform.GetChild(0).GetComponent<Renderer>().material = wrongDigitMat;
                }

                // Nach dem Timeout soll das Zahlenfeld zurückgesetzt werden

                if (timer >= timeoutAfterTry)
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
            // Hinweis geben
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collision nur akzeptieren, wenn es sich um den 

        if (other.CompareTag("Digit") && other.gameObject.GetComponent<Digit>().digit.ToString() != previousNumber && timer == 0 && !quizOneDone)
        {
            // Digit zum zu parsenden String hinzufügen

            tempNumber += other.gameObject.GetComponent<Digit>().digit.ToString();

            // Um ungewollte Eingaben zu verhindern, sollen nicht zwei gleiche Zahlen hintereinander gedrückt werden können

            previousNumber = other.gameObject.GetComponent<Digit>().digit.ToString();

            // gedrückte Zahl zur Liste, die später alle gedrückten Zahlen grün bzw. rot färbt

            digits.Add(other.gameObject);

            // Eingabe durch Einfärben markieren

            other.gameObject.GetComponent<Renderer>().material = highlightDigitMat;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = highlightDigitMat;

            // 'clickSound' abspielen

            AudioManager.instance.Play("clickSound");
        }
    }
}
