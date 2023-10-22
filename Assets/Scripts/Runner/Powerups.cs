using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public PowerUpScriptableObject multiplierValues;
    public PowerUpScriptableObject magnetValues;
    public PowerUpScriptableObject shieldValues;
    public PowerUpScriptableObject speedValues;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        multiplierValues.currentPowerupTime = 0;
        magnetValues.currentPowerupTime = 0;
        shieldValues.currentPowerupTime = 0;
        speedValues.currentPowerupTime = 0;
    }
    public void MultiplyScore()
    {
        
    }
}
