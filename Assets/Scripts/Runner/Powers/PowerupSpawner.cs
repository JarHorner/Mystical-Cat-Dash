using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerups;
    [SerializeField] private List<GameObject> powerupParent;

    void Start()
    {
        SpawnPowerup();
    }

    private void SpawnPowerup()
    {
        int powerupSpawnPercentage = Random.Range(0, 8); // range of 0 to 7 = 12% chance to spawn powerup

        if (powerupSpawnPercentage == 0)
        {
            int powerupSpawnLocation = Random.Range(0, 3); // range of 0-2 to get proper location from list
            GameObject powerup = DeterminePowerup();
            powerupParent[powerupSpawnLocation].SetActive(true);

            GameObject spawnedPowerup = Instantiate(powerup, new Vector3(powerupParent[powerupSpawnLocation].transform.position.x, powerupParent[powerupSpawnLocation].transform.position.y, powerupParent[powerupSpawnLocation].transform.position.z), Quaternion.identity);
            spawnedPowerup.transform.parent = powerupParent[powerupSpawnLocation].transform;
        }
    }

    private GameObject DeterminePowerup()
    {
        int chosenPowerup = Random.Range(0, 4); // range of 0 - 3 for easy array get

        return powerups[chosenPowerup];
    }

}
