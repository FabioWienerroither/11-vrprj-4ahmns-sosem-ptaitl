using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tor : MonoBehaviour
{
    public enum TorArt
    {
        Eingang,
        Ausgang
    }

    #region Fields
    public TorArt tor;
    #endregion

    void Update()
    {
        // Beim Minigame angekommen gibt es kein Zurück mehr, der Eingang schließt sich
        if(tor == TorArt.Eingang && GameFlow.playerHasReachedMinigameOne)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        // Sobald das Minigame richtig gelöst wurde, öffnet sich der Ausgang

        if(tor == TorArt.Ausgang && GameFlow.playerHasDoneMinigameOne)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("TorNachUnten");
        }
    }
}
