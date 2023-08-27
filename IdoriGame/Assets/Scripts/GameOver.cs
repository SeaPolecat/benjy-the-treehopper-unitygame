using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * ATTACHED TO:
 * -Game Manager
 */

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // a panel that shows the user they lost

    void Update()
    {
        // continuously checks to see if the player is dead; if so, activate the game over panel
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    /**
     * REQUIRES:
     * n/a
     * 
     * MODIFIES:
     * n/a
     * 
     * EFFECTS:
     * reloads the scene to restart the game
     */
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
