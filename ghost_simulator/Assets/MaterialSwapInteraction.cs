using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using PlayerSystem;

public class MaterialSwapInteraction : ItemInteraction
{
    public Renderer TargetRenderer;
    public Material NewMaterial;

    public override void Execute(PlayerEntityController playerEntityController)
    {
        base.Execute(playerEntityController);

        TargetRenderer.sharedMaterials = new Material[] { NewMaterial, NewMaterial };
    }
}
