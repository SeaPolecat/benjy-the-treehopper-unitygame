using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // SINGLETONS
    public static bool gameStarted = false;
    public static bool gameEnded = false;
    public static int endScore = 0;
    public static int highScore = 0;
    public static int endSeedCount = 0;

    public GameObject titleScreen;
    public GameObject instructionsScreen;
    public GameObject deathScreen;
    public GameObject cutscene1;
    public GameObject cutscene2;
    public GameObject cutscene3;
    public GameObject cutscene4;
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject nextButton;
    //
    public Text TX_EndScoreText;
    public Text TX_HighScoreText;
    public Text TX_EndSeedCountText;
    public ShowSeeds S_EndSeedSpawnpoint_ShowSeeds;
    public AudioSource AS_ButtonSound;

    void Start()
    {
        if (gameEnded)
        {
            titleScreen.SetActive(false);
            cutscene3.SetActive(true);

            TX_EndScoreText.text = "Score: " + endScore;
            TX_HighScoreText.text = "High Score: " + highScore;

            if(endSeedCount == 0)
            {
                TX_EndSeedCountText.text = "You found 0 Seeds... try again?";
            }
            else
            {
                TX_EndSeedCountText.text = "Hooray! You found " + endSeedCount;

                if (endSeedCount == 1)
                {
                    TX_EndSeedCountText.text += " Seed!";
                }
                else
                {
                    TX_EndSeedCountText.text += " Seeds!";
                }
            }
        }
    }

    public void PlayButton()
    {
        titleScreen.SetActive(false);
        cutscene1.SetActive(true);

        AS_ButtonSound.Play();
    }

    public void NextButton()
    {
        if (cutscene1.activeSelf)
        {
            cutscene1.SetActive(false);
            cutscene2.SetActive(true);
        }
        else if (cutscene2.activeSelf)
        {
            cutscene2.SetActive(false);
            instructionsScreen.SetActive(true);
        }
        else if (cutscene3.activeSelf)
        {
            cutscene3.SetActive(false);
            cutscene4.SetActive(true);
        }
        else if (cutscene4.activeSelf)
        {
            cutscene4.SetActive(false);
            deathScreen.SetActive(true);

            S_EndSeedSpawnpoint_ShowSeeds.showSeeds();
        }
        AS_ButtonSound.Play();
    }

    public void GoButton()
    {
        if (instructionsScreen.activeSelf)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void RetryButton()
    {
        deathScreen.SetActive(false);
        titleScreen.SetActive(true);

        gameStarted = false;
        gameEnded = false;

        AS_ButtonSound.Play();
    }
}
