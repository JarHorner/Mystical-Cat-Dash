using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] private int pointsWorth;
    [SerializeField] private AudioClip collectCoinSound;
    public float moveSpeed = 5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Powerups.Instance.magnetPickedUp)
        {
            // Calculate the distance between this object and the target object.
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < Powerups.Instance.magnetSize)
            {
                // Calculate the direction to the target.
                Vector3 direction = (player.position - transform.position).normalized;

                // Move the object towards the target using the specified moveSpeed.
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                //CoinScored();
            }
        }
    }

    // adds score to calculated score
    public void CoinScored()
    {
        SoundManager.Instance.Play(collectCoinSound);
        GameManager.Instance.Scored(pointsWorth);

        Destroy(this.gameObject);
    }

}
