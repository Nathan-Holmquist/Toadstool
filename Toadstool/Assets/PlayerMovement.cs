using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // You can adjust this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // You can initialize things here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Apply movement to the capsule
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Keep the capsule upright (locks X and Z rotation)
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

}
