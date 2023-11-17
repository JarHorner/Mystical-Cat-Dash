using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class InGameMenus : MonoBehaviour
{
    public static InGameMenus Instance { get; private set; }
    [SerializeField] private AudioClip buttonPress;
    [SerializeField] private bool pauseGame = false;

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
        SoundManager.Instance.Play(buttonPress, 0.4f);

        Tween.Instance.TweenReplayGame();
    }

    // quits out of the game
    public void QuitToMainMenu()
    {
        if (pauseGame)
            PauseGame();

        SoundManager.Instance.Play(buttonPress, 0.4f);

        Tween.Instance.backToMainMenu = true;
        Tween.Instance.TweenMainMenu();
    }

    // open/closes the pause menu
    public void PauseGame()
    {
        if (pauseGame)
        {
            Time.timeScale = 1;
            GameUI.Instance.pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            GameUI.Instance.pausePanel.SetActive(true);
        }
        SoundManager.Instance.Play(buttonPress, 0.4f);

        pauseGame = !pauseGame;
    }
}
