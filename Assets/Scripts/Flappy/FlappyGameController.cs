using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyGameController : MonoBehaviour
{
    public bool jumped = false;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(player, new Vector3(-3.2f, 2.1f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
