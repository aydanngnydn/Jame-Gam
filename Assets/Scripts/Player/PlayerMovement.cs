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

    [Header("Ground Check")] 
    private bool isPlayerGrouneded;

    [SerializeField] float groundCheckDistance;
    [SerializeField] private Vector3 colliderOffset;
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("Jump")] 
    [SerializeField] private float jumpSpeed = 15f;

    [SerializeField] private float jumpDelay = 0.25f;
    private bool jumpMode = false;
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
