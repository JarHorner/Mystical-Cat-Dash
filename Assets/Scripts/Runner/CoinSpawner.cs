using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] coinLanes;

    void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        int randomIndexForLane = Random.Range(0, 3); // range of 0 - 2 for lanes array

        coinLanes[randomIndexForLane].SetActive(true);
    }
}
