using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] private int pointsWorth;

    // adds score to calculated score
    public void Scored()
    {
        GameManager.Instance.calculatedScore += pointsWorth;
        GameUI.Instance.score.GetComponent<TMP_Text>().text = GameManager.Instance.calculatedScore.ToString();
        
        Debug.Log("Coin Collected");
        Destroy(this.gameObject);
    }
}
