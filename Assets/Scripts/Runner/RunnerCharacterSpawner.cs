using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class RunnerCharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject runnerPlayer;

    void Awake()
    {
        GameObject newRunnerPlayer = Instantiate(runnerPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        newRunnerPlayer.name = runnerPlayer.name;
    }
}
