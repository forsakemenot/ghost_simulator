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
    public Material HLMaterial;

    [SerializeField] private AudioClip sfxToPlayOnTrigger;
    [SerializeField] private AudioClip sfxToPlayOnRevert;
    
    public override bool Revertable { get { return true; } }

    private void SetSharedMaterials(Material mat)
    {
        TargetRenderer.sharedMaterials = new Material[] { mat, mat };
    }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        base.Execute(playerEntityController);
        base.PlaySfx(sfxToPlayOnTrigger);
        SetSharedMaterials(NewMaterial);
    }

    public override void Revert()
    {
        SetSharedMaterials(BaseMaterial);

        base.Revert();
        base.PlaySfx(sfxToPlayOnRevert);
    }

    public override void SetHighlighted(bool highlighted)
    {
        if(highlighted)
        {
            SetSharedMaterials(HLMaterial);
        }
        else if(item.currentState != ItemState.Modified)
        {
            SetSharedMaterials(BaseMaterial);
        }
    }
}
