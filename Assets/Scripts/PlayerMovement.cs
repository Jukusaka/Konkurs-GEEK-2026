using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

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
}
