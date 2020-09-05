using Interactables;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWriteOnMirror : ItemInteraction
{
    public Material testMat;
    private InteractableItem item;

    public float FearValue;

    // Start is called before the first frame update
    void Start()
    {
        item = GetComponent<InteractableItem>();
    }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        testMat.color = Color.red;
        item.ApplyInteractionValues(ItemState.Modified, FearValue);

        NPCController.AddItemToWatch(item);
    }

    // Test stuff
    public void OnMouseDown()
    {
        Debug.Log("Mouse clicked " + name);
        Execute(null);
    }
}
