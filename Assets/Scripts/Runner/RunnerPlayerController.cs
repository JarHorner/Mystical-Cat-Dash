using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction primaryContact, primaryPosition, shiftLeft, shiftRight, jump;

    private Vector2 initialPos;
    private Vector2 currentPos => primaryPosition.ReadValue<Vector2>();

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

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
    }

    void Start()
    {
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
    }

    void OnDisable()
    {
        shiftLeft.performed -= ShiftLeft;
        shiftLeft.Disable();

        shiftRight.performed -= ShiftRight;
        shiftRight.Disable();

        jump.performed -= Jump;
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
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
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Object")
        {
            GameManager.Instance.gameOver = true;
        }
    }

    private void ShiftLeft(InputAction.CallbackContext context)
    {
        if (desiredLane > 0)
        {
            desiredLane--;
        }
    }

    public void SwipeShiftLeft()
    {
        if (desiredLane > 0)
        {
            desiredLane--;
        }
    }

    private void ShiftRight(InputAction.CallbackContext context)
    {
        if (desiredLane < 2)
        {
            desiredLane++;
        }
    }

    public void SwipeShiftRight()
    {
        if (desiredLane < 2)
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


}
