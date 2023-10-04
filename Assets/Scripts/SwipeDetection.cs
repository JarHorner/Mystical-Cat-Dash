using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshhold = 0.9f;
    [SerializeField]

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    [SerializeField]
    private RunnerPlayerController runnerPlayer;


    void Update()
    {
        // needed to ensure the runnerPlayer varible is assigned
        if (SceneManager.GetActiveScene().name == "Runner" && runnerPlayer == null)
        {
            runnerPlayer = GameObject.FindWithTag("Player").GetComponent<RunnerPlayerController>();
        }
    }

    private void OnEnable()
    {
        InputManager.Instance.OnStartTouch += SwipeStart;
        InputManager.Instance.OnEndTouch += SwipeEnd;
    }

    void OnDisable()
    {
        InputManager.Instance.OnStartTouch -= SwipeStart;
        InputManager.Instance.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    // helps detect if a swipe has occcured and not a random touch on the screen.
    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
        (endTime - startTime) <= maximumTime)
        {
            Debug.Log("Swipe Detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    // depending on the swipe direction, different actions occur
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshhold)
        {
            Debug.Log("UP");
            runnerPlayer.SwipeJump();
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshhold)
        {
            Debug.Log("DOWN");
            runnerPlayer.SwipeSlide();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshhold)
        {
            Debug.Log("LEFT");
            runnerPlayer.SwipeShiftLeft();
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshhold)
        {
            Debug.Log("RIGHT");
            runnerPlayer.SwipeShiftRight();
        }
    }
}
