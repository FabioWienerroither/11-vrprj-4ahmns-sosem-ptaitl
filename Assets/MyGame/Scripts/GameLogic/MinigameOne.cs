using System;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOne : MonoBehaviour
{
    #region Privates
    int currentNumber;
    float timer;
    string tempNumber;
    bool soundHasPlayed;
    bool animationWasSet;
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
    [Header("Referenten")]
    public Animator speechbubbleAnimator;
    public GameObject bunny;
    public Camera mainCamera;
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
                // Wenn das Egebniss richtig ist, soll der Checkpoint angepasst

                Checkpoints.playerHasDoneMinigameOne = true;

                // Der 'correctSound' soll nur einmal abgespielt werden

                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play(AudioManager.instance.correctSound);
                    soundHasPlayed = true;
                }

                // Der neue Text auf der Sprechblase soll erst sichtbar werden, wenn die Camera den Hasen sieht

                if (!animationWasSet && IsVisible(mainCamera, bunny))
                {
                    speechbubbleAnimator.ResetTrigger("EinblendenOhneZeit");
                    speechbubbleAnimator.SetTrigger("EinblendenMitZeit");
                    animationWasSet = true;
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
                // Checkpoint ändern, damit ein Hinweis in der Sprechblase angezeigt wird

                Checkpoints.playerHasFailedMinigameOne = true;

                // Der 'wrongSound' soll nur einmal abgespielt werden

                if (!soundHasPlayed)
                {
                    AudioManager.instance.Play(AudioManager.instance.wrongSound);
                    soundHasPlayed = true;
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
                    tempNumber = "";
                }
            }
        }
        else
        {
            // Reset des 'soundHasPlayed' bool nach jedem Versuch

            soundHasPlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collision nur akzeptieren, wenn:

        bool objectIsAllowed = (other.CompareTag("Digit") && !digits.Contains(other.gameObject));
        bool timingIsRight = (timer == 0);
        bool checkpointsAreRight = (!Checkpoints.playerHasDoneMinigameOne && Checkpoints.playerHasReachedMinigameOne);

        if (objectIsAllowed && timingIsRight && checkpointsAreRight)
        {
            // Digit zum zu parsenden String hinzufügen

            tempNumber += other.gameObject.GetComponent<Digit>().digit.ToString();

            // gedrückte Zahl zur Liste, die später alle gedrückten Zahlen grün bzw. rot färbt

            digits.Add(other.gameObject);

            // Eingabe durch Einfärben markieren

            other.gameObject.GetComponent<Renderer>().material = highlightDigitMat;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material = highlightDigitMat;

            // 'clickSound' abspielen

            AudioManager.instance.Play(AudioManager.instance.clickSound);
        }
    }

    // Je nachdem, ob ein Object von einer Kamera gesehen wird oder nicht, gibt die Funktion einen entsprechenden bool zurück

    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
}
