using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject portal;
    public bool playerPositioned = false;
    private bool loaded = false;

    void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    // waits some time before spawning the player and destoying the portal behind them
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(player, new Vector3(-3.5f, 3f, 0f), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(portal);
    }

    void Update()
    {
        // if not loaded, uses a transition into the scene.
        if (!loaded)
        {
            Tween.Instance.TweenInNewScene();
            loaded = true;
        }
    }

}
