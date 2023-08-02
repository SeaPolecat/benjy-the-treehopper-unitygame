using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This script is attached to:
 * -Score Text
 */

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    private float score;

    void Update()
    {
        //need to check if player is alive

        score += Time.deltaTime;

        int formattedScore = (int)score * 10;

        scoreText.text = "Score: " + formattedScore.ToString();
    }
}
