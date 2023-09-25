using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction shiftLeft, shiftRight, jump;

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -20;

    void Awake()
    {
        var playerActionMap = inputMaster.FindActionMap("RunnerPlayer");

        shiftLeft = playerActionMap.FindAction("ShiftLeft");
        shiftRight = playerActionMap.FindAction("ShiftRight");
        jump = playerActionMap.FindAction("Jump");
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);

    }

    void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void ShiftLeft(InputAction.CallbackContext context)
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

    private void Jump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.performed)
        {
            direction.y = jumpForce;
        }

    }


}
