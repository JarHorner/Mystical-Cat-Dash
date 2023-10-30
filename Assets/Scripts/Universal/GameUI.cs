using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{

    public static GameUI Instance { get; private set; }

    public GameObject score;
    public Image powerupImage;
    public GameObject gameOverScore;
    public GameObject pauseButton;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject startingText;
    public RectTransform fader;
    public GameObject megaflapButton;
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
}
