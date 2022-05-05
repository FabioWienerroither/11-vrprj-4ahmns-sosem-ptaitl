using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource hoppSound;
    public AudioSource clickSound;
    public AudioSource wrongSound;
    public AudioSource correctSound;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Play(string sound)
   {
        switch (sound)
        {
            case "hoppSound":
                hoppSound.Play();
                break;
            case "clickSound":
                hoppSound.Play();
                break;
            case "wrongSound":
                wrongSound.Play();
                break;
            case "correctSound":
                correctSound.Play();
                break;
        }
    }
}
