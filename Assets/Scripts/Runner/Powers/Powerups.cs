using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Powerups : MonoBehaviour
{
    public static Powerups Instance { get; private set; }

    [SerializeField] private GameObject runnerPlayer;
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    [SerializeField] private float powerupAlmostDoneTimer = 2f; // used to determine when to blink and for how long
    public bool powerupAlmostDone = false; // used to start the blink coroutine

    // Multiplier powerup variables
    [SerializeField] private GameObject multiplyBuff;
    public float multiplierLength;
    public int multiplyValue = 2;
    public float currentMultiplierTime;
    public bool multiplyPickedUp = false;
    public bool destroyMultiplierVFX = false;

    // Magnet powerup variables    
    [SerializeField] private GameObject magnetBuff;
    public float magnetLength;
    public float magnetSize = 10;
    public float currentMagnetTime;
    public bool magnetPickedUp = false;
    public bool destroyMagnetVFX = false;


    // Shield powerup variables
    [SerializeField] private GameObject shieldBuff;
    public float shieldLength;
    public float currentShieldTime;
    public bool shieldPickedUp = false;
    public bool destroyShieldVFX = false;

    // Speed powerup variables
    [SerializeField] private GameObject speedBuff;
    public float speedLength;
    public int speedValue = 2;
    public float currentSpeedTime;
    public bool speedPickedUp = false;
    public bool destroySpeedVFX = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // need to ensure the runnerPlayer varible is assigned
        if (SceneManager.GetActiveScene().name == "Runner" && runnerPlayer == null)
        {
            runnerPlayer = GameObject.FindWithTag("Player");
            playerMesh = runnerPlayer.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        }

        if (multiplyPickedUp)
        {
            currentMultiplierTime -= Time.deltaTime;

            if (currentMultiplierTime <= 0)
            {
                destroyMultiplierVFX = true;

                multiplyPickedUp = false;
                powerupAlmostDone = false;

                GameUI.Instance.powerupImages[0].enabled = false;

                currentMultiplierTime = 0;
            }
        }

        if (magnetPickedUp)
        {
            currentMagnetTime -= Time.deltaTime;
            if (currentMagnetTime <= 0)
            {
                destroyMagnetVFX = true;

                magnetPickedUp = false;

                GameUI.Instance.powerupImages[1].enabled = false;

                currentMagnetTime = 0;
            }
        }

        if (shieldPickedUp)
        {
            currentShieldTime -= Time.deltaTime;
            if (currentShieldTime <= 0)
            {
                destroyShieldVFX = true;

                shieldPickedUp = false;

                GameUI.Instance.powerupImages[2].enabled = false;

                currentShieldTime = 0;
            }
        }

        if (speedPickedUp)
        {
            currentSpeedTime -= Time.deltaTime;

            if (!powerupAlmostDone && currentSpeedTime <= powerupAlmostDoneTimer)
            {
                powerupAlmostDone = true;
                StartCoroutine(PlayerBlink(0.1f));
            }

            if (currentSpeedTime <= 0)
            {
                destroySpeedVFX = true;
                
                speedPickedUp = false;
                powerupAlmostDone = false;

                GameUI.Instance.powerupImages[3].enabled = false;

                currentSpeedTime = 0;
            }
        }
    }

    public IEnumerator PlayerBlink(float waitTime)
    {
        float counter = powerupAlmostDoneTimer / (waitTime * 2);

        for (int i = 0; i <= counter; i++)
        {
            playerMesh.enabled = false;
            yield return new WaitForSeconds(waitTime);
            playerMesh.enabled = true;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
