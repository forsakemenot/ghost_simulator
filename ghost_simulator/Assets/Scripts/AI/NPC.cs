using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interactables;
using System.Linq;

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
    public float FOV;
    //public float ItemDetectionRange; // we could have a DetectionRange field in the item itself, a fire could be more visible than a fallen book
    public float ShockLength; // could be affected by the items ?

    public AIState state; // should be priavet but for easy debug
    private int currentDestId;
    private NavMeshAgent navAgent;
    private float idleStartTime;

    public List<InteractableItem> scaryItems; // TODO : In real scene, when the player interact with an item add it to the list of item AI should react to ?
                                              // then remove them from the list once the AI has reacted to it
                                              // if multiple AI each have their list of objects they need to react to ?

    public Vector3 EyesPosition { get { return transform.position + new Vector3(0, navAgent.height / 2.0f, 0); } }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        state = AIState.Roaming;
        SetNextRoamingDestination();
        scaryItems = GameObject.FindObjectsOfType<InteractableItem>().ToList();
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
                CheckSightForAllItems();

                break;

            case AIState.Suspicious:
                // check suspicion source reached
                break;

            case AIState.Shocked:
                RoamingToIdle(); // temp for shock state
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

    private void CheckSightForAllItems()
    {
        InteractableItem itemReactedTo = null; // need this otherwise c# cries cause we modify the collection during a loop

        foreach (InteractableItem item in scaryItems)
        {
            if(TryReactToItem(item))
            {
                itemReactedTo = item;
                break;
            }
        }

        if(itemReactedTo != null)
        {
            scaryItems.Remove(itemReactedTo);
        }
    }

    public bool TryReactToItem(InteractableItem item)
    {
        Vector3 aiToItem = item.transform.position - EyesPosition;
        float sightToItemAngle = Vector3.Angle(aiToItem, transform.forward);

        if (sightToItemAngle <= FOV / 2.0f) // is object in FOV ?
        {
            if (Physics.Raycast(EyesPosition, aiToItem.normalized, out RaycastHit hit, item.DetectionDistance)) // does the AI has direct sight on the object (TODO : Make sure the view is only blocked by relevant objects)
            {
                if(hit.collider.gameObject == item.gameObject)
                {
                    return ReactToItem(item);
                }
            }

            Debug.DrawRay(EyesPosition, aiToItem.normalized * item.DetectionDistance, Color.green);
        }

        return false;
    }

    private bool ReactToItem(InteractableItem item) // useless method rightNow, but i keep it if we had other reactions later ?
    {
        switch(item.currentState)
        {
            case ItemState.Modified:
                ApplyFear(item);
                return true;

            default:
                return false;
        }
    }

    private void ApplyFear(InteractableItem item)
    {
        Debug.Log(gameObject.name + " has been scared for " + item.FearValue + " points.");
        navAgent.SetDestination(transform.position);
        state = AIState.Shocked;
    }
}
