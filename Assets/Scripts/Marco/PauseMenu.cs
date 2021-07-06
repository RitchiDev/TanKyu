using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Audio")]

    [SerializeField] private GameObject InGamePauseMenu;
    private bool pause;

    private void Start()
    {
        InGamePauseMenu.SetActive(false);
        pause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        InGamePauseMenu.SetActive(true);
        AudioListener.pause = true;
        pause = true;

        Time.timeScale = 0;
    }

    public void Resume()
    {
        InGamePauseMenu.SetActive(false);
        AudioListener.pause = false;
        pause = false;

        Time.timeScale = 1;
    }

    public void Restart()
    {
        InGamePauseMenu.SetActive(false);
        AudioListener.pause = false;
        pause = false;

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        //Debug.Log("Moved to Main Menu");
        InGamePauseMenu.SetActive(false);
        AudioListener.pause = false;
        pause = false;

        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        //Debug.Log("Closed game");
        Application.Quit();
    }

}
