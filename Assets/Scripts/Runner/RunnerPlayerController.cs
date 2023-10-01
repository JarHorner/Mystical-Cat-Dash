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
    public float forwardSpeed;
    public float maximumForwardSpeed;

    public int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -20;

    void Awake()
    {
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
        currentState = PlayerState.idle;
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
    }

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

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.run)
        {
            animator.runtimeAnimatorController = Resources.Load<AnimatorController>("BasicMotions@Run");
        }

        // increases speed of player slowly as game progresses to a maximum amount
        if (forwardSpeed < maximumForwardSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

        direction.z = forwardSpeed;
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

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            currentState = PlayerState.run;
        }

        if (!GameManager.Instance.isGameStarted)
            return;

        controller.Move(direction * Time.fixedDeltaTime);
    }

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
    }

    private void ShiftLeft(InputAction.CallbackContext context)
    {
        if (desiredLane > 0 && currentState != PlayerState.idle)
        {
            desiredLane--;
        }
    }

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

    public void SwipeJump()
    {
        if (controller.isGrounded)
        {
            direction.y = jumpForce;
        }
    }

    private void Slide(InputAction.CallbackContext context)
    {
        if (controller.isGrounded)
        {
            StartCoroutine(RunnerSlide());
        }
    }

    public void SwipeSlide()
    {
        if (controller.isGrounded)
        {
            StartCoroutine(RunnerSlide());
        }
    }

    private IEnumerator RunnerSlide()
    {
        Debug.Log("Slide!");
        gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
        yield return new WaitForSeconds(1f);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }


}
