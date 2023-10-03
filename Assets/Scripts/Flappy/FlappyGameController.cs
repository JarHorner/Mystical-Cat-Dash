using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject portal;
    public bool playerPositioned = false;

    void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(player, new Vector3(-3.5f, 3f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(portal);
    }

    void Update()
    {
    }

}
