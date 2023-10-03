using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<FlappyPlayerController>() != null)
        {
            GameManager.Instance.Scored();
        }
    }
}
