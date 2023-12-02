using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        PlayerHealth.dead = false;
    }

}
