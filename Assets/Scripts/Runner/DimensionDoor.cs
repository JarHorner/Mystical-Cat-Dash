using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionDoor : MonoBehaviour
{
    public Transform playerTransform;
    public Animation animator;
    public float distanceToOpen;
    private bool closed = true;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (closed)
        {
            float distance = Vector3.Distance(playerTransform.position, gameObject.transform.position);

            if (distance < distanceToOpen)
            {
                animator.Play();
                closed = false;
            }
        }
    }
}
