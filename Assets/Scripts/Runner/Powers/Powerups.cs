using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public static Powerups Instance { get; private set; }

    // Multiplier powerup variables
    public float multiplierLength;
    public int multiplyValue = 2;
    public float currentMultiplierTime;
    public bool multiplyPickedUp;

    // Magnet powerup variables
    public float magnetLength;
    public float currentMagnetTime;
    public bool magnetPickedUp;

    // Shield powerup variables
    public float shieldLength;
    public float currentShieldTime;
    public bool shieldPickedUp;

    // Speed powerup variables
    public float speedLength;
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
    void Update()
    {
        if (multiplyPickedUp)
        {
            currentMultiplierTime -= Time.deltaTime;
            if (currentMultiplierTime <= 0)
            {
                multiplyPickedUp = false;
                currentMultiplierTime = 0;
            }
        }

        if (magnetPickedUp)
        {
            currentMagnetTime -= Time.deltaTime;
            if (currentMagnetTime <= 0)
            {
                magnetPickedUp = false;
                currentMagnetTime = 0;
            }
        }

        if (shieldPickedUp)
        {
            currentShieldTime -= Time.deltaTime;
            if (currentShieldTime <= 0)
            {
                shieldPickedUp = false;
                currentShieldTime = 0;
            }
        }

        if (speedPickedUp)
        {
            currentSpeedTime -= Time.deltaTime;
            if (currentSpeedTime <= 0)
            {
                speedPickedUp = false;
                currentSpeedTime = 0;
            }
        }
    }
}
