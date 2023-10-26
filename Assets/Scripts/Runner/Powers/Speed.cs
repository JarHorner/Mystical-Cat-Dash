using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    [SerializeField] private Sprite speedSprite;


    void Start()
    {
        powerups = GameObject.Find("GameManager").GetComponent<Powerups>();
    }

    // starts the timer of the speed powerup
    public void SpeedBuff()
    {
        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = speedSprite;
        GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1.5f, 2f, 1);
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentSpeedTime = Powerups.Instance.speedLength;
        Powerups.Instance.speedPickedUp = true;
        StartCoroutine(powerups.Invulnerable(powerups.speedLength));
    }

}
