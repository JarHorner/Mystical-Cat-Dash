using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private FlappyPlayerController flappyPlayerController;
    private Rigidbody2D rb;
    [SerializeField] private float scrollSpeed;
    private bool scrolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flappyPlayerController = GameObject.FindWithTag("Player").GetComponent<FlappyPlayerController>();
    }

    void Update()
    {
        if (!scrolling)
        {
            rb.velocity = new Vector2(-scrollSpeed, 0);
            scrolling = true;
        }

        // if (GameController.instance.scoreNum % 4 == 0 && GameController.instance.scoreNum >= 4)
        // {
        //     ChangeScrollSpeed();
        // }

        // if (GameManager.Instance.gameOver)
        // {
        //     rb.velocity = Vector2.zero;
        // }
    }

    // private void ChangeScrollSpeed()
    // {
    //     if (GameController.instance.scoreNum == 4)
    //     {
    //         scrollSpeed = 11.25f;
    //         rb.velocity = new Vector2(-scrollSpeed, 0);
    //     }
    //     else if (GameController.instance.scoreNum == 8)
    //     {
    //         scrollSpeed = 11.5f;
    //         rb.velocity = new Vector2(-scrollSpeed, 0);
    //     }
    //     else if (GameController.instance.scoreNum == 12)
    //     {
    //         scrollSpeed = 11.75f;
    //         rb.velocity = new Vector2(-scrollSpeed, 0);
    //     }
    //     else if (GameController.instance.scoreNum == 16)
    //     {
    //         scrollSpeed = 12f;
    //         rb.velocity = new Vector2(-scrollSpeed, 0);
    //     }
    // }
}
