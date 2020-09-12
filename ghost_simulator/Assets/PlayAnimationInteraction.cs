using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using PlayerSystem;

public class PlayAnimationInteraction : ItemInteraction
{
    [SerializeField] private string Trigger;
    [SerializeField] private string ReverseTrigger;
    private Animator anim;
    [SerializeField] private AudioClip sfxToPlayOnTrigger;
    [SerializeField] private AudioClip sfxToPlayOnRevert;

    public override bool Revertable { get { return true; } }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Execute(PlayerEntityController playerEntityController)
    {
        base.Execute(playerEntityController);
        base.PlaySfx(sfxToPlayOnTrigger);
        anim.SetTrigger(Trigger);
    }

    public override void Revert()
    {
        anim.SetTrigger(ReverseTrigger);
        base.Revert();
        base.PlaySfx(sfxToPlayOnRevert);
    }
}
