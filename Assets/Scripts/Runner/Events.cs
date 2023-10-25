using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Events : MonoBehaviour
{
    public static Events Instance { get; private set; }

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
        Tween.Instance.TweenReplayGame();
    }

    // quits out of the game
    public void QuitToMainMenu()
    {
        Tween.Instance.backToMainMenu = true;
        Tween.Instance.TweenMainMenu();
    }
}
