using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform goal;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Transform> waypoints;
    private bool isPlayerTargeted;
    private NavMeshAgent _agent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _agent = GetComponent<NavMeshAgent>();

        // Required for 2D NavMesh
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        MoveToPoint();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (goal == null) return;
        
        if (isPlayerTargeted)
        {
            _agent.SetDestination(goal.position);

            if (Vector2.Distance(transform.position, goal.position) > 10f)
            {
                isPlayerTargeted = false;
                MoveToPoint();
            }
        }
        
        Vector2 agentVelocity = _agent.velocity;
        rb.velocity = agentVelocity;

        if (agentVelocity.sqrMagnitude > 0.01f)
            HandleWalkAnimation(agentVelocity.normalized);
        
        if (!isPlayerTargeted && !_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            MoveToPoint();
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
            _agent.SetDestination(goal.position);
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
        _agent.SetDestination(goal.position);
    }

    public void JebaćDisa()
    {
        
    }
}