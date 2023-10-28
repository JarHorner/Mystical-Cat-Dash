using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tween : MonoBehaviour
{
    public static Tween Instance { get; private set; }

    public bool backToMainMenu = false;

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

    public void TweenEnd()
    {
        Debug.Log("Tween end");
        // uses LeanTween to fade in at the start of the game.
        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.1f, 1.1f, 1.1f), 0);
        // instead of using coroutine, append what happens after using anonymous function setOnComplete.
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            GameUI.Instance.fader.gameObject.SetActive(false);
        });
    }

    public void TweenStartGame(RectTransform fader)
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            if (GameManager.Instance)
            {
                GameManager.Instance.loaded = false;
            }
            SceneManager.LoadScene("Runner");
        });
    }

    public void TweenBetweenScenes()
    {
        Debug.Log("Tween between scenes");
        Scene scene = SceneManager.GetActiveScene();

        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            if (scene.name == "Runner")
            {
                GameUI.Instance.powerupImage.enabled = false;

                Powerups.Instance.multiplyPickedUp = false;
                Powerups.Instance.magnetPickedUp = false;
                Powerups.Instance.shieldPickedUp = false;
                Powerups.Instance.speedPickedUp = false;

                SceneManager.LoadScene("Flappy");
            }
            else if (scene.name == "Flappy")
            {
                GameManager.Instance.loaded = false;
                SceneManager.LoadScene("Runner");
            }
        });
    }

    public void TweenReplayGame()
    {
        Debug.Log("Tween replay game");

        // uses LeanTween to fade in at the start of the game.
        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            Destroy(GameManager.Instance.gameObject);

            GameUI.Instance.score.GetComponent<TMP_Text>().text = "000";
            GameUI.Instance.powerupImage.enabled = false;

            SceneManager.LoadScene("Runner");
        });
    }

    public void TweenMainMenu()
    {
        Debug.Log("Tween Main Menu");

        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            Destroy(GameManager.Instance.gameObject);

            GameUI.Instance.score.GetComponent<TMP_Text>().text = "000";
            GameUI.Instance.powerupImage.enabled = false;
            GameUI.Instance.score.transform.parent.gameObject.SetActive(false);
            GameUI.Instance.gameOverPanel.SetActive(false);

            SceneManager.LoadScene("MainMenu");
        });
    }
}
