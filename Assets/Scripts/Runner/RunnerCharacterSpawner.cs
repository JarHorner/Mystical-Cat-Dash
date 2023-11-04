using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class RunnerCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject runnerPlayer;

    // spawns the player at the starting place
    void Awake()
    {
        // if game is just started, runner player will spawn normal, if the game is running 
        // (comes back to runner from flappy section), the runner player will spawn in the 
        // air with a portal.
        if (!GameManager.Instance.isGameStarted)
        {
            GameObject newRunnerPlayer = Instantiate(runnerPlayer, new Vector3(0, 0, 0), Quaternion.identity);
            newRunnerPlayer.name = runnerPlayer.name;
        }
        else
        {
            GameObject newRunnerPlayer = Instantiate(runnerPlayer, new Vector3(0, 10f, 0), Quaternion.identity);
            newRunnerPlayer.name = runnerPlayer.name;
        }

        Destroy(this.gameObject);
    }
}
