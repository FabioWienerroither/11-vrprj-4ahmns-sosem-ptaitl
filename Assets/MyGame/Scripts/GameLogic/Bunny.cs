using UnityEngine;

public class Bunny : MonoBehaviour
{
    #region Privates
    bool alreadyTriggeredAtMinigameOne;
    bool alreadyTriggeredAtEntry;
    bool alreadyTriggeredAtExit;
    #endregion

    #region Fields
    public Animator speechbubbleAnimator;
    #endregion

    // Mithilfe eines Enums werden festgelegte Positionen im Spielfluss definiert
    [System.Serializable]
    public enum BunnyPosition
    {
        atEntry,
        atMinigameOne,
        atExit
    }

    #region Fields
    [HideInInspector]
    public BunnyPosition bunnyPosition;
    #endregion

    void Start()
    {
        // Der Hase startet immer beim Eingang

        bunnyPosition = BunnyPosition.atEntry;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Je nach Standpunkt des Hasen während der Kollision mit dem Spieler soll ein anderer Checkpoint true werden

            switch (bunnyPosition)
            {
                case BunnyPosition.atEntry:
                    Checkpoints.playerHasReachedBunny = true;
                    if (!alreadyTriggeredAtEntry)
                    {
                        speechbubbleAnimator.SetTrigger("EinblendenMitZeit");
                        alreadyTriggeredAtEntry = true;
                    }
                    break;

                case BunnyPosition.atMinigameOne:
                    Checkpoints.playerHasReachedMinigameOne = true;
                    if (!alreadyTriggeredAtMinigameOne)
                    {
                        speechbubbleAnimator.ResetTrigger("EinblendenMitZeit");
                        speechbubbleAnimator.SetTrigger("EinblendenOhneZeit");
                        alreadyTriggeredAtMinigameOne = true;
                    }
                    break;

                case BunnyPosition.atExit:
                    Checkpoints.playerHasReachedExit = true;
                    if (!alreadyTriggeredAtExit)
                    {
                        speechbubbleAnimator.SetTrigger("EinblendenOhneZeit");
                        alreadyTriggeredAtExit = true;
                    }
                    break;
            }
        }
    }

    // Wird am Ende der Animation in Richtung Minigame One getriggert

    void AtMinigameOne()
    {
        GetComponent<Animator>().SetLayerWeight(1, 0);
        bunnyPosition = BunnyPosition.atMinigameOne;
    }

    // Wird am Ender der Animation in Richtung Exit getriggert

    void AtExit()
    {
        GetComponent<Animator>().SetLayerWeight(1, 0);
        bunnyPosition = BunnyPosition.atExit;
    }

    // Wird in der 'Jump' Animation aufgerufen

    void PlayHoppSound()
    {
        AudioManager.instance.Play(AudioManager.instance.hoppSound);
    }
}
