using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    private int[] lanes = new int[] { -2, 0, 2 };
    private int[] heights = new int[] { 1, 3, 5 };
    [SerializeField] private Transform runnerPlayerTransform;
    [SerializeField] private float zSpawnDistance = 140f;


    void Start()
    {
        runnerPlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

    }

    public void DeterminePowerupSpawn()
    {
        float powerupSpawnPercentage = Random.Range(1, 101); // range of 1 - 100

        SpawnPowerup();

        // if (tilesSpawnedUntilPortal >= 16 && tilesSpawnedUntilPortal >= 30 && portalSpawnPercentage >= 90)
        // {
        //      SpawnPowerup();
        // }
        // else if (tilesSpawnedUntilPortal >= 31 && tilesSpawnedUntilPortal >= 45 && portalSpawnPercentage >= 85)
        // {
        //      SpawnPowerup();
        // }
        // else if (tilesSpawnedUntilPortal > 46 && portalSpawnPercentage >= 80)
        // {
        //      SpawnPowerup();
        // }
    }

    private void SpawnPowerup()
    {
        GameObject powerup = DeterminePowerup();
        Vector3 location = DeterminePowerupLocation();

        Instantiate(powerup, location, transform.rotation);


    }

    private GameObject DeterminePowerup()
    {
        int chosenPowerup = Random.Range(0, 4); // range of 0 - 3 for easy array get

        return powerups[chosenPowerup];
    }

    private Vector3 DeterminePowerupLocation()
    {
        int randomIndexForLane = Random.Range(0, 3); // range of 0 - 2 for lanes array
        int randomLane = lanes[randomIndexForLane];

        int randomIndexForHeight = Random.Range(0, 2); // range of 0 - 2 for Height array
        int randomHeight = heights[randomIndexForHeight];

        Vector3 powerupLocation = new Vector3(randomLane, randomHeight, runnerPlayerTransform.position.z + zSpawnDistance);

        return powerupLocation;
    }
}
