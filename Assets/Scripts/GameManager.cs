using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance { get; private set; }
    [SerializeField]
    private Camera runnerCamera;
    [SerializeField]
    private Camera flappyCamera;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TMP_Text score;
    private int calculatedScore = 0;
    private float countdownToPointGain = 0f;

    private void Awake()
    {
        if (GameManagerInstance == null)
        {
            GameManagerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        countdownToPointGain += Time.deltaTime;

        convertTimeToPoints();
    }

    private void convertTimeToPoints() 
    {
        if (countdownToPointGain >= 1)
        {
            calculatedScore += 100;
            score.text = calculatedScore.ToString();
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
    }
}
