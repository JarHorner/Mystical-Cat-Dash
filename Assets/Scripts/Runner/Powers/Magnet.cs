using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    [SerializeField] private Sprite magnetSprite;

    // starts the timer of the shield powerup
    public void MagnetBuff()
    {
        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = magnetSprite;
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentMagnetTime = Powerups.Instance.magnetLength;
        Powerups.Instance.magnetPickedUp = true;
    }

}
