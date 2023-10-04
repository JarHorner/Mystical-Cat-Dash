using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public RectTransform fader;
    public int calculatedScore = 0;
    public float forwardSpeed;
    public float maximumForwardSpeed;
    private float countdownToPointGain = 0f;
    public bool gameOver = false;
    public bool gameOverMenuOpen = false;
    public bool isGameStarted;
    private GameUI gameUI;
    public bool loaded = false;
    [SerializeField] private AudioClip gameOverSound;

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

    void Start()
    {
        gameUI = GameUI.Instance;
        isGameStarted = false;
    }

    void Update()
    {
        if (!loaded)
        {
            // uses LeanTween to fade in at the start of the game.
            fader.gameObject.SetActive(true);
            loaded = true;
            LeanTween.scale(fader, new Vector3(1.1f, 1.1f, 1.1f), 0);
            // instead of using coroutine, append what happens after using anonymous function setOnComplete.
            LeanTween.scale(fader, Vector3.zero, 0.5f).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        }
        
        // if game is not started, start game text will appear and game over panel will be hidden
        // if game is started and no game over, points start adding up.
        if (!isGameStarted)
        {
            gameUI.gameOverPanel.SetActive(false);
            gameUI.startingText.SetActive(true);
        }
        else
        {
            if (!gameOver)
            {
                countdownToPointGain += Time.deltaTime;
                ConvertTimeToPoints();
            }
            else
            {
                // when game over menu is active, game over sound plays, time scale is 0 and values are reset.
                if (!gameOverMenuOpen)
                {
                    SoundManager.Instance.Play(gameOverSound);

                    Time.timeScale = 0;


                    gameUI.gameOverScore.GetComponent<TMP_Text>().text = calculatedScore.ToString();
                    gameUI.gameOverPanel.SetActive(true);
                    countdownToPointGain = 0f;

                    gameOverMenuOpen = true;
                }
            }
        }
    }

    // converts the time playing the game into points every second.
    private void ConvertTimeToPoints()
    {
        if (countdownToPointGain >= 1)
        {
            calculatedScore += 100;
            gameUI.score.GetComponent<TMP_Text>().text = calculatedScore.ToString();
            countdownToPointGain = 0;
        }
    }

    // adds score to calculated score
    public void Scored()
    {
        calculatedScore += 200;
        gameUI.score.GetComponent<TMP_Text>().text = calculatedScore.ToString();
    }

    // depending on what scene you are in, swaps to the other with transitions
    public void SwitchDimensions()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);


        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setOnComplete(() =>
        {
            if (scene.name == "Runner")
            {
                SceneManager.LoadScene("Flappy");
            }
            else if (scene.name == "Flappy")
            {
                loaded = false;
                SceneManager.LoadScene("Runner");
            }
        });
    }
}
