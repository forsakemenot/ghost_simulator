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

            item.LastRevertableInteraction = null; // do not let this item be reverted;

            Debug.Log("execute PU");
        }
    }
}
