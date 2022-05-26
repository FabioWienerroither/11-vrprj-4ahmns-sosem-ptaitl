using UnityEngine;

public class Bunny : MonoBehaviour
{
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
        switch (other.gameObject.tag)
        {
            case "Player":

                // Je nach Standpunkt des Hasen während der Kollision mit dem Spieler soll ein anderer Checkpoint true werden

                switch (bunnyPosition)
                {
                    case BunnyPosition.atEntry:
                        GameFlow.playerHasReachedBunny = true;

                        // Kommt der Spieler in die Nähe des Hasen, soll die Sprechblase eingeblendet werden 

                        speechbubbleAnimator.SetTrigger("EinblendenMitZeit");
                        break;

                    case BunnyPosition.atMinigameOne:
                        GameFlow.playerHasReachedMinigameOne = true;
                        speechbubbleAnimator.SetTrigger("EinblendenOhneZeit");
                        break;

                    case BunnyPosition.atExit:
                        GameFlow.playerHasReachedExit = true;
                        break;
                }
                break;

            case "Ground":

                // Wenn der Hase den Boden berührt, soll ein entsprechender Sound abgespielt werden

                AudioManager.instance.Play(AudioManager.instance.hoppSound);
                break;
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
}
