using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool pausedGame = false;
    public GameObject pauseMenuUI;
    public GameManager gameManager;

    public Text messageUI;
    public Text continueButtonTxt;

    private void Start()
    {
        gameManager.onGameOver += OnGameOverEventHandler;
        gameManager.onStageCleared += OnStageClearedEventHandler;

        continueButtonTxt.text = "CONTINUE";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManager.PlayerLives > 0)
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (gameManager.PlayerLives > 0)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            pausedGame = false;
        }
        else
        {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
            pausedGame = false;
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        pausedGame = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    protected void OnGameOverEventHandler()
    {
        messageUI.text = "GAME OVER!" + Environment.NewLine + gameManager.scoreTxt.text;
        continueButtonTxt.text = "NEW GAME";
        Pause();
    }

    protected void OnStageClearedEventHandler()
    {
        messageUI.text = "STAGE CLEARED!" + Environment.NewLine + gameManager.scoreTxt.text;
        continueButtonTxt.text = "NEW GAME";
        Pause();
    }
}