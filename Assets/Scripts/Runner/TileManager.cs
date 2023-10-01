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
    public int totalTilesSpawned = 0;
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
            if (i == swapGameTileSpawnNum)
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
            if (totalTilesSpawned == swapGameTileSpawnNum)
            {
                SpawnSwapGameTile();
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
            DeleteTile();
        }
    }

    public void SpawnTile(int index)
    {
        totalTilesSpawned++;
        GameObject tile = Instantiate(tilePrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(tile);
        zSpawn += tileLength;
    }

    public void SpawnSwapGameTile()
    {
        totalTilesSpawned++;
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
