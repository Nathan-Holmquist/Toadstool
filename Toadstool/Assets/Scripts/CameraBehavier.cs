using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The capsule's transform
    public Vector3 offset;    // Offset between the camera and capsule
    public float smoothSpeed = 0.125f; // Smooth camera movement

    void Start()
    {

        // Custom offset (X, Y, Z) for 3rd person view
        offset = new Vector3(0, 3, -5);
    }

    void LateUpdate()
    {
        // Desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between current position and the desired one
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position
        transform.position = smoothedPosition;

        // Make the camera look at the capsule
        transform.LookAt(target);
    }
}
