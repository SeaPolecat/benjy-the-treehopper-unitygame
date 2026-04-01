using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public AudioSource AS_GameMusic;
    public AudioSource AS_FallSound;

    private bool musicStopped;

    void Update()
    {
        if (Menu.gameEnded && !musicStopped)
        {
            musicStopped = true;

            AS_FallSound.Play();
            AS_GameMusic.Stop();
        }
    }
}
