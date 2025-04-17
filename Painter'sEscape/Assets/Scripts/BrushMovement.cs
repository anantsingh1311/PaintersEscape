using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrushMovement : MonoBehaviour
{

    //paint brush refrence 
    public Rigidbody2D paintBrush;

    //move speed of the brush
    public float moveSpeed = 5.0f;

    //Calculate the horizontal movement of the player
    float horizontalMovement;

    //variable to calculate vertical jump
    float VerticleJump;

    //factor by which the player is allowed to jump
    public float jumpHeight;

    //jump cool down 
    private float timer;

    // to check if the player is on the ground 
    private bool ground;




    // Start is called before the first frame update
    void Start()
    {
        if(paintBrush == null){
            paintBrush = GetComponent<Rigidbody2D>();
        }

        timer = 0;
        ground = true;
      
        
    }

    // Update is called once per frame
    void Update()
    {

    //      timer += Time.deltaTime;

    // if (timer >= 0.5f)
    // {   
    //     //if timer becomes greater than 1 set ground to 0
    //     ground = false;
    //     // Debug.Log(ground);
    //     timer = 0;  // set timer to 0 again
    //     ground = true; // set to ground again to true, after the object returns to the ground
    //     // Reset timer again 
    // }
    // ground = true;


    // Increase cooldown timer
        if (!ground) 
        {
            timer += Time.deltaTime;
        }

        // Only reset ground after 0.5 seconds
        if (timer >= 0.80f)
        {   
            ground = true; // Allow jumping again
            timer = 0; // Reset timer
        }

        
        paintBrush.velocity = new Vector2(horizontalMovement*moveSpeed,paintBrush.velocity.y);

        // OnCollisionEnter2D()
    
        


    }
    public void Move(InputAction.CallbackContext callBackContext)
    {
        horizontalMovement = callBackContext.ReadValue<Vector2>().x;
        // VerticleJump = callBackContext.ReadValue<Vector2>().y;
        
    }

public void Jump(InputAction.CallbackContext callBackContext){

        VerticleJump = callBackContext.ReadValue<Vector2>().y;
        if((Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && ground){
            paintBrush.velocity = new Vector2(paintBrush.velocityX,VerticleJump*jumpHeight);
            ground = !ground;
            timer = 0;
        }
    
}











}

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class BrushMovement : MonoBehaviour
// {
//     public Rigidbody2D paintBrush;
//     public float moveSpeed = 5.0f;

//     private float horizontalMovement;
//     private float jumpPressTime;
//     public float jumpHeight = 5.0f;
//     public float maxJumpTime = 0.5f; // Max time the jump button can be held
//     public float jumpCooldown = 1.0f; // Cooldown between jumps

//     private bool isGrounded;
//     private bool isJumping;
//     private float jumpTimer; // Timer to manage jump cooldown

//     public Transform groundCheck; // Empty GameObject at feet
//     public LayerMask groundLayer; // Assign "Ground" layer in Unity

//     void Start()
//     {
//         if (paintBrush == null)
//         {
//             paintBrush = GetComponent<Rigidbody2D>();
//         }

//         jumpTimer = 0;
//         isGrounded = true;
//         isJumping = false;
//     }

//     void Update()
//     {
//         jumpTimer += Time.deltaTime;

//         // Simulate ground reset every 1 second
//         if (jumpTimer >= 1.0f)
//         {
//             isGrounded = true;
//         }

//         // Apply horizontal movement
//         paintBrush.velocity = new Vector2(horizontalMovement * moveSpeed, paintBrush.velocity.y);

//         // If jump is being held and within max jump time, apply force
//         if (isJumping && jumpPressTime < maxJumpTime)
//         {
//             jumpPressTime += Time.deltaTime;
//             paintBrush.velocity = new Vector2(paintBrush.velocity.x, jumpHeight);
//         }
//     }

//     public void Move(InputAction.CallbackContext callBackContext)
//     {
//         horizontalMovement = callBackContext.ReadValue<Vector2>().x;
//     }

//     public void Jump(InputAction.CallbackContext callBackContext)
//     {
//         if (callBackContext.started && isGrounded && jumpTimer >= jumpCooldown)
//         {
//             isJumping = true;
//             isGrounded = false; // Assume jump started
//             jumpPressTime = 0; // Reset jump duration tracker
//             jumpTimer = 0; // Reset cooldown timer
//         }

//         if (callBackContext.canceled)
//         {
//             isJumping = false; // Stop applying force when jump button is released
//         }
//     }
// }


