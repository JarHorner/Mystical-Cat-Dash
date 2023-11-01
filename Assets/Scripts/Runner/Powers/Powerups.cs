using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Powerups : MonoBehaviour
{
    public static Powerups Instance { get; private set; }

    [SerializeField] private GameObject runnerPlayer;
    [SerializeField] private SkinnedMeshRenderer playerMesh;
    [SerializeField] private float powerupAlmostDoneTimer = 2f;
    public bool powerupAlmostDone = false;

    // Multiplier powerup variables
    [SerializeField] private GameObject multiplyBuff;
    public float multiplierLength;
    public int multiplyValue = 2;
    public float currentMultiplierTime;
    public bool multiplyPickedUp = false;

    // Magnet powerup variables    
    [SerializeField] private GameObject magnetBuff;
    public float magnetLength;
    public float magnetSize = 10;
    public float currentMagnetTime;
    public bool magnetPickedUp = false;


    // Shield powerup variables
    [SerializeField] private GameObject shieldBuff;
    public float shieldLength;
    public float currentShieldTime;
    public bool shieldPickedUp = false;

    // Speed powerup variables
    [SerializeField] private GameObject speedBuff;
    public float speedLength;
    public int speedValue = 2;
    public float currentSpeedTime;
    public bool speedPickedUp = false;

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
                Destroy(runnerPlayer.transform.GetChild(2).gameObject);

                multiplyPickedUp = false;
                powerupAlmostDone = false;

                GameUI.Instance.powerupImage.enabled = false;

                currentMultiplierTime = 0;
            }
        }

        if (magnetPickedUp)
        {
            currentMagnetTime -= Time.deltaTime;
            if (currentMagnetTime <= 0)
            {
                Destroy(runnerPlayer.transform.GetChild(2).gameObject);

                magnetPickedUp = false;

                GameUI.Instance.powerupImage.enabled = false;

                currentMagnetTime = 0;
            }
        }

        if (shieldPickedUp)
        {
            currentShieldTime -= Time.deltaTime;
            if (currentShieldTime <= 0)
            {
                Destroy(runnerPlayer.transform.GetChild(2).gameObject);

                shieldPickedUp = false;

                GameUI.Instance.powerupImage.enabled = false;
                GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

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
                Destroy(runnerPlayer.transform.GetChild(2).gameObject);

                speedPickedUp = false;
                powerupAlmostDone = false;

                GameUI.Instance.powerupImage.enabled = false;
                GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

                currentSpeedTime = 0;
            }
        }
    }

    IEnumerator PlayerBlink(float waitTime)
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
