using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform goal;
    private int counter;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(goal.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 120)
        {
            counter = 0;
            agent.SetDestination(goal.position);
        }
        else
        {
            counter++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goal = other.gameObject.transform;
        }
    }
}
