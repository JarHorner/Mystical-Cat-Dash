using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyCamera : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 20f;
    public bool cameraMove = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMove)
        {
            transform.Translate(Vector2.right * cameraSpeed * Time.deltaTime);
        }

        if (transform.position.x > 0)
        {
            cameraMove = false;
        }
    }
}
