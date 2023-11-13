using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject swapGameTile;
    public GameObject startTile;
    public int swapGameTileSpawnNum;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numOfTiles = 5;
    public int tilesSpawnedUntilPortal = 0;
    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        for (int i = 0; i < numOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnStartGameTile();
            }
            // For testing flappy bird gameplay
            // if (i == swapGameTileSpawnNum)
            // {
            //     SpawnPortalGameTile();
            // }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numOfTiles * tileLength))
        {
            if (tilesSpawnedUntilPortal == swapGameTileSpawnNum) // this will be removed
            {
                SpawnPortalGameTile();
            }
            else
            {
                DetermineTileSpawn();
            }
            DeleteTile();
        }
    }

    private void DetermineTileSpawn()
    {
        int portalSpawnPercentage = Random.Range(1, 101); // range of 1 - 100
        Debug.Log("portal tile spawn num: " + portalSpawnPercentage);

        if (tilesSpawnedUntilPortal >= 10 && tilesSpawnedUntilPortal <= 20 && portalSpawnPercentage >= 90) // 10% chance
        {
            SpawnPortalGameTile();
        }
        else if (tilesSpawnedUntilPortal >= 21 && tilesSpawnedUntilPortal <= 31 && portalSpawnPercentage >= 80) // 20% chance
        {
            SpawnPortalGameTile();
        }
        else if (tilesSpawnedUntilPortal >= 32 && portalSpawnPercentage >= 70) // 30% chance
        {
            SpawnPortalGameTile();
        }
        else
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    public void SpawnTile(int index)
    {
        tilesSpawnedUntilPortal++;
        GameObject tile = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    public void SpawnPortalGameTile()
    {
        // resets number so tons of portals do not open
        tilesSpawnedUntilPortal = 0;
        Debug.Log("Portal Spawned");
        GameObject tile = Instantiate(swapGameTile, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    public void SpawnStartGameTile()
    {
        GameObject tile = Instantiate(startTile, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
