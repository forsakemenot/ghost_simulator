using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Interactables;

public class NPCController : MonoBehaviour
{
    public event Action<float> OnNPCScared;

    private static NPC[] npcs;

    // Start is called before the first frame update
    void Start()
    {
        npcs = GetComponentsInChildren<NPC>();
    }

    public static void AddItemToWatch(InteractableItem item) // Add modified item for further notice by NPC
    {
        foreach(NPC npc in npcs)
        {
            if (npc.ScaryItems.Contains(item) == false)
                npc.ScaryItems.Add(item);
        }
    }

    public static void TestNPCsReaction(InteractableItem item) // Check if item is seen by NPCs now
    {
        //Debug.Log("Check NPCs for item : " + item);

        foreach (NPC npc in npcs)
        {
            if (npc.IsAvailableForSightCheck)
                npc.TryReactToItem(item);
        }
    }

    public static void ForceNPCsReaction(InteractableItem item) // Check if item is seen by NPCs now
    {
        foreach (NPC npc in npcs)
        {
            if (npc.IsAvailableForSightCheck)
                npc.ReactToItem(item);
        }
    }

    public void NPCScared(float score)
    {
        OnNPCScared?.Invoke(score);
    }
}
