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
            RectTransform fader = GameObject.Find("Fader").GetComponent<RectTransform>();

            // uses LeanTween to fade in at the start of the game.
            fader.gameObject.SetActive(true);
            loaded = true;
            LeanTween.scale(fader, new Vector3(1f, 1f, 1f), 0);
            // instead of using coroutine, append what happens after using anonymous function setOnComplete.
            LeanTween.scale(fader, Vector3.zero, 0.5f).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        }
    }

}
