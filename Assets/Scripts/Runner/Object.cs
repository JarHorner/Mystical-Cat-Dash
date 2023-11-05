using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin Colliding");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
