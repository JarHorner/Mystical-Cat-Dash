using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public float zSpawn = 0;
    public float distanceBehindToDelete;
    public float cloudLength = 20;
    public int numOfClouds = 5;
    public int totalCloudsSpawned = 0;
    private List<GameObject> activeClouds = new List<GameObject>();
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameObject.FindWithTag("Player").transform;
        for (int i = 0; i < numOfClouds; i++)
        {
            SpawnCloud(Random.Range(0, cloudPrefabs.Length));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform.position.z - distanceBehindToDelete > zSpawn - (numOfClouds * cloudLength))
        {
            SpawnCloud(Random.Range(0, cloudPrefabs.Length));
            DeleteCloud();
        }
    }
    
    public void SpawnCloud(int index)
    {
        totalCloudsSpawned++;
        GameObject cloud = Instantiate(cloudPrefabs[index], transform.forward * zSpawn, transform.rotation);
        activeClouds.Add(cloud);
        zSpawn += cloudLength;
    }

    private void DeleteCloud()
    {
        Destroy(activeClouds[0]);
        activeClouds.RemoveAt(0);
    }
}
