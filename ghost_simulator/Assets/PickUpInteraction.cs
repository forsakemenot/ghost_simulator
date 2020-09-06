using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystem;

namespace Interactables
{
    public class PickUpInteraction : ItemInteraction
    {
        public override void Execute(PlayerEntityController playerEntityController)
        {
            base.Execute(playerEntityController);

            Debug.Log("execute PU");
        }
    }
}
