using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Bools")]
    public bool playerHasReachedBunny;

    [Header("Referenzvariablen")]
    public GameObject speechbubble;
    public Animator bunnyAnimator;
    public Bunny bunnyScript;

    void Update()
    {
        if (playerHasReachedBunny)
        {
            speechbubble.SetActive(true);

            if (speechbubble.GetComponent<SpeechbubbleProperties>().timeUp)
            {
                speechbubble.SetActive(false);
                speechbubble.transform.GetChild(1).gameObject.SetActive(false);
                bunnyAnimator.SetLayerWeight(1, 1);
                bunnyAnimator.SetTrigger("BunnyStartToQuizOne");
            }

            if (bunnyScript.atQuizOne)
            {
                bunnyAnimator.SetLayerWeight(1, 0);

            }
        }
    }
}
