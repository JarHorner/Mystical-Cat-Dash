using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private FlappyGameController gameController;
    [SerializeField] private GameObject easyPillarPrefab;
    [SerializeField] private GameObject regularPillarPrefab;
    [SerializeField] private GameObject hardPillarPrefab;
    [SerializeField] private GameObject portalPillarPrefab;
    [SerializeField] private List<GameObject> pillars;
    private Vector2 objectPoolPosition = new Vector2(-15, -25f);
    [SerializeField] private int maxPillarPoolSize = 5;
    [SerializeField] float spawnRate = 2.8f; // base 2.8
    [SerializeField] private float[] easyPillarYPositions;
    [SerializeField] private float[] regularPillarYPositions;
    [SerializeField] private float[] hardPillarYPositions;
    [SerializeField] private float lastPillarYPosition;
    private float timeSinceLastSpawned = 0f;
    public float spawnXPosition = 15f;
    private int currentPillar = 0;
    private int pillarsSpawned;

    void Start()
    {
        pillars = new List<GameObject>();
        //ensures the spawn of first pillar when starting the game
        timeSinceLastSpawned = spawnRate;
    }

    void Update()
    {
        if (GameManager.Instance.loadedInto2DWorld == true)
        {
            ChangeSpawnRate();
            GameManager.Instance.loadedInto2DWorld = false;
        }

        if (gameController.playerPositioned)
        {
            timeSinceLastSpawned += Time.deltaTime;
        }

        if (timeSinceLastSpawned >= spawnRate && !GameManager.Instance.gameOver)
        {
            timeSinceLastSpawned = 0f;
            // for Testing
            if (pillarsSpawned == 0)
            {
                pillars.Add((GameObject)Instantiate(hardPillarPrefab, new Vector2(spawnXPosition, -4f), Quaternion.identity));
            }
            else if (pillarsSpawned == 1)
            {
                pillars.Add((GameObject)Instantiate(hardPillarPrefab, new Vector2(spawnXPosition, 6f), Quaternion.identity));
            }

            DeterminePillarSpawn();

            if (currentPillar >= maxPillarPoolSize)
            {
                Destroy(pillars[0]);
                pillars.RemoveAt(0);
            }

            currentPillar++;
            pillarsSpawned++;
        }
    }

    private void DeterminePillarSpawn()
    {
        float portalSpawnPercentage = Random.Range(1, 101); // range of 1 - 100

        if (pillarsSpawned >= 5 && pillarsSpawned < 7 && portalSpawnPercentage >= 70)
        {
            float spawnYPosition = 0;
            pillars.Add((GameObject)Instantiate(portalPillarPrefab, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity));
        }
        else if (pillarsSpawned >= 7 && pillarsSpawned <= 9 && portalSpawnPercentage >= 40)
        {
            float spawnYPosition = 0;
            pillars.Add((GameObject)Instantiate(portalPillarPrefab, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity));
        }
        else if (pillarsSpawned >= 9 && pillarsSpawned <= 11 && portalSpawnPercentage >= 10)
        {
            float spawnYPosition = 0;
            pillars.Add((GameObject)Instantiate(portalPillarPrefab, new Vector2(spawnXPosition, spawnYPosition), Quaternion.identity));
        }
        else
        {
            SpawnPillar();
        }
    }


    private void SpawnPillar()
    {
        int randomYPosition;
        float chosenYPosition;

        if (GameManager.Instance.timesEntered2DWorld == 1) // 1
        {
            // easy pillar spawn
            randomYPosition = Random.Range(0, easyPillarYPositions.Length);
            chosenYPosition = easyPillarYPositions[randomYPosition];
            pillars.Add((GameObject)Instantiate(easyPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
            lastPillarYPosition = chosenYPosition;
        }
        else if (GameManager.Instance.timesEntered2DWorld == 2) // 2
        {
            float chosenPillarNum = Random.Range(0, 2);

            // easy or regular pillar spawn
            switch (chosenPillarNum)
            {
                case 0:
                    randomYPosition = Random.Range(0, easyPillarYPositions.Length);
                    chosenYPosition = easyPillarYPositions[randomYPosition];
                    pillars.Add((GameObject)Instantiate(easyPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
                    break;
                case 1:
                    randomYPosition = Random.Range(0, regularPillarYPositions.Length);
                    chosenYPosition = regularPillarYPositions[randomYPosition];
                    pillars.Add((GameObject)Instantiate(regularPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
                    break;
                default:
                    break;
            }
        }
        else if (GameManager.Instance.timesEntered2DWorld == 3) // 3
        {
            // regular pillar spawn
            randomYPosition = Random.Range(0, easyPillarYPositions.Length);
            chosenYPosition = easyPillarYPositions[randomYPosition];
            pillars.Add((GameObject)Instantiate(regularPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
        }
        else if (GameManager.Instance.timesEntered2DWorld == 4) // 4
        {
            float chosenPillarNum = Random.Range(0, 2);

            // regular or hard pillar spawn
            switch (chosenPillarNum)
            {
                case 0:
                    randomYPosition = Random.Range(0, regularPillarYPositions.Length);
                    chosenYPosition = regularPillarYPositions[randomYPosition];
                    pillars.Add((GameObject)Instantiate(regularPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
                    break;
                case 1:
                    randomYPosition = Random.Range(0, hardPillarYPositions.Length);
                    chosenYPosition = hardPillarYPositions[randomYPosition];
                    pillars.Add((GameObject)Instantiate(hardPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
                    break;
                default:
                    break;
            }
        }
        else if (GameManager.Instance.timesEntered2DWorld >= 5) // 5
        {
            // hard pillar spawn
            randomYPosition = Random.Range(0, hardPillarYPositions.Length + 1);
            chosenYPosition = hardPillarYPositions[randomYPosition];
            pillars.Add((GameObject)Instantiate(hardPillarPrefab, new Vector2(spawnXPosition, chosenYPosition), Quaternion.identity));
        }
    }

    // based on the amount of time player enters 2D world, spawn rate will be decreased.
    private void ChangeSpawnRate()
    {
        if (GameManager.Instance.timesEntered2DWorld == 2)
        {
            spawnRate -= 0.1f;
        }
        else if (GameManager.Instance.timesEntered2DWorld == 3)
        {
            spawnRate -= 0.2f;
        }
        else if (GameManager.Instance.timesEntered2DWorld == 4)
        {
            spawnRate -= 0.3f;
        }
        else if (GameManager.Instance.timesEntered2DWorld >= 5)
        {
            spawnRate -= 0.4f;
        }
    }
}
