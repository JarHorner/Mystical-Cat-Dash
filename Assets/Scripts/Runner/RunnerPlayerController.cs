using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    idle,
    run,
    slide,
    jump,
    dead,
}
public class RunnerPlayerController : MonoBehaviour
{
    public PlayerState currentState;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction primaryContact, primaryPosition;
    public Animator animator;
    public float playIdleAnimTimer = 6f;
    private Vector2 initialPos;
    private Vector2 currentPos => primaryPosition.ReadValue<Vector2>();

    [SerializeField] private CharacterController controller;
    [SerializeField] private PowerupSpawner powerupSpawner;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip swapLaneSound;
    [SerializeField] private AudioClip jumpSound;
    private Vector3 direction;
    public int desiredLane = 1;
    public float laneSwapSpeed = 25;
    public float laneDistance = 4;
    public float jumpForce;
    public float slideTime;
    public float gravity = -20;
    private bool hitPortal = false;
    public GameObject multiplierBuffVFX;
    public GameObject magnetBuffVFX;
    public GameObject shieldBuffVFX;
    public GameObject speedBuffVFX;
    private IEnumerator invulIEnumerator;

    void Awake()
    {
        // Getting action and and setting up each action for the player
        var playerActionMap = inputMaster.FindActionMap("RunnerPlayer");

        primaryContact = playerActionMap.FindAction("PrimaryContact");
        primaryPosition = playerActionMap.FindAction("PrimaryPosition");

        hitPortal = false;
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
        powerupSpawner = GameObject.Find("PowerupSpawner").GetComponent<PowerupSpawner>();
        Time.timeScale = 1;
    }

    void Update()
    {
        // when idle, every couple of seconds, an idle animation will play
        if (currentState == PlayerState.idle)
        {
            playIdleAnimTimer -= Time.deltaTime;
            if (playIdleAnimTimer <= 0)
            {
                int randomIdleAnimChoice = Random.Range(0, 2); // 0 or 1

                if (randomIdleAnimChoice == 0)
                {
                    animator.SetTrigger("IdleAction");
                }
                else if (randomIdleAnimChoice == 1)
                {
                    animator.SetTrigger("IdleAction2");
                }
                playIdleAnimTimer = 6f;
            }
        }
        else
        {
            // when not idle the game will slowly increase speed until capped
            if ((currentState != PlayerState.idle && currentState != PlayerState.dead) || Powerups.Instance.speedPickedUp)
            {
                // increases speed of player slowly as game progresses to a maximum amount
                if (GameManager.Instance.forwardSpeed < GameManager.Instance.maximumForwardSpeed)
                {
                    GameManager.Instance.forwardSpeed += 0.1f * Time.deltaTime;
                }
            }

            if (Powerups.Instance.destroyMultiplierVFX)
            {
                Powerups.Instance.destroyMultiplierVFX = false;
                Destroy(multiplierBuffVFX);
            }
            else if (Powerups.Instance.destroyMagnetVFX)
            {
                Powerups.Instance.destroyMagnetVFX = false;
                Destroy(magnetBuffVFX);
            }
            else if (Powerups.Instance.destroyShieldVFX)
            {
                Powerups.Instance.destroyShieldVFX = false;
                Destroy(shieldBuffVFX);
            }
            else if (Powerups.Instance.destroySpeedVFX)
            {
                Powerups.Instance.destroySpeedVFX = false;
                Destroy(speedBuffVFX);
            }

            // moves the player along the z axis, speed based on whether player has speed buff
            if (!Powerups.Instance.speedPickedUp)
            {
                direction.z = GameManager.Instance.forwardSpeed;
            }
            else
            {
                direction.z = (GameManager.Instance.forwardSpeed * Powerups.Instance.speedValue);
            }


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
            {
                return;
            }
            else
            {
                Vector3 diff = targetPosition - transform.position;
                Vector3 moveDir = diff.normalized * laneSwapSpeed * Time.deltaTime;
                if (currentState != PlayerState.dead)
                {
                    if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                    {
                        controller.Move(moveDir);
                    }
                    else
                    {
                        controller.Move(diff);
                    }
                }
            }
        }
    }

    // ensures the player is moving at a fixed amount
    void FixedUpdate()
    {
        if (!GameManager.Instance.isGameStarted || currentState == PlayerState.dead)
            return;

        controller.Move(direction * Time.fixedDeltaTime);
    }

    // based on the collider hit, does something.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Object")
        {
            SoundManager.Instance.Play(hitSound);
            // if player has shield powerup, he does not die and enters invulnerable state
            if (Powerups.Instance.shieldPickedUp)
            {
                StartCoroutine(Powerups.Instance.PlayerBlink(0.1f));
                StartCoroutine(Invulnerable(2f));
                Powerups.Instance.currentShieldTime = 0;
            }
            else
            {
                GameManager.Instance.gameOver = true;

                animator.SetBool("Run", false);
                currentState = PlayerState.dead;
                animator.SetTrigger("Die");


                Powerups.Instance.currentMultiplierTime = 0;
                Powerups.Instance.currentMagnetTime = 0;
                Powerups.Instance.currentShieldTime = 0;
                Powerups.Instance.currentSpeedTime = 0;
            }
        }
        else if (hit.transform.tag == "Portal" && !hitPortal)
        {
            hitPortal = true;

            GameManager.Instance.SwitchDimensions();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Coin coin = other.gameObject.GetComponent<Coin>();
            coin.CoinScored();
        }
        else if (other.tag == "Multiplier")
        {
            Multiplier multiplier = other.gameObject.GetComponent<Multiplier>();
            if (!Powerups.Instance.multiplyPickedUp)
            {
                // gives the buff VFX to the player
                multiplierBuffVFX = multiplier.gameObject.transform.GetChild(0).gameObject;
                multiplier.gameObject.transform.GetChild(0).parent = this.gameObject.transform;

                multiplier.MultiplyBuff();
            }
            else
            {
                multiplier.ExtendBuff();
            }

        }
        else if (other.tag == "Magnet")
        {
            Magnet magnet = other.gameObject.GetComponent<Magnet>();

            if (!Powerups.Instance.magnetPickedUp)
            {
                // gives the buff VFX to the player
                magnetBuffVFX = magnet.gameObject.transform.GetChild(0).gameObject;
                magnet.gameObject.transform.GetChild(0).parent = this.gameObject.transform;

                magnet.MagnetBuff();
            }
            else
            {
                magnet.ExtendBuff();
            }
        }
        else if (other.tag == "Shield")
        {
            Shield shield = other.gameObject.GetComponent<Shield>();

            if (!Powerups.Instance.shieldPickedUp)
            {
                // gives the buff VFX to the player
                shieldBuffVFX = shield.gameObject.transform.GetChild(0).gameObject;
                shield.gameObject.transform.GetChild(0).parent = this.gameObject.transform;

                shield.ShieldBuff();
            }
            else
            {
                shield.ExtendBuff();
            }
        }
        else if (other.tag == "Speed")
        {
            Speed speed = other.gameObject.GetComponent<Speed>();
            invulIEnumerator = Invulnerable(Powerups.Instance.speedLength);

            if (!Powerups.Instance.speedPickedUp)
            {
                invulIEnumerator = Invulnerable(Powerups.Instance.speedLength);
                StartCoroutine(invulIEnumerator);
                // gives the buff VFX to the player
                speedBuffVFX = speed.gameObject.transform.GetChild(0).gameObject;
                speed.gameObject.transform.GetChild(0).parent = this.gameObject.transform;

                speed.SpeedBuff();
            }
            else
            {
                StopCoroutine(invulIEnumerator);

                invulIEnumerator = Invulnerable(Powerups.Instance.speedLength);
                StartCoroutine(invulIEnumerator);
                speed.ExtendBuff();
            }
        }
    }

    public IEnumerator Invulnerable(float invulnerableTime)
    {
        Physics.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(invulnerableTime);
        Physics.IgnoreLayerCollision(6, 7, false);
    }

    // shifts the players lane to the left
    public void SwipeShiftLeft()
    {
        if (desiredLane > 0 && currentState != PlayerState.idle)
        {
            SoundManager.Instance.Play(swapLaneSound);
            desiredLane--;
        }
    }

    // shifts the players lane to the right
    public void SwipeShiftRight()
    {
        if (desiredLane < 2 && currentState != PlayerState.idle)
        {
            SoundManager.Instance.Play(swapLaneSound);
            desiredLane++;
        }
    }

    // jumps the player up
    public void SwipeJump()
    {
        if (controller.isGrounded)
        {
            SoundManager.Instance.Play(jumpSound);
            StartCoroutine(RunnerJump());
            direction.y = jumpForce;
        }
    }

    private IEnumerator RunnerJump()
    {
        direction.y = jumpForce;
        currentState = PlayerState.jump;
        animator.SetBool("Run", false);
        animator.SetTrigger("Jump");

        yield return new WaitForSeconds(1f);

        if (currentState != PlayerState.dead)
        {
            currentState = PlayerState.run;
            animator.SetTrigger("Land");
            animator.SetBool("Run", true);
        }
    }


    // slides the player along the ground
    public void SwipeSlide()
    {
        if (controller.isGrounded)
        {
            SoundManager.Instance.Play(swapLaneSound);
            StartCoroutine(RunnerSlide());
        }
    }

    private IEnumerator RunnerSlide()
    {
        currentState = PlayerState.slide;
        animator.SetBool("Run", false);
        animator.SetTrigger("SleepStart");

        // these are needed as I cannot edit the animations themselves, this would be in the animations if I could.
        this.gameObject.transform.position =
        controller.center = new Vector3(0.002f, 0.015f, 0.04f);
        controller.radius = 0.01f;
        controller.height = 0f;

        yield return new WaitForSeconds(slideTime);

        if (currentState != PlayerState.dead)
        {
            currentState = PlayerState.run;
            animator.SetTrigger("SleepEnd");
            animator.SetBool("Run", true);
        }

        // these are needed as I cannot edit the animations themselves, this would be in the animations if I could.
        controller.center = new Vector3(0f, 0.025f, 0f);
        controller.radius = 0.01f;
        controller.height = 0.055f;
    }


}
