using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float timer = 2f;
    private float countdown = 0f;

    void Update()
    {
        countdown += Time.deltaTime;

        if (countdown >= timer) 
        {
            SceneManager.LoadScene("Runner");
        }
    }
}
