using Interactables;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTVScreamer : ItemInteraction
{
    private InteractableItem item;

    public float FearValue;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<InteractableItem>();
    }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        // test stuff
        transform.eulerAngles += new Vector3(0, 30, 0);

        // Set Item
        item.ApplyInteractionValues(ItemState.Modified, FearValue);

        // Test NPCs sight instantly
        NPCController.TestNPCsReaction(item);
    }

    // Test stuff
    public void OnMouseDown()
    {
        Debug.Log("Mouse clicked " + name);
        Execute(null);
    }
}
