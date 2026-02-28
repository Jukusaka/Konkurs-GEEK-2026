using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform goal;
    private int counter;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float moveSpeed = 5f;
    private bool isPlayerTargeted;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveToPoint();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        
        if (goal == null) return;
        Vector2 direction = (goal.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        HandleWalkAnimation(direction);

        if (!isPlayerTargeted)
        {
            if (Vector2.Distance(transform.position, goal.position) < 0.5f && counter > 60)
            {
                MoveToPoint();
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, goal.position) > 10f)
            {
                isPlayerTargeted = false;
                MoveToPoint();
            }
        }
    }

    private void HandleWalkAnimation(Vector2 direction)
    {
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);
        animator.SetBool("isWalkingUp", false);
        animator.SetBool("isWalkingDown", false);

        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y < 0)
                animator.SetBool("isWalkingDown", true);
            else
                animator.SetBool("isWalkingUp", true);
        }
        else
        {
            if (direction.x < 0)
                animator.SetBool("isWalkingLeft", true);
            else
                animator.SetBool("isWalkingRight", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goal = other.gameObject.transform;
            isPlayerTargeted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerTargeted = false;
            MoveToPoint();
        }
    }

    private void MoveToPoint()
    {
        goal = waypoints[Random.Range(0, waypoints.Count)];
    }
}