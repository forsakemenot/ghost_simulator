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

    [SerializeField] private AudioClip sfxToPlayOnTrigger;
    [SerializeField] private AudioClip sfxToPlayOnRevert;
    
    public override bool Revertable { get { return true; } }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        base.Execute(playerEntityController);
        base.PlaySfx(sfxToPlayOnTrigger);
        TargetRenderer.sharedMaterials = new Material[] { NewMaterial, NewMaterial };
    }

    public override void Revert()
    {
        TargetRenderer.sharedMaterials = new Material[] { BaseMaterial, BaseMaterial };
        base.Revert();
        base.PlaySfx(sfxToPlayOnRevert);
    }
}
