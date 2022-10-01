using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")] 
    private Vector2 inputDir;
    private bool facingRight = true;
    [SerializeField] private float moveSpeed;
    private float dirY, dirX;

    [Header("Ground Check")] 
    private bool isPlayerGrounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] private Vector3 colliderOffset;
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("Jump")] 
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float jumpDelay = 0.25f;
    private bool jumpMode = false;
    private float jumpTimer;

    [Header("Physics")] 
    private Rigidbody2D rb;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float linearDrag = 2f;
    [SerializeField] private float gravity = 1;
    [SerializeField] private float fallMultiplier = 4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
        if (jumpMode && name == "Player1" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        else if (jumpMode && name == "Player2" && Input.GetKeyDown(KeyCode.W))
        {
            jumpTimer = Time.time + jumpDelay;
        }
        
        if (OnGroundCheck() && jumpTimer > Time.time) Jump();
    }

    void FixedUpdate()
    {
        MovePlayer(dirX);
        ModifyPhysics();
    }
    void PlayerInput()
    {
        if (name == "Player2")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                dirX = moveSpeed;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dirX = -moveSpeed;
            }

        }
        else if(name == "Player1")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                dirX = -moveSpeed;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                dirX = +moveSpeed;
            }
        }
    }
    
    void MovePlayer(float direction)
    {
        rb.AddForce(direction * Vector2.right);
        if (!jumpMode)
        {
            rb.AddForce(dirY * moveSpeed * Vector2.up);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0,facingRight ? 0 : 180, 0);
    }

    void ModifyPhysics()
    {
        
        bool changingDirection = (dirX > 0 && rb.velocity.x < 0) || (dirX < 0 && rb.velocity.x > 0);
        if (OnGroundCheck() && jumpMode)
        {
            if (Math.Abs(dirX) < 0.4f || changingDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = linearDrag * 0.8f;
            }

            //rb.gravityScale = 0;
        }
        else if (jumpMode)
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag;

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
        else
        {
            rb.drag = linearDrag;
            //rb.gravityScale = 0;
        }
    }

    bool OnGroundCheck()
    {
        isPlayerGrounded =
            Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundCheckDistance,
                groundCheckLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down,
                groundCheckDistance, groundCheckLayer);
        return isPlayerGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + (Vector3.down * groundCheckDistance));
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + (Vector3.down * groundCheckDistance));
    }
}
