using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraTarget : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime * speed);
    }
}
