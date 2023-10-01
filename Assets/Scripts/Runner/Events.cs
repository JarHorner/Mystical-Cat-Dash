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
    public void ReplayGame()
    {
        GameManager.Instance.gameOver = false;
        GameManager.Instance.gameOverMenuOpen = false;
        GameManager.Instance.calculatedScore = 0;
        GameUI.Instance.gameOverPanel.SetActive(false);
        GameUI.Instance.score.GetComponent<TMP_Text>().text = "0";
        SceneManager.LoadScene("Runner");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
