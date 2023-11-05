using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedCoin = Instantiate(coin, new Vector3(0f, 0.5f, 20f), Quaternion.identity);

        if (spawnedCoin)
        {
            Debug.Log("Spawned");
        }
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
