using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [Header("3D AudioSources")]
    public AudioSource hoppSound;
    public AudioSource clickSound;
    public AudioSource wrongSound;
    public AudioSource correctSound;

    [Header("2D AudioSources")]
    public AudioSource ambience;
    public AudioSource music;
    public AudioSource birds;
    #endregion

    public static AudioManager instance;

    void Awake()
    {
        // Über die statische Instanz des AudioManager, kann von jedem Script darauf zugegriffen werden

        instance = this;
    }

    void Start()
    {
        Play(ambience);
    }

    // Über die Play Methode kann zu jedem Zeitpunkt im Spiel ein Sound getriggert werden

    public void Play(AudioSource sound)
    {
        sound.Play();
    }
}
