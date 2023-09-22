using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public static GameUI GameUIInstance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (GameUIInstance != null && GameUIInstance != this)
        {
            Destroy(this);
        }
        else
        {
            GameUIInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
