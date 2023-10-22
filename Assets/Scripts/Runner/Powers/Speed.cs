using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    
    // starts the timer of the speed powerup
    public void SpeedBuff()
    {
        powerups.currentSpeedTime = powerups.speedLength;
        powerups.speedPickedUp = true;
        StartCoroutine(powerups.Invulnerable(powerups.speedLength));
    }

}
