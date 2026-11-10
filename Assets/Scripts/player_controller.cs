using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class player_controller : MonoBehaviour
{
    private Animator controller;
    public Camera player_camera;
    public float walk_speed;
    public float run_speed;
    public float jump_power;
    public float gravity;

    public float look_speed = 2f;
    public float look_x_limit = 45f;

    Vector3 move_direction = Vector3.zero;
    float rotation_x = 0;

    public bool can_move = true;

    CharacterController character_controller;


    void Start()
    {
        character_controller = GetComponent<CharacterController>();
        controller = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
    if (Time.timeScale == 0f) return;
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = can_move ? (isRunning ? run_speed : walk_speed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = can_move ? (isRunning ? run_speed : walk_speed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = move_direction.y;
        move_direction = (forward * curSpeedX) + (right * curSpeedY);

        #endregion
        if (controller != null) 
    {
        // Check if there is any forward/backward input
        bool isMovingForward = Input.GetAxis("Vertical") > 0;
        
        bool shouldRun = isMovingForward && isRunning;
        bool shouldWalk = isMovingForward && !isRunning;
        
        // Update your Animator parameters
        controller.SetBool("IsWalking", shouldWalk);
        controller.SetBool("IsRunning", shouldRun);
    }

        #region Handles Jumping
        if (Input.GetButton("Jump") && can_move && character_controller.isGrounded)
        {
            controller.SetTrigger("Jump");
            move_direction.y = jump_power;
        
        }
        else
        {
            move_direction.y = movementDirectionY;
        }

        if (!character_controller.isGrounded)
        {
            move_direction.y -= gravity * Time.deltaTime;
        }
        if (controller != null)
        {
            controller.SetBool("isGrounded", character_controller.isGrounded);

            if (!character_controller.isGrounded)
            {
                // Force running to false while in mid-air so falling takes priority
                controller.SetBool("IsRunning", false); 
                controller.SetBool("IsWalking", false);
            }
        }

        #endregion

        #region Handles Rotation
        character_controller.Move(move_direction * Time.deltaTime);

        if (can_move)
        {
            rotation_x += -Input.GetAxis("Mouse Y") * look_speed;
            rotation_x = Mathf.Clamp(rotation_x, -look_x_limit, look_x_limit);
            player_camera.transform.localRotation = Quaternion.Euler(rotation_x, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * look_speed, 0);
        }
        else
         {
        move_direction = Vector3.zero; 
         }

        #endregion
    }
}

