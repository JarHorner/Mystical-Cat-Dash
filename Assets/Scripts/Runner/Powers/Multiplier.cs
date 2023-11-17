using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private Sprite multiplySprite;
    [SerializeField] private AudioClip collectPowerupSound;

    // starts the timer of the multiplier powerup
    public void MultiplyBuff()
    {
        SoundManager.Instance.Play(collectPowerupSound, 0.4f);

        // shows the buff symbol on the screen
        GameUI.Instance.powerupImages[0].enabled = true;

        Powerups.Instance.currentMultiplierTime = Powerups.Instance.multiplierLength;
        Powerups.Instance.multiplyPickedUp = true;

        Destroy(this.gameObject);
    }

    public void ExtendBuff()
    {
        Powerups.Instance.currentMultiplierTime = Powerups.Instance.multiplierLength;

        Destroy(this.gameObject);
    }
}
