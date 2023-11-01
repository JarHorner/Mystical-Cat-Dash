using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    // these are the delegate events for the input actions used by the player when playing the runner section.
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    public delegate void TapStart();
    public event TapStart OnTapStart;
    #endregion

    public static InputManager Instance { get; private set; }
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction primaryContact, primaryPosition, tap;
    private Player player;
    [SerializeField] private Camera mainCamera;
    private bool removing = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            removing = true;
            Destroy(gameObject);
        }
        var playerActionMap = inputMaster.FindActionMap("RunnerPlayer");

        primaryContact = playerActionMap.FindAction("PrimaryContact");
        primaryPosition = playerActionMap.FindAction("PrimaryPosition");
        tap = playerActionMap.FindAction("StartGame");
    }

    void OnEnable()
    {
        primaryPosition.Enable();
        primaryContact.Enable();
        primaryContact.started += ctx => StartTouchPrimary(ctx);
        primaryContact.canceled += ctx => EndTouchPrimary(ctx);

        tap.Enable();
        tap.started += ctx => StartGameTap(ctx);
        tap.canceled += ctx => StartGameTap(ctx);
    }

    void OnDisable()
    {
        if (!removing)
        {
            primaryContact.started -= ctx => StartTouchPrimary(ctx);
            primaryContact.canceled -= ctx => EndTouchPrimary(ctx);
            primaryContact.Disable();
            primaryPosition.Disable();

            tap.started -= ctx => StartGameTap(ctx);
            tap.canceled -= ctx => StartGameTap(ctx);
            tap.Disable();
        }
    }

    void Update()
    {
        // needed to ensure the mainCamera varible is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCamera, primaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera, primaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    private void StartGameTap(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.isGameStarted)
        {
            OnTapStart();
        }
    }

    // helper method to map the touch based on real-world position on screen
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, player.RunnerPlayer.PrimaryPosition.ReadValue<Vector2>());
    }
}
