using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private DefaultShoot defaultShoot;
    [SerializeField] private TripleShoot tripleShoot;
    private PlayerHealth health;
    private Rigidbody2D rigidBody;
    private Animator animator;

    [SerializeField] private float speedOffset;

    private void Awake()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        health = GetComponentInParent<PlayerHealth>();
        
    }

    private void OnEnable()
    {
        health.OnHealthDecrease += HitAnim;
        health.OnPlayerDeath += DeathAnim;
        defaultShoot.OnDefaultFire += DefaultFireAnim;
        tripleShoot.OnTripleFire += TripleFireAnim;
    }

    private void OnDisable()
    {
        health.OnHealthDecrease -= HitAnim;
        health.OnPlayerDeath -= DeathAnim;
        defaultShoot.OnDefaultFire -= DefaultFireAnim;
        tripleShoot.OnTripleFire -= TripleFireAnim;
    }

    private void Update()
    {
        IdleAnim();
    }

    private void IdleAnim()
    {
        if (Mathf.Abs(rigidBody.velocity.x) > speedOffset)
        {
            animator.SetFloat("PlayerSpeed", Mathf.Abs(rigidBody.velocity.x));
        }
        else
        {
            animator.SetFloat("PlayerSpeed", 0);
        }
    }

    private void HitAnim()
    {
        //animator.SetTrigger("TakeDamage");
    }

    private void DeathAnim()
    {
        animator.SetTrigger("Death");
    }

    private void TripleFireAnim()
    {
        animator.SetTrigger("TripleFire");
    }

    private void DefaultFireAnim()
    {
        animator.SetTrigger("DefaultFire");
    }
}