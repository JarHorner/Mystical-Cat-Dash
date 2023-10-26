using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Powerups powerups;
    [SerializeField] private Sprite shieldSprite;

    void Start()
    {
        powerups = GameObject.Find("GameManager").GetComponent<Powerups>();
    }
    
    // starts the timer of the shield powerup
    public void ShieldBuff()
    {
        // shows the buff symbol on the screen
        GameUI.Instance.powerupImage.sprite = shieldSprite;
        GameUI.Instance.powerupImage.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
        GameUI.Instance.powerupImage.enabled = true;

        Powerups.Instance.currentShieldTime = Powerups.Instance.shieldLength;
        Powerups.Instance.shieldPickedUp = true;
    }

}
