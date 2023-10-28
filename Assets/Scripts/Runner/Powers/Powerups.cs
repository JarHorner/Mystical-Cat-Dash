using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public static Powerups Instance { get; private set; }

    public GameObject player;

    // Multiplier powerup variables
    public GameObject multiplyBuff;
    public float multiplierLength;
    public int multiplyValue = 2;
    public float currentMultiplierTime;
    public bool multiplyPickedUp;

    // Magnet powerup variables    
    public GameObject magnetBuff;
    public float magnetLength;
    public float magnetSize = 5;
    public float currentMagnetTime;
    public bool magnetPickedUp;


    // Shield powerup variables
    public GameObject shieldBuff;
    public float shieldLength;
    public float currentShieldTime;
    public bool shieldPickedUp;

    // Speed powerup variables
    public GameObject speedBuff;
    public float speedLength;
    public int speedValue = 2;
    public float currentSpeedTime;
    public bool speedPickedUp;

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

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (multiplyPickedUp)
        {
            currentMultiplierTime -= Time.deltaTime;
            if (currentMultiplierTime <= 0)
            {
                Destroy(player.transform.GetChild(2).gameObject);

                multiplyPickedUp = false;

                GameUI.Instance.powerupImage.enabled = false;

                currentMultiplierTime = 0;
            }
        }

        if (magnetPickedUp)
        {
            currentMagnetTime -= Time.deltaTime;
            if (currentMagnetTime <= 0)
            {
                Destroy(player.transform.GetChild(2).gameObject);

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
                Destroy(player.transform.GetChild(2).gameObject);

                shieldPickedUp = false;

                GameUI.Instance.powerupImage.enabled = false;
                GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

                currentShieldTime = 0;
            }
        }

        if (speedPickedUp)
        {
            currentSpeedTime -= Time.deltaTime;
            if (currentSpeedTime <= 0)
            {
                Destroy(player.transform.GetChild(2).gameObject);

                speedPickedUp = false;

                GameUI.Instance.powerupImage.enabled = false;
                GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

                currentSpeedTime = 0;
            }
        }
    }

    public IEnumerator Invulnerable(float invulnerableTime)
    {
        Physics.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(invulnerableTime);
        Physics.IgnoreLayerCollision(6, 7, false);
    }
}
