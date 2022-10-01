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
        if (name == "Player1" && Input.GetKeyDown(KeyCode.W) || name == "Player2" && Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpTimer = Time.time + jumpDelay;
        }
    }
    void FixedUpdate()
    {
        MovePlayer(inputDir);
        ModifyPhysics();
        if (OnGroundCheck() && jumpTimer > Time.time) Jump();
    }
    void PlayerInput()
    {
        if (name == "Player2")
        {
            inputDir = new Vector2(Input.GetAxisRaw("Player2 Horizontal"), 0);

        }
        else if (name == "Player1")
        {
            inputDir = new Vector2(Input.GetAxisRaw("Player1 Horizontal"), 0);
        }
        OnGroundCheck();
    }

    void MovePlayer(Vector2 direction)
    {
        rb.AddForce(direction.x * moveSpeed * Vector2.right);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);

        rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Impulse);
        jumpTimer = 0;

    }

    void FlipFace()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    void ModifyPhysics()
    {
        bool changingDirection = (inputDir.x > 0 && rb.velocity.x < 0) || (inputDir.x < 0 && rb.velocity.x > 0);

        if (OnGroundCheck())
        {
            if (Math.Abs(inputDir.x) < 0.4f || changingDirection)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = linearDrag * 0.8f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag;

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && (name == "Player1" && !Input.GetKey(KeyCode.W) || name == "Player2" && !Input.GetKey(KeyCode.UpArrow)))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
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
