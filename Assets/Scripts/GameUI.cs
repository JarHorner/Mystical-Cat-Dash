using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{

    public static GameUI Instance { get; private set; }

    public GameObject score;
    public GameObject gameOverScore;
    public GameObject gameOverPanel;
    public GameObject startingText;
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

    }

    void Update()
    {

    }
}
