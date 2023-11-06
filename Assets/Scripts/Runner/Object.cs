using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private float changeCoinYPos = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Coin Colliding");
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, changeCoinYPos, other.gameObject.transform.position.z);
        }

    }

}
