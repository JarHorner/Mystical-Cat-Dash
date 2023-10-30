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
        GameUI.Instance.powerupImage.sprite = speedSprite;
        GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1.5f, 2f, 1);
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentSpeedTime = Powerups.Instance.speedLength;
        Powerups.Instance.speedPickedUp = true;

        Destroy(this.gameObject);
    }

}
