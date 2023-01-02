using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zippers : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform startPosition;
    public Transform endPosition;
    private bool movingTowardsEnd = true;

    private void Update()
    {
        // Move the platform towards the end position if movingTowardsEnd is true, otherwise move towards the start position
        if (movingTowardsEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, speed * Time.deltaTime);
        }

        // If the platform has reached the end position, set movingTowardsEnd to false
        if (transform.position == endPosition.position)
        {
            movingTowardsEnd = false;
        }

        // If the platform has reached the start position, set movingTowardsEnd to true
        if (transform.position == startPosition.position)
        {
            movingTowardsEnd = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check that the collider is the player
        if (collision.gameObject.tag == "Player")
        {
            // Enable the player's ability to move horizontally with the platform
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
