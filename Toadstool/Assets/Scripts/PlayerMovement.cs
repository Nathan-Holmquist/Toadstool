using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // You can adjust this in the Inspector

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalize the movement vector to ensure consistent speed
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Apply movement to the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Keep the player upright (locks X and Z rotation)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
