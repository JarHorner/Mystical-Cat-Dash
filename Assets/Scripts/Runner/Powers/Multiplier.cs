using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private Sprite multiplySprite;

    // starts the timer of the multiplier powerup
    public void MultiplyBuff()
    {
        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = multiplySprite;
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentMultiplierTime = Powerups.Instance.multiplierLength;
        Powerups.Instance.multiplyPickedUp = true;
        Destroy(this.gameObject);
    }
}
