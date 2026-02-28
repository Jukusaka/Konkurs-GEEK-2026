using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] private Animator animator;

    float horizontal;
    float vertical;

    public float runSpeed = 20.0f;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        HandleWalkAnimation();
    }

    private void FixedUpdate()
    {  
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void HandleWalkAnimation()
    {
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);

        if (vertical < 0)
        {
            animator.SetBool("isWalkingDown", true);
        }
        else if (vertical > 0)
        {
            animator.SetBool("isWalkingUp", true);
        }
        else if (horizontal < 0)
        {
            animator.SetBool("isWalkingLeft", true);
        }
        else if (horizontal > 0)
        {
            animator.SetBool("isWalkingRight", true);
        }
    }
}
