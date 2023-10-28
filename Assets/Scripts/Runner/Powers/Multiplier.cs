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
        SoundManager.Instance.Play(collectPowerupSound);

        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = multiplySprite;
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentMultiplierTime = Powerups.Instance.multiplierLength;
        Powerups.Instance.multiplyPickedUp = true;

        Destroy(this.gameObject);
    }
}
