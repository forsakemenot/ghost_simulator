using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using PlayerSystem;

public class MaterialSwapInteraction : ItemInteraction
{
    public Renderer TargetRenderer;
    public Material NewMaterial;
    public Material BaseMaterial;

    public override bool Revertable { get { return true; } }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        base.Execute(playerEntityController);

        TargetRenderer.sharedMaterials = new Material[] { NewMaterial, NewMaterial };
    }

    public override void Revert()
    {
        TargetRenderer.sharedMaterials = new Material[] { BaseMaterial, BaseMaterial };
        base.Revert();
    }
}
