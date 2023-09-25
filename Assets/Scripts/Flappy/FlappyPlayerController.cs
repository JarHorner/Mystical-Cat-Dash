using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlappyPlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 velocity;
    public Animator anim;
    private FlappyGameController flappyGameController;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction jump;
    [SerializeField] private float jumpSpeed;

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
        rb = GetComponent<Rigidbody>();
        flappyGameController = GameObject.Find("FlappyGameController").GetComponent<FlappyGameController>();
    }

    void Update()
    {
        velocity.y -= 15 * Time.deltaTime;
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(Death());
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP!");
        velocity.y = Mathf.Sqrt(jumpSpeed);
        
    }

    IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        GameManager.Instance.gameOver = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
