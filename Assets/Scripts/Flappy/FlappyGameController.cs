using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(0f, 0f, 0f), player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
