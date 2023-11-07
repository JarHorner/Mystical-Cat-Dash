using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private float[] lanes = new float[] { -2f, 0f, 2f };
    [SerializeField] private float[] spawnZDistances;
    [SerializeField] private float distanceBetweenCoins;
    private bool spawnedStartCoins = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //int willSpawnCoins = Random.Range(0, 2); // 0 to 1 = 1/2 chance to spawn coins
            //if (willSpawnCoins == 0)
            //{
                Debug.Log("Spawning Coins");
                StartCoroutine(SpawnCoins(other.gameObject));
            //}

        }

    }

    IEnumerator SpawnCoins(GameObject player)
    {
        int numOfCoins = Random.Range(4, 9); // 4 to 8 coins

        int randomIndexForLane = Random.Range(0, 3); // range of 0 - 2 for lanes array
        float randomLane = lanes[randomIndexForLane];

        int randomIndexForSpawnDistance = Random.Range(0, 3); // 0 - 2, so 3 options for distance
        float SpawnDistance = spawnZDistances[randomIndexForSpawnDistance];


        float currentDistanceBetweenCoins = distanceBetweenCoins;

        for (int i = 0; i <= numOfCoins; i++)
        {
            GameObject spawnedCoin = Instantiate(coin, new Vector3(randomLane, 1f, (player.transform.position.z + SpawnDistance + currentDistanceBetweenCoins)), Quaternion.identity);
            currentDistanceBetweenCoins += distanceBetweenCoins;
        }

        yield return null;
    }
}
