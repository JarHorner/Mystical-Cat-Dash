using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private FlappyGameController flappyGameController;
    private Rigidbody2D rb;
    [SerializeField] private float scrollSpeed = 4.5f; // base 4.5
    private bool scrolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flappyGameController = GameObject.FindWithTag("GameController").GetComponent<FlappyGameController>();
    }

    void Update()
    {
        if (!scrolling && flappyGameController.playerPositioned)
        {
            ChangeScrollSpeed();
            rb.velocity = new Vector2(-scrollSpeed, 0);
            scrolling = true;
        }

        if (GameManager.Instance.gameOver)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ChangeScrollSpeed()
    {
        if (GameManager.Instance.timesEntered2DWorld == 1)
        {
            rb.velocity = new Vector2(-scrollSpeed, 0);
        }
        else if (GameManager.Instance.timesEntered2DWorld == 2)
        {
            scrollSpeed += 0.25f;
            rb.velocity = new Vector2(-scrollSpeed, 0);
        }
        else if (GameManager.Instance.timesEntered2DWorld == 3)
        {
            scrollSpeed += 0.5f;
            rb.velocity = new Vector2(-scrollSpeed, 0);
        }
        else if (GameManager.Instance.timesEntered2DWorld == 4)
        {
            scrollSpeed += 0.75f;
            rb.velocity = new Vector2(-scrollSpeed, 0);
        }
        else if (GameManager.Instance.timesEntered2DWorld >= 5)
        {
            scrollSpeed += 1.0f;
            rb.velocity = new Vector2(-scrollSpeed, 0);
        }
    }
}
