using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public enum PlayerState
{
    idle,
    run,
    sprint,
    slide,
    jump,
}
public class RunnerPlayerController : MonoBehaviour
{
    public PlayerState currentState;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction primaryContact, primaryPosition, shiftLeft, shiftRight, jump, slide;
    public Animator animator;
    private Vector2 initialPos;
    private Vector2 currentPos => primaryPosition.ReadValue<Vector2>();

    [SerializeField] private CharacterController controller;
    private Vector3 direction;
    public int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -20;

    void Awake()
    {
        // Getting action and and setting up each action for the player
        var playerActionMap = inputMaster.FindActionMap("RunnerPlayer");

        primaryContact = playerActionMap.FindAction("PrimaryContact");
        primaryPosition = playerActionMap.FindAction("PrimaryPosition");
        shiftLeft = playerActionMap.FindAction("ShiftLeft");
        shiftRight = playerActionMap.FindAction("ShiftRight");
        jump = playerActionMap.FindAction("Jump");
        slide = playerActionMap.FindAction("Slide");
    }

    void Start()
    {
        // if game is not started, player is idle. if it is the player will be running
        if (!GameManager.Instance.isGameStarted)
        {
            currentState = PlayerState.idle;
        }
        else
        {
            currentState = PlayerState.run;
            animator.SetBool("Run", true);
        }
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
    }

    // enables all the actions
    private void OnEnable()
    {
        shiftLeft.Enable();
        shiftLeft.performed += ShiftLeft;

        shiftRight.Enable();
        shiftRight.performed += ShiftRight;

        jump.Enable();
        jump.performed += Jump;

        slide.Enable();
        slide.performed += Slide;
    }

    // disables all the actions
    void OnDisable()
    {
        shiftLeft.performed -= ShiftLeft;
        shiftLeft.Disable();

        shiftRight.performed -= ShiftRight;
        shiftRight.Disable();

        jump.performed -= Jump;
        jump.Disable();

        slide.performed -= Slide;
        slide.Disable();

        Debug.Log("Player deleted");
    }

    void Update()
    {
        // when not idle the game will slowly increase speed until capped
        if (currentState != PlayerState.idle)
        {
            // increases speed of player slowly as game progresses to a maximum amount
            if (GameManager.Instance.forwardSpeed < GameManager.Instance.maximumForwardSpeed)
            {
                GameManager.Instance.forwardSpeed += 0.1f * Time.deltaTime;
            }
        }

        // moves the player along the z axis
        direction.z = GameManager.Instance.forwardSpeed;
        direction.y += gravity * Time.deltaTime;

        // determines the location of the player based on desired lane
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //Normalizes and lerps the player to the different lanes. Allows collisions to occur properly!
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    // ensures the player is moving at a fixed amount
    void FixedUpdate()
    {
        if (!GameManager.Instance.isGameStarted)
            return;

        controller.Move(direction * Time.fixedDeltaTime);
    }

    // based on the collider hit, does something.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Object")
        {
            GameManager.Instance.gameOver = true;
        }
        else if (hit.transform.tag == "Portal")
        {
            GameManager.Instance.SwitchDimensions();
        }
        else if (hit.transform.tag == "Coin")
        {
            Coin coin = hit.gameObject.GetComponent<Coin>();
            coin.Scored();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("powerup entered");
    }

    private void ShiftLeft(InputAction.CallbackContext context)
    {
        if (desiredLane > 0 && currentState != PlayerState.idle)
        {
            desiredLane--;
        }
    }

    // shifts the players lane to the left
    public void SwipeShiftLeft()
    {
        if (desiredLane > 0 && currentState != PlayerState.idle)
        {
            desiredLane--;
        }
    }

    private void ShiftRight(InputAction.CallbackContext context)
    {
        if (desiredLane < 2 && currentState != PlayerState.idle)
        {
            desiredLane++;
        }
    }

    // shifts the players lane to the right
    public void SwipeShiftRight()
    {
        if (desiredLane < 2 && currentState != PlayerState.idle)
        {
            desiredLane++;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.performed)
        {
            direction.y = jumpForce;
        }
    }

    // jumps the player up
    public void SwipeJump()
    {
        if (controller.isGrounded && currentState != PlayerState.slide)
        {
            StartCoroutine(RunnerJump());
            direction.y = jumpForce;
        }
    }

    private IEnumerator RunnerJump()
    {
        Debug.Log("Jump!");
        direction.y = jumpForce;
        currentState = PlayerState.jump;
        animator.SetBool("Run", false);
        animator.SetTrigger("Jump");

        yield return new WaitForSeconds(1f);

        currentState = PlayerState.run;
        animator.SetBool("Run", true);
        animator.SetTrigger("Land");
    }

    private void Slide(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            StartCoroutine(RunnerSlide());
        }
    }

    // slides the player along the ground
    public void SwipeSlide()
    {
        if (controller.isGrounded && currentState != PlayerState.jump)
        {
            StartCoroutine(RunnerSlide());
        }
    }

    private IEnumerator RunnerSlide()
    {
        Debug.Log("Slide!");
        currentState = PlayerState.slide;
        animator.SetBool("Run", false);
        animator.SetTrigger("SleepStart");

        // these are needed as I cannot edit the animations themselves, this would be in the animations if I could.
        this.gameObject.transform.position =
        controller.center = new Vector3(0.002f, 0.015f, 0.04f);
        controller.radius = 0.01f;
        controller.height = 0f;

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("SleepEnd");
        currentState = PlayerState.run;
        animator.SetBool("Run", true);
        
        // these are needed as I cannot edit the animations themselves, this would be in the animations if I could.
        controller.center = new Vector3(0f, 0.025f, 0f);
        controller.radius = 0.01f;
        controller.height = 0.055f;
    }


}
