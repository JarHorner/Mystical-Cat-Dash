using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject swapGameTile;
    public int swapGameTileSpawnNum;
    public float zSpawn = 0;
    public float tileLength = 20;
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
                SpawnTile(0);
            }
            if (i == swapGameTileSpawnNum) // this will be removed
            {
                SpawnSwapGameTile();
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 25 > zSpawn - (numOfTiles * tileLength))
        {
            if (tilesSpawnedUntilPortal == swapGameTileSpawnNum) // this will be removed
            {
                SpawnSwapGameTile();
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
        float portalSpawnPercentage = Random.Range(1, 101); // range of 1 - 100

        if (tilesSpawnedUntilPortal >= 16 && tilesSpawnedUntilPortal >= 30 && portalSpawnPercentage >= 90)
        {
            SpawnSwapGameTile();
        }
        else if (tilesSpawnedUntilPortal >= 31 && tilesSpawnedUntilPortal >= 45 && portalSpawnPercentage >= 85)
        {
            SpawnSwapGameTile();
        }
        else if (tilesSpawnedUntilPortal > 46 && portalSpawnPercentage >= 80)
        {
            SpawnSwapGameTile();
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

    public void SpawnSwapGameTile()
    {
        tilesSpawnedUntilPortal = 0;
        GameObject tile = Instantiate(swapGameTile, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
