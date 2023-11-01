using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private Sprite speedSprite;
    [SerializeField] private AudioClip collectPowerupSound;
    [SerializeField] private AudioClip speedUpSound;

    // starts the timer of the speed powerup
    public void SpeedBuff()
    {
        SoundManager.Instance.Play(collectPowerupSound);

        SoundManager.Instance.Play(speedUpSound);

        // shows the buff symbol on the screen
        GameUI.Instance.powerupImages[3].enabled = true;

        Powerups.Instance.currentSpeedTime = Powerups.Instance.speedLength;
        Powerups.Instance.speedPickedUp = true;

        Destroy(this.gameObject);
    }

    public void ExtendBuff()
    {
        Powerups.Instance.currentSpeedTime = Powerups.Instance.speedLength;

        Destroy(this.gameObject);
    }

}
