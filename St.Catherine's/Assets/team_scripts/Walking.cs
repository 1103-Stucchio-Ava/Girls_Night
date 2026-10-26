using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sneakSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isSneaking;

    public bool IsSneaking => isSneaking;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    
    void Update()
    {
        float currentSpeed = isSneaking ? sneakSpeed : moveSpeed;
        rb.linearVelocity = moveInput * currentSpeed;
    }

    public void Move(InputAction.CallbackContext context){
        animator.SetBool("isWalking", true);

        if(context.canceled){
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSneaking = true;
        }
        else if (context.canceled)
        {
            isSneaking = false;
        }
    }
}
