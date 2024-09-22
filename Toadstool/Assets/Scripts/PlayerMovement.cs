using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float speed = 5f;

    public float turnSmoothTime = 0.2f;
    public float directionSmoothTime = 0.2f;

    private float turnSmoothVelocity;

    private Vector3 currentDirection;
    private Vector3 directionVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f){

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f , angle, 0f);

            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection, ref directionVelocity, directionSmoothTime);

            controller.Move(currentDirection.normalized * speed * Time.deltaTime);
        }
    }
}
