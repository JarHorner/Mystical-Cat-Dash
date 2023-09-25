using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public static GameUI GameUIInstance { get; private set; }
    private void Awake()
    {
        if (GameUIInstance == null)
        {
            GameUIInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
