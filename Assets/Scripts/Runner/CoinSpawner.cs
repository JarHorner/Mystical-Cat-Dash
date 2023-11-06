using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private float[] lanes = new float[] { -2f, 0f, 2f };
    [SerializeField] private float spawnZDistance;
    [SerializeField] private float distanceBetweenCoins;
    //private float baseDistanceBetweenCoins;

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
        if (other.tag == "Player")
        {
            int willSpawnCoins = Random.Range(0, 2); // 0 to 1 = 1/2 chance to spawn coins
            if (willSpawnCoins == 0)
            {
                Debug.Log("Spawning Coins");
                StartCoroutine(SpawnCoins(other.gameObject));
            }

        }

    }

    IEnumerator SpawnCoins(GameObject player)
    {
        int numOfCoins = Random.Range(1, 7); // 1 to 6 coins

        int randomIndexForLane = Random.Range(0, 3); // range of 0 - 2 for lanes array
        float randomLane = lanes[randomIndexForLane];

        float currentDistanceBetweenCoins = distanceBetweenCoins;

        for (int i = 0; i <= numOfCoins; i++)
        {
            GameObject spawnedCoin = Instantiate(coin, new Vector3(randomLane, 1f, (player.transform.position.z + spawnZDistance + currentDistanceBetweenCoins)), Quaternion.identity);
            currentDistanceBetweenCoins += distanceBetweenCoins;
        }

        //distanceBetweenCoins = baseDistanceBetweenCoins;
        yield return null;
    }
}
