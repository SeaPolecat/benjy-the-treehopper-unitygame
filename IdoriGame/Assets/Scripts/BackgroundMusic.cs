using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Background Music
 */

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic; // a background music object (idrk what it does lmao)

    // 'Awake' is similar to the 'BeginPlay' event in Unreal Engine
    void Awake()
    {
        // makes sure that the background music isn't null when the game starts
        if(backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
