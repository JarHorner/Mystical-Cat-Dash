using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] private int pointsWorth;

    // adds score to calculated score
    public void CoinScored()
    {
        GameManager.Instance.Scored(pointsWorth);
        
        Debug.Log("Coin Collected");
        Destroy(this.gameObject);
    }
}
