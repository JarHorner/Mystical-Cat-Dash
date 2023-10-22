using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    
    // starts the timer of the multiplier powerup
    public void MultiplyScore()
    {
        powerups.currentMultiplierTime = powerups.multiplierLength;
        powerups.multiplyPickedUp = true;
    }
}
