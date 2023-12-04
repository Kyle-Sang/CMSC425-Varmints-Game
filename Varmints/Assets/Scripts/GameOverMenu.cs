using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        PlayerHealth.dead = false;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
