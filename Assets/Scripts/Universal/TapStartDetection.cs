using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapStartDetection : MonoBehaviour
{
    private GameUI gameUI;

    void Start()
    {
        gameUI = GameUI.Instance;
    }

    private void OnEnable()
    {
        InputManager.Instance.OnTapStart += TapStart;
    }

    void OnDisable()
    {
        InputManager.Instance.OnTapStart -= TapStart;
    }

    // enables the player to start the game by taping the screen.
    private void TapStart()
    {
        if (SceneManager.GetActiveScene().name == "Runner")
        {
            GameManager.Instance.isGameStarted = true;
            gameUI.startingText.SetActive(false);

            RunnerPlayerController runnerPlayer = GameObject.FindWithTag("Player").GetComponent<RunnerPlayerController>();
            runnerPlayer.currentState = PlayerState.run;

            Animator runnerPlayerAnim = runnerPlayer.gameObject.GetComponent<Animator>();
            runnerPlayerAnim.SetBool("Run", true);
        }
    }
}
