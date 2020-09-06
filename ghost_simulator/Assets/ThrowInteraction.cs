using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class ThrowInteraction : ItemInteraction
    {
        public override void Execute(PlayerEntityController playerEntityController)
        {
            base.Execute(playerEntityController);

            Debug.Log("execute throw");
        }
    }
}
