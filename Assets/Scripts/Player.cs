using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object will move
    [SerializeField] public GameObject trigger;


    void Update()
    {
        // Check for user input
        float moveDirection = Input.GetAxis("Horizontal");

        // Calculate new position
        Vector3 newPosition = transform.position + new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;

        // Apply the new position to the object
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dot")
        {
            Debug.Log("Triggerd");
        }

    }
}
