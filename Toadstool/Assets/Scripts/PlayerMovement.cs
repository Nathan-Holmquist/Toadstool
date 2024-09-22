using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string balls = "dicknballs";
    public float speed = 5f; // You can adjust this in the Inspector
    public Rigidbody Rigidbody;
    public float jumpAmount = 5f;
    private bool isGrounded = false;

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded) )
        {
            Rigidbody.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }
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
    

    // CUSTOM FUNCTIONS

    // Chekcs when the player enters a object
    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        GameObject Floor = collision.gameObject;
        Debug.Log("Collided with: " + Floor);
    }

    // Checks when the player exits a object
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
        
    }

}
