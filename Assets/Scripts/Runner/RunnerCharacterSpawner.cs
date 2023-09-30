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
        Instantiate(runnerPlayer, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
