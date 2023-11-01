using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Sprite shieldSprite;
    [SerializeField] private AudioClip collectPowerupSound;

    // starts the timer of the shield powerup
    public void ShieldBuff()
    {
        SoundManager.Instance.Play(collectPowerupSound);

        // shows the buff symbol on the screen
        GameUI.Instance.powerupImages[2].enabled = true;

        Powerups.Instance.currentShieldTime = Powerups.Instance.shieldLength;
        Powerups.Instance.shieldPickedUp = true;

        Destroy(this.gameObject);
    }

    public void ExtendBuff()
    {
        Powerups.Instance.currentShieldTime = Powerups.Instance.shieldLength;

        Destroy(this.gameObject);
    }
}
