using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float runSpeed = 8f;
    public float sprintSpeed = 12f;
    public float dashSpeed = 18f;
    public float gravity = -50f;
    public float jump = 1.8f;

    public float dashDuration = 0.25f;
    private float dashTimer;

    public float turnSmoothTime = 0.2f;
    public float directionSmoothTime = 0.2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float turnSmoothVelocity;

    
    private Vector3 currentDirection;
    private Vector3 directionVelocity;
    private Vector3 velocity;

    // obvious
    private bool isGrounded;
    private bool isDashing;
    private bool isRunning;

    // runs every frame
    void Update()
    {
        // obvious : Check's if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // who tf knows ¯\_(ツ)_/¯ 
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f){

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f , angle, 0f);

            Vector3 targetDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            currentDirection = Vector3.SmoothDamp(currentDirection, targetDirection, ref directionVelocity, directionSmoothTime);

            DashAndRun();

            controller.Move(currentDirection.normalized * runSpeed * Time.deltaTime);
        }
        
        if(Input.GetButtonDown("Jump")&& isGrounded){
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    // Dash 
    void DashAndRun(){
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && isGrounded){
            isDashing = true;
            dashTimer = dashDuration;
            runSpeed = dashSpeed;
        }

        if (isDashing){
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f){
                isDashing = false;
                if (Input.GetKey(KeyCode.LeftShift)){
                isRunning = true;
                runSpeed = sprintSpeed;
                }
                else{
                isRunning = false;
                runSpeed = 8f;
                }
            }
        }  
        else if (Input.GetKey(KeyCode.LeftShift) && isRunning){
            runSpeed = sprintSpeed;
        }
         else if (Input.GetKeyUp(KeyCode.LeftShift)){
            isDashing = false;
            isRunning = false;
            runSpeed = 8f;
        }

    }
}
