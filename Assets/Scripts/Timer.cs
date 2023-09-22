using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float timer = 2f;
    private float countdown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;

        if (countdown >= timer) 
        {
            SceneManager.LoadScene("Runner");
        }
    }
}
