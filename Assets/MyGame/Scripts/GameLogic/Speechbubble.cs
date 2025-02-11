using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speechbubble : MonoBehaviour
{
    #region Fields
    public Animator bunnyAnimator;
    #endregion

    void Update()
    {
        // Texte innerhalb der Sprechblase entsprechend der aktuellen Checkpoints

        if (Checkpoints.playerHasReachedExit)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Das war's! Wir haben es erfolgfreich aus dem Labyrinth geschafft.";
        }

        else if (Checkpoints.playerHasDoneMinigameOne)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Super, du hast das Zahlenrätsel richtig gelöst! Folge mir bis zum Ausgang.";
        }

        else if (Checkpoints.playerHasFailedMinigameOne && Checkpoints.playerHasReachedMinigameOne)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Ich merke schon, Mathe ist nicht deine Stärke... \n Das Ergebnis ist »2318«";
        }

        else if (Checkpoints.playerHasReachedMinigameOne)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Ostersonntag wird jedes Jahr am ersten Sonntag nach dem Vollmond im Frühling gefeiert. Heuer ist das der 17/04/2022. Um dieses Rätsel zu lösen, bilde die Quersumme aus diesem Datum, addiere die Zahl 2300 und gibt das Ergebnis durch Berühren der Ziffern mit der rechten Hand in das nebenstehende Zahlenfeld ein.";
        }

        else if (Checkpoints.playerHasReachedBunny)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Mein Name ist Hase, ich weiß von nichts... \n Naja, das stimmt nicht ganz, ich weiß nämlich von einem Zahlenrätsel, das im Labyrinth versteckt ist. Folge mir und versuche es zu lösen!";
        }
    }

    // Funktion wird in der Sprechblasenanimation getriggert

    public void ToNextPosition()
    {
        bunnyAnimator.SetLayerWeight(1, 1);

        // Animation entsprechend der aktuellen Checkpoints triggern

        if (Checkpoints.playerHasReachedBunny && !Checkpoints.playerHasDoneMinigameOne)
        {
            bunnyAnimator.SetTrigger("ToMinigameOne");

        }
        else if (Checkpoints.playerHasReachedBunny && Checkpoints.playerHasDoneMinigameOne)
        {
            bunnyAnimator.SetTrigger("ToExit");
        }
    }
}
