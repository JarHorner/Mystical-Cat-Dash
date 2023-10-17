using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 offset;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // ensures the camera is properly following the player while moving.
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.fixedDeltaTime);
    }
}
