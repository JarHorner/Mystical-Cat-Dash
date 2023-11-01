using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenus : MonoBehaviour
{
    public static InGameMenus Instance { get; private set; }
    [SerializeField] private AudioClip buttonPress;
    private bool pauseGame = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // allows the player to restart the game by resetting all the values of the GameManager.
    public void ReplayGame()
    {
        SoundManager.Instance.Play(buttonPress);

        Tween.Instance.TweenReplayGame();
    }

    // quits out of the game
    public void QuitToMainMenu()
    {
        if (pauseGame)
            PauseGame();

        SoundManager.Instance.Play(buttonPress);
        
        Tween.Instance.backToMainMenu = true;
        Tween.Instance.TweenMainMenu();
    }

    // open/closes the pause menu
    public void PauseGame()
    {
        SoundManager.Instance.Play(buttonPress);
        
        pauseGame = !pauseGame;

        if (!pauseGame)
        {
            Time.timeScale = 1;
            GameUI.Instance.pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            GameUI.Instance.pausePanel.SetActive(true);
        }
    }
}
