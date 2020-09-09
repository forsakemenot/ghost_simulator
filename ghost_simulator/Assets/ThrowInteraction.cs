using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class ThrowInteraction : ItemInteraction
    {
        public override bool Revertable { get { return true; } }

        public override void Execute(PlayerEntityController playerEntityController)
        {
            base.Execute(playerEntityController);
            Debug.Log("execute throw");
        }

        public override void Revert()
        {
            item.ResetPosition();
            item.ResetState();
            base.Revert();
        }
    }
}
