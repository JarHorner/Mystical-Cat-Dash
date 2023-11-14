using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> coinLanes;

    void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        int coinsSpawnPercentage = Random.Range(0, 2); // range of 0 or 1 = 50% chance to spawn coins

        if (coinsSpawnPercentage == 0)
        {
            int randomIndexForLane = Random.Range(0, 3); // range of 0 - 2 for lanes
            coinLanes[randomIndexForLane].SetActive(true);

            // determines if a different lane of coins spawns
            int secondCoinsSpawnPercentage = Random.Range(0, 4); // range of 0 - 3 = 25% chance to spawn second row of coins
            if (secondCoinsSpawnPercentage == 0)
            {
                coinLanes.RemoveAt(randomIndexForLane);
                randomIndexForLane = Random.Range(0, 2); // range of 0 - 1 for remaining lanes
                coinLanes[randomIndexForLane].SetActive(true);
            }

        }
    }
}
