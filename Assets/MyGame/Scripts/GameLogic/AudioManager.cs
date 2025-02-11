using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Fields
    [Header("3D AudioSources")]
    public AudioSource hoppSound;
    public AudioSource clickSound;
    public AudioSource wrongSound;
    public AudioSource correctSound;
    public AudioSource bunnyIdle;

    [Header("2D AudioSources")]
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
        // Die Hintergrundmusik, die Vogelgeräusche und die Bunny Idle-Sounds sollen als 2D Stereoquellen von Anfang an abspielen

        Play(birds);
        Play(music);
        Play(bunnyIdle);
    }

    // Über die Play Methode kann zu jedem Zeitpunkt im Spiel ein Sound getriggert werden

    public void Play(AudioSource sound)
    {
        sound.Play();
    }
}
