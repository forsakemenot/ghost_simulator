using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Interactables;
using System.Linq;
using FearSystem;

public enum AIState
{
    Idle,
    Suspicious,
    Roaming,
    Scared,
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

    public AIState state; // should be private but for i set public for easy debug
    private int currentDestId;
    private NavMeshAgent navAgent;
    private float idleStartTime;
    private Animator anim;
    private NPCController npcController;
    private InteractableItem itemToRevert;
    private FearController _fearController;

    public List<InteractableItem> ScaryItems { get; set; } = new List<InteractableItem>();  // When the player interact with an item add it to the list of item AI should react to ?
                                                                                            // then remove them from the list once the AI has reacted to it

    public Vector3 EyesPosition { get { return transform.position + new Vector3(0, navAgent.height / 2.0f, 0); } }

    public bool IsAvailableForSightCheck { get{ return state != AIState.Scared; } }

    // Start is called before the first frame update
    void Start()
    {
        //testMat.color = Color.white;
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        npcController = GetComponentInParent<NPCController>(); // ugly referencing
        _fearController = FindObjectOfType<FearController>(); // Better than mine for sure lol, V. 

        state = AIState.Roaming;
        SetNextRoamingDestination();
        //scaryItems = GameObject.FindObjectsOfType<InteractableItem>().ToList();
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

            case AIState.Scared:
                RoamingToScared(); // temp for shock state
                break;

            default:
                Debug.Log("AI : unknown state for " + gameObject.name);
                break;
        }

        // debug
        if(Input.GetKeyDown(KeyCode.L))
        {
            RoamingToScared();
        }
        else if(Input.GetKeyDown(KeyCode.M))
        {
            state = AIState.Suspicious;
            anim.SetTrigger("ScaredToDeath");
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            state = AIState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    private void IdleToRoaming()
    {
        //testMat.color = Color.white;
        StartRoaming();
    }

    private void RoamingToIdle()
    {
        StartIdle(false);
    }

    private void RoamingToScared()
    {
        navAgent.SetDestination(transform.position);
        StartIdle(true);
    }

    private void StartIdle(bool scared)
    {
        //Debug.Log("Start Idle");
        state = AIState.Idle;
        idleStartTime = Time.time;

        if(scared)
            anim.SetTrigger("Scared");
        else
            anim.SetTrigger("IdleStart");
    }

    private void StartRoaming()
    {
        //Debug.Log("Start Walk");
        state = AIState.Roaming;
        SetNextRoamingDestination();
        anim.SetTrigger("WalkStart");
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

        foreach (InteractableItem item in ScaryItems)
        {
            if(TryReactToItem(item))
            {
                itemReactedTo = item;
                break;
            }
        }

        if(itemReactedTo != null)
        {
            ScaryItems.Remove(itemReactedTo);
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
        npcController.NPCScared(item.FearValue);
        _fearController.FearIncrement(item.FearValue);
        state = AIState.Scared;

        if (item.LastRevertableInteraction != null)
        {
            itemToRevert = item;
            Invoke("RevertItem", 1);
        }
            
    }

    private void RevertItem()
    {
        itemToRevert.LastRevertableInteraction.Revert();
    }
}
