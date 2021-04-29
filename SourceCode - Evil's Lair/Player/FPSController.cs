using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector2 move, rotate;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    //Sounds
    public AudioSource audioSource;
    public AudioClip jumpSound, runSound, walkSound;

    private void Awake()
    {

#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;


#else
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.unityLogger.logEnabled = false;
     
#endif
    }
    void Start()


    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (!Pause.isPaused)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Gamepad.current.leftStickButton.isPressed;
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * move.y : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * move.x : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Gamepad.current.buttonSouth.isPressed && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
                audioSource.PlayOneShot(jumpSound);
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -(Mathf.Clamp(rotate.y, -1, 1)) * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Mathf.Clamp(rotate.x, -1, 1) * lookSpeed, 0);
            }

            if (move.x != 0 || move.y != 0)
            {
               // Debug.Log("moving");
                if (isRunning == true)
                {
                    audioSource.clip = runSound;
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
                else
                {
                    audioSource.clip = walkSound;
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                }
            }
            else if (move.x == 0 && move.y == 0)
            {
                audioSource.Stop();
            }
        }
    }
    public void OnMove(CallbackContext context)
    {
            move = context.ReadValue<Vector2>();
    }
    public void OnRotate(CallbackContext context)
    {
        rotate = context.ReadValue<Vector2>();
    }

    public void SensitivitySlider(float newSensValue)
    {
        lookSpeed = newSensValue;

    }
}