using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * ATTACHED TO:
 * -Score Text
 */

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // a text object that displays the player's score

    private float score; // a float to keep track of the score

    void Update()
    {
        // continously checks to see if the player is dead; if so, stop increasing the score
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            // increase the score by 1 every second
            score += Time.deltaTime;

            // multiply that score by 10 (so it goes up by 10 every sec instead)
            int formattedScore = (int)score * 10;

            // modify the text object to display the score
            scoreText.text = "Score: " + formattedScore.ToString();
        }
    }
}
