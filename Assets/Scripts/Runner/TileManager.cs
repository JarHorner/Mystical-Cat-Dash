using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject swapGameTile;
    public GameObject startTile;
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
            DetermineTileSpawn();

            DeleteTile();
        }
    }

    private void DetermineTileSpawn()
    {
        int portalSpawnPercentage = Random.Range(1, 101); // range of 1 - 100

        if (tilesSpawnedUntilPortal >= 10 && tilesSpawnedUntilPortal <= 20 && portalSpawnPercentage >= 75) // 25% chance
        {
            SpawnPortalGameTile();
        }
        else if (tilesSpawnedUntilPortal >= 21 && tilesSpawnedUntilPortal <= 31 && portalSpawnPercentage >= 65) // 35% chance
        {
            SpawnPortalGameTile();
        }
        else if (tilesSpawnedUntilPortal >= 32 && portalSpawnPercentage >= 40) // 40% chance
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
