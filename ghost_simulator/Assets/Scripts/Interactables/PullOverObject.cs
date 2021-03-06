﻿using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public class PullOverObject : ItemInteraction
    {

        private Rigidbody _rigidbody;
        [SerializeField] private float _strength = 1;
        [SerializeField] private AudioClip sfxToPlayOnTrigger;

        protected override void Start()
        {
            base.Start();
            _rigidbody = GetComponent<Rigidbody>();
            interactionName = "Pull Over";
            staminaCost = 3;
        }
    
        public override void Execute(PlayerEntityController playerEntityController)
        {
            _rigidbody.AddForce(transform.forward * _strength, ForceMode.Impulse);      
            base.PlaySfx(sfxToPlayOnTrigger);
            base.Execute(playerEntityController);
        }

        public override void Revert()
        {
            item.ResetPosition();
            item.ResetState();
            base.Revert();
        }
    }
}
