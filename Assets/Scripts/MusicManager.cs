using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource inGameMusic;

    void Start()
    {
        // Start with menu music playing
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        inGameMusic.Stop();
        menuMusic.Play();
    }

    public void PlayInGameMusic()
    {
        menuMusic.Stop();
        inGameMusic.Play();
    }
}
