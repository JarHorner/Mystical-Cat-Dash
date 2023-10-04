using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

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
        GameManager.Instance.isGameStarted = true;
        gameUI.startingText.SetActive(false);

        RunnerPlayerController runnerPlayer = GameObject.FindWithTag("Player").GetComponent<RunnerPlayerController>();
        runnerPlayer.currentState = PlayerState.run;

        Animator runnerPlayerAnim = runnerPlayer.gameObject.GetComponent<Animator>();
        runnerPlayerAnim.runtimeAnimatorController = Resources.Load<AnimatorController>("BasicMotions@Run");
    }
}
