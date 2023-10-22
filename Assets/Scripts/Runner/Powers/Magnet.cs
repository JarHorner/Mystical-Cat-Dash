using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Powerups powerups;

    // starts the timer of the shield powerup
    public void MagnetBuff()
    {
        powerups.currentMagnetTime = powerups.magnetLength;
        powerups.magnetPickedUp = true;
    }

}
