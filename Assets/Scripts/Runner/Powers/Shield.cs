using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    
    // starts the timer of the shield powerup
    public void ShieldHit()
    {
        powerups.currentShieldTime = powerups.shieldLength;
        powerups.shieldPickedUp = true;
    }

}
