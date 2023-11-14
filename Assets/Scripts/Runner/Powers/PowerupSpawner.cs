using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerups;
    [SerializeField] private GameObject powerupParent;

    void Start()
    {
        SpawnPowerup();
    }

    private void SpawnPowerup()
    {
        float powerupSpawnPercentage = Random.Range(0, 2); // range of 0 to 9 = 10% chance to spawn powerup

        if (powerupSpawnPercentage == 0)
        {
            Debug.Log("Powerup spawned");
            GameObject powerup = DeterminePowerup();
            powerupParent.SetActive(true);

            GameObject spawnedPowerup = Instantiate(powerup, new Vector3(powerupParent.transform.position.x, 1f, powerupParent.transform.position.z), Quaternion.identity);
            spawnedPowerup.transform.parent = powerupParent.transform;
        }
    }

    private GameObject DeterminePowerup()
    {
        int chosenPowerup = Random.Range(0, 4); // range of 0 - 3 for easy array get

        return powerups[chosenPowerup];
    }

}
