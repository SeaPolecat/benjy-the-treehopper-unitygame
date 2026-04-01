using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float deathPauseTime;
    //
    public ScoreManager S_ScoreText_ScoreManager;
    public CollectItems S_Player_CollectItems;

    private float deathPauseTimeCounter;

    void Start()
    {
        deathPauseTimeCounter = deathPauseTime;
    }

    void Update()
    {
        if (Menu.gameEnded)
        {
            if (deathPauseTimeCounter > 0)
            {
                deathPauseTimeCounter -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);

            Menu.gameEnded = true;
            Menu.endScore = S_ScoreText_ScoreManager.Score;

            if (S_ScoreText_ScoreManager.Score > Menu.highScore)
            {
                Menu.highScore = S_ScoreText_ScoreManager.Score;
            }
            Menu.endSeedCount = S_Player_CollectItems.SeedCount;
        }
    }
}
