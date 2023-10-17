using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject easyPillarPrefab;
    [SerializeField] private GameObject regularPillarPrefab;
    [SerializeField] private GameObject hardPillarPrefab;
    [SerializeField] private GameObject portalPillarPrefab;
    private GameObject[] pillars;
    private Vector2 objectPoolPosition = new Vector2(-15, -25f);
    [SerializeField] private int pillarPoolSize = 5;
    [SerializeField] private float spawnRate;
    [SerializeField] private float easyColumnMin;
    [SerializeField] private float easyColumnMax;
    [SerializeField] private float regColumnMin;
    [SerializeField] private float regColumnMax;
    [SerializeField] private float hardColumnMin;
    [SerializeField] private float hardColumnMax;
    private float timeSinceLastSpawned = 0f;
    public float spawnXPosition = 15f;
    private int currentPillar = 0;
    private int pillarDestroyed = 0;
    private int pillarsPast;

    void Start()
    {
        pillars = new GameObject[pillarPoolSize];
        //ensures the spawn of first pillar when starting the game
        timeSinceLastSpawned = spawnRate;
    }

    void Update()
    {
        if (GetComponent<FlappyGameController>().playerPositioned)
        {
            timeSinceLastSpawned += Time.deltaTime;
        }

        if (timeSinceLastSpawned >= spawnRate)//!GameController.instance.gameOver && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            if (pillarsPast == 1)
            {
                float spawnYPosition = 0;
                pillars[currentPillar] = (GameObject)Instantiate(portalPillarPrefab, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity);
            }
            else
            {
                float spawnYPosition = Random.Range(easyColumnMin, easyColumnMax);
                pillars[currentPillar] = (GameObject)Instantiate(easyPillarPrefab, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity);
            }

            if (currentPillar >= 3)
            {
                Destroy(pillars[pillarDestroyed]);
                pillarDestroyed++;
            }

            currentPillar++;
            pillarsPast++;
        }

        // if (GameController.instance.scoreNum % 4 == 0 && GameController.instance.scoreNum >= 4)
        // {
        //     ChangeSpawnRate();
        // }
    }

    // private void ChangeSpawnRate()
    // {
    //     if (GameController.instance.scoreNum == 4)
    //     {
    //         spawnRate = 1.85f;
    //     }
    //     else if (GameController.instance.scoreNum == 8)
    //     {
    //         spawnRate = 1.6f;
    //     }
    //     else if (GameController.instance.scoreNum == 12)
    //     {
    //         spawnRate = 1.45f;
    //     }
    //     if (GameController.instance.scoreNum == 16)
    //     {
    //         spawnRate = 1.25f;
    //     }
    // }
}
