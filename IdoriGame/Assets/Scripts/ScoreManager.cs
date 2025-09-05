using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float appleTime;
    public float scoreMultiplier;
    public float appleScoreMultiplier;
    //
    public Animator AN_AppleUIImage;

    private float score; // a float to keep track of the score
    private float appleTimeCounter;
    //
    private TMP_Text TX_ScoreText;
    private Material M_ScoreText;

    public int Score
    {
        get { return (int)score * 10; }
    }

    void Start()
    {
        TX_ScoreText = GetComponent<TMP_Text>();

        M_ScoreText = new Material(TX_ScoreText.fontSharedMaterial);
        TX_ScoreText.fontSharedMaterial = M_ScoreText;
    }

    void Update()
    {
        // continously checks to see if the player is dead; if so, stop increasing the score
        if(Menu.gameStarted && !Menu.gameEnded)
        {
            // increase the score by 1 every second
            score += Time.deltaTime * scoreMultiplier;

            // multiply that score by 10 (so it goes up by 10 every sec instead)
            int formattedScore = (int)score * 10;

            // modify the text object to display the score
            TX_ScoreText.text = "Score: " + formattedScore;
        }

        if (appleTimeCounter > 0)
        {
            appleTimeCounter -= Time.deltaTime;
        }
        else if (scoreMultiplier != 1)
        {
            scoreMultiplier = 1;

            AN_AppleUIImage.Play("AppleFadeOut");
            M_ScoreText.SetFloat(ShaderUtilities.ID_GlowPower, 0f);
        }
    }

    public void EatApple()
    {
        appleTimeCounter = appleTime;
        scoreMultiplier = appleScoreMultiplier;

        AN_AppleUIImage.enabled = true;
        AN_AppleUIImage.Play("AppleFadeIn");
        M_ScoreText.SetFloat(ShaderUtilities.ID_GlowPower, 1f);
    }
}
