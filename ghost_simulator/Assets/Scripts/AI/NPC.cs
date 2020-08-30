using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Suspicious,
    Roaming,
    Shocked,
}

public class NPC : MonoBehaviour
{
    [Header("Movement")]
    public float idleDuration; // Could give different idle time depending on previous state (ie: idleAfterRoamDuration, idleAfterShockDuration...) but this is just a demo so...
    public Transform[] RoamDestinations; // Could randomize later

    [Header("Scary reactions")]
    public float DetectionRange;
    public float ShockLength;

    public AIState state; // should be priavet but for easy debug
    private int currentDestId;
    private NavMeshAgent navAgent;
    private float idleStartTime;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        state = AIState.Roaming;
        SetNextRoamingDestination();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AIState.Idle:
                CheckIdleTime();
                break;

            case AIState.Roaming:
                CheckRoamDestinationReached();
                // Check sight line
                break;

            case AIState.Suspicious:
                // check suspicion source reached
                break;

            case AIState.Shocked:
                break;

            default:
                Debug.Log("AI : unknown state for " + gameObject.name);
                break;
        }
    }

    private void IdleToRoaming()
    {
        StartRoaming();
    }

    private void RoamingToIdle()
    {
        StartIdle();
    }

    private void StartIdle()
    {
        state = AIState.Idle;
        idleStartTime = Time.time;
    }

    private void StartRoaming()
    {
        state = AIState.Roaming;
        SetNextRoamingDestination();
    }

    private void SetNextRoamingDestination()
    {
        currentDestId %= RoamDestinations.Length;
        //Debug.Log("Setting id " + currentDestId);

        var dest = RoamDestinations[currentDestId].position;
        navAgent.SetDestination(dest);
    }

    private void CheckIdleTime()
    {
        if (idleStartTime + idleDuration < Time.time)
        {
            IdleToRoaming();
        }
    }

    private void CheckRoamDestinationReached()
    {
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    currentDestId++;
                    //Debug.Log("Aggent stopped, next id : " + currentDestId);

                    RoamingToIdle();
                }
            }
        }
    }
}
