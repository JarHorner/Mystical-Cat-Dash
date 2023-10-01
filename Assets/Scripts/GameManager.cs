using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Animations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    public int calculatedScore = 0;
    private float countdownToPointGain = 0f;
    public bool gameOver = false;
    public bool isGameStarted;
    private GameUI gameUI;

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

    private void OnEnable()
    {
        InputManager.Instance.OnTapStart += TapStart;
    }

    void OnDisable()
    {
        InputManager.Instance.OnTapStart -= TapStart;
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (!gameOver)
            {
                countdownToPointGain += Time.deltaTime;
                convertTimeToPoints();
            }
            else
            {
                Time.timeScale = 0;

                

                gameUI.gameOverScore.GetComponent<TMP_Text>().text = calculatedScore.ToString();
                gameUI.gameOverPanel.SetActive(true);
                countdownToPointGain = 0f;
            }
        }
    }

    private void TapStart()
    {
        isGameStarted = true;
        Destroy(gameUI.startingText);

        RunnerPlayerController runnerPlayer = GameObject.FindWithTag("Player").GetComponent<RunnerPlayerController>();
        runnerPlayer.currentState = PlayerState.run;

        Animator runnerPlayerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        runnerPlayerAnim.runtimeAnimatorController = Resources.Load<AnimatorController>("BasicMotions@Run");



    }

    private void convertTimeToPoints()
    {
        if (countdownToPointGain >= 1)
        {
            calculatedScore += 100;
            gameUI.score.GetComponent<TMP_Text>().text = calculatedScore.ToString();
            countdownToPointGain = 0;
        }
    }

    public void SwitchDimensions()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        if (scene.name == "Runner")
        {
            SceneManager.LoadScene("Flappy");
        }
        else if (scene.name == "Flappy")
        {
            SceneManager.LoadScene("Runner");
        }
    }
}
