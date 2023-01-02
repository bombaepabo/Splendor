using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalJumpPad : MonoBehaviour
{
      public void OnTriggerEnter2D(Collider2D other)
    {
        // Check that the player has collided with the jump pad
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enter");
            // Apply a horizontal force to the player
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100000, 0));
        }
    }
}
