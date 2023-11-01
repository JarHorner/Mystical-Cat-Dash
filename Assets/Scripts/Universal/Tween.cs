using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        Scene scene = SceneManager.GetActiveScene();

        GameUI.Instance.portalFader.gameObject.SetActive(true);
        LeanTween.rotate(GameUI.Instance.portalFader, 360f, 0.5f);

        LeanTween.scale(GameUI.Instance.portalFader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.portalFader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            if (scene.name == "Runner")
            {
                GameManager.Instance.loadedInto2DWorld = true;
                GameManager.Instance.timesEntered2DWorld++;


                Powerups.Instance.multiplyPickedUp = false;
                Powerups.Instance.magnetPickedUp = false;
                Powerups.Instance.shieldPickedUp = false;
                Powerups.Instance.speedPickedUp = false;

                GameUI.Instance.DisablePowerupImages();

                SceneManager.LoadScene("Flappy");
            }
            else if (scene.name == "Flappy")
            {
                GameManager.Instance.loadedFrom2DWorld = true;

                SceneManager.LoadScene("Runner");
            }
        });
    }

    public void TweenInNewScene()
    {
        // uses LeanTween to fade in at the start of the game.
        GameUI.Instance.portalFader.gameObject.SetActive(true);
        LeanTween.rotate(GameUI.Instance.portalFader, 360f, 0.5f);

        LeanTween.scale(GameUI.Instance.portalFader, new Vector3(1.1f, 1.1f, 1.1f), 0);
        // instead of using coroutine, append what happens after using anonymous function setOnComplete.
        LeanTween.scale(GameUI.Instance.portalFader, Vector3.zero, 0.5f).setOnComplete(() =>
        {
            GameUI.Instance.portalFader.gameObject.SetActive(false);
        });
    }

    public void TweenReplayGame()
    {
        // uses LeanTween to fade in at the start of the game.
        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            Destroy(GameManager.Instance.gameObject);

            GameUI.Instance.score.GetComponent<TMP_Text>().text = "000";

            GameUI.Instance.DisablePowerupImages();

            SceneManager.LoadScene("Runner");
        });
    }

    public void TweenMainMenu()
    {
        GameUI.Instance.fader.gameObject.SetActive(true);
        LeanTween.scale(GameUI.Instance.fader, Vector3.zero, 0);
        LeanTween.scale(GameUI.Instance.fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            Destroy(GameManager.Instance.gameObject);

            GameUI.Instance.score.GetComponent<TMP_Text>().text = "000";


            GameUI.Instance.DisablePowerupImages();
            GameUI.Instance.pauseButton.SetActive(false);
            GameUI.Instance.score.transform.parent.gameObject.SetActive(false);
            GameUI.Instance.gameOverPanel.SetActive(false);

            SceneManager.LoadScene("MainMenu");
        });
    }
}
