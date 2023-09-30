using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    public static InputManager Instance { get; private set; }
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction primaryContact, primaryPosition;
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

        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        var playerActionMap = inputMaster.FindActionMap("RunnerPlayer");

        primaryContact = playerActionMap.FindAction("PrimaryContact");
        primaryPosition = playerActionMap.FindAction("PrimaryPosition");
    }

    void Start()
    {
        //mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void OnEnable()
    {
        primaryPosition.Enable();
        primaryContact.Enable();
        primaryContact.started += ctx => StartTouchPrimary(ctx);
        primaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    void OnDisable()
    {
        if (!removing)
        {
            primaryContact.started -= ctx => StartTouchPrimary(ctx);
            primaryContact.canceled -= ctx => EndTouchPrimary(ctx);
            primaryContact.Disable();
        }
    }

    void Update()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, player.RunnerPlayer.PrimaryPosition.ReadValue<Vector2>());
    }
}
