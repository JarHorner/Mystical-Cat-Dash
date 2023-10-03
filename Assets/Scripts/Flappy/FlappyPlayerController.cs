using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlappyPlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector3 velocity;
    public Animator anim;
    [SerializeField] private FlappyGameController flappyGameController;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction jump;
    [SerializeField] private float jumpSpeed;
    private bool isDead = false;

    void Awake()
    {
        var playerActionMap = inputMaster.FindActionMap("FlappyPlayer");

        jump = playerActionMap.FindAction("Jump");
    }


    private void OnEnable()
    {
        jump.Enable();
        jump.performed += Jump;
        jump.canceled += Jump;
    }

    void OnDisable()
    {
        jump.performed -= Jump;
        jump.canceled -= Jump;
        jump.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flappyGameController = GameObject.FindWithTag("GameController").GetComponent<FlappyGameController>();
    }

    void Update()
    {
        if (!flappyGameController.playerPositioned)
        {
            transform.Translate(Vector2.right * 10 * Time.deltaTime);
        }

        if (transform.position.x > -2)
        {
            flappyGameController.playerPositioned = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OutOfBounds" || collision.gameObject.tag == "Object")
        {
            Debug.Log("Dying");
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Portal")
        {
            //GameManager.Instance.SwitchDimensions();
            SceneManager.LoadScene("Runner");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!isDead && context.performed)
        {
            Debug.Log("JUMP!");
            rb.AddForce(new Vector2(0, jumpSpeed));
            rb.velocity = Vector2.zero;
        }
        // if (!GameManager.Instance.gameOver && context.performed)
        // {
        //     Debug.Log("JUMP!");
        //     rb.AddForce(new Vector2(0, jumpSpeed));
        //     rb.velocity = Vector2.zero;
        // }

    }

    IEnumerator Death()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector2.zero;
        isDead = true;
        //GameManager.Instance.gameOver = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
