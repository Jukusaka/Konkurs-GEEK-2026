using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform goal;
    private int counter;
    private Rigidbody2D rb;
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
        if (goal == null) return;
        Vector2 direction = (goal.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // coś tu się dzieje
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (!isPlayerTargeted)
        {
            // Patrol mode: switch waypoints
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
            // Chase mode: only stop if player is too far away
            if (Vector2.Distance(transform.position, goal.position) > 10f)
            {
                isPlayerTargeted = false;
                MoveToPoint();
            }
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