using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    [Header("Bools")]
    public bool atQuizOne;

    [Header("Referenzvariablen")]
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerHasReachedBunny = true;
        }

        switch (other.gameObject.tag)
        {
            case "Player":
                gameManager.playerHasReachedBunny = true;
                break;

            case "Ground":
                AudioManager.instance.Play("hoppSound");
                break;
        }
    }
}
