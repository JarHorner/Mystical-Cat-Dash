using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField] private GameObject sky;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // ensures the camera is slowly moving along the forest, and the sky follows.
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.fixedDeltaTime);

        // ensures the clouds is properly following the player while moving.
        Vector3 newCloudPosition = new Vector3(sky.transform.position.x, sky.transform.position.y, 95 + target.position.z);
        sky.transform.position = Vector3.Lerp(sky.transform.position, newCloudPosition, 10 * Time.fixedDeltaTime);
    }
}
