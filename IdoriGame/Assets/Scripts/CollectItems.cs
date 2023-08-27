using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ATTACHED TO:
 * -Player
 */

public class CollectItems : MonoBehaviour
{
    public GameObject seed; // the seed object that the player collects
    public Text seedCountText; // a text object that displays how many seeds the player has collected

    private float seedCount; // counts the number of seeds the player has
    private AudioSource popSound; // a popping sound that plays every time the player collects a seed

    void Start()
    {
        popSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Seed")
        {
            Destroy(other.gameObject);

            // increase the seed count and display it
            seedCount++;
            seedCountText.text = seedCount.ToString();

            popSound.Play();
        }
    }
}
