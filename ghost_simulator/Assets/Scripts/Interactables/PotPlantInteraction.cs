using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    public class PotPlantInteraction : ItemInteraction
    {
        private  Rigidbody _rigidbody;
    
        protected override void Start()
        {
            base.Start();

            _rigidbody = GetComponent<Rigidbody>();
            //interactionName = "KickOver";
            staminaCost = 10;
        }


        public override void Execute(PlayerEntityController playerEntityController)
        {
            playerEntityController.DeductStamina(staminaCost);
            _rigidbody.AddForce(-transform.forward*3, ForceMode.Impulse);

            base.Execute(playerEntityController);
        }
    }
}
