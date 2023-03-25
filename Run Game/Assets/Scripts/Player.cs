using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    PlayerInput playerInput;

    private Vector2 characterMovementInput;
    private Vector3 characterMovement;
    private Vector3 positionToLookAt;
    private bool isMoving;
    private float rotationVelocity = 5f;
    private bool runPressed;
    private float startVelocity = 2f;
    private bool dashPressed;

    [SerializeField] private float velocity;
    [SerializeField] private float velocityRunning;
    [SerializeField] private float dashForce;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }


    void Update()
    {
        SetMovement();
        AnimationHandler();
        PlayerRotationHandler();
    }

    void SetMovement()
    {
        characterController.Move(characterMovement * velocity * Time.deltaTime);

        if (runPressed)
        {
            velocity = velocityRunning;
        }
        if (!runPressed)
        {
            velocity = startVelocity;
        }
    }
    

    void AnimationHandler()
    {
        if(!animator.GetBool("isMoving") && isMoving)
        {
            animator.SetBool("isMoving", true);
        }
        if(animator.GetBool("isMoving") && !isMoving)
        {
            animator.SetBool("isMoving", false);
        }
        if((isMoving && runPressed) && !animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
        }
        if ((isMoving && !runPressed) && animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", false);
        }
        if((!isMoving && runPressed) && animator.GetBool("isRunning"))
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
        }

        
    }
    
    void PlayerRotationHandler()
    {
        positionToLookAt = new Vector3(characterMovement.x, characterMovement.y, characterMovement.z);
        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity * Time.deltaTime);
        }
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        characterMovementInput = context.ReadValue<Vector2>();
        characterMovement = new Vector3(characterMovementInput.x, 0, characterMovementInput.y);

        isMoving = characterMovementInput.x != 0 || characterMovementInput.y != 0;
    }

    public void SetRunning(InputAction.CallbackContext context)
    {
        runPressed = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
