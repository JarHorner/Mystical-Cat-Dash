using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Sprite magnetSprite;
    [SerializeField] private AudioClip collectPowerupSound;

    // starts the timer of the shield powerup
    public void MagnetBuff()
    {
        SoundManager.Instance.Play(collectPowerupSound);

        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = magnetSprite;
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentMagnetTime = Powerups.Instance.magnetLength;
        Powerups.Instance.magnetPickedUp = true;

        Destroy(this.gameObject);
    }

}
