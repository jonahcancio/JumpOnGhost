using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Speeds and Forces")]
    public float groundSpeed;
    public float airSpeed;
    public float jumpForce;
    private float moveInput;
    private float moveSpeed;

    [SerializeField]
    private float jumpMomentum;

    [SerializeField]
    private bool isGrounded;


    [Header("Ground Checking")]
    public Transform feetPosition;
    public Vector2 checkSize;
    public float checkAngle;
    public LayerMask groundLayer;

    [Header("Jump Control")]
    private float timeLastJumped;
    public float maxJumpPressTime;
    private bool isJumpPressing;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(feetPosition.position, checkSize, checkAngle, groundLayer);
        if (!isGrounded && jumpMomentum != moveInput)
        {
            moveSpeed = airSpeed;
            jumpMomentum = 0;             
        } else
        {
            moveSpeed = groundSpeed;
            jumpMomentum = moveInput;
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (isJumpPressing)
        {
            if (Time.time - timeLastJumped < maxJumpPressTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            } 
        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<float>();
            //Debug.Log("Player Moving: " + context.phase + " => " + moveInput);
        }
            
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressing = context.ReadValueAsButton();
        //Debug.Log("Player Jumping: " + context.phase + " => " + isJumpPressing);
        if (context.phase == InputActionPhase.Performed)
        {
            if (isGrounded && isJumpPressing)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                timeLastJumped = Time.time;
                jumpMomentum = moveInput;
            }
        }
        
    }
}
