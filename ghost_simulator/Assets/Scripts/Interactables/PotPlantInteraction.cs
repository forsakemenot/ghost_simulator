using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    public class PotPlantInteraction : ItemInteraction
    {
        private  Rigidbody _rigidbody;
        public override bool Revertable { get { return true; } }
        [SerializeField] private AudioClip sfxToPlayOnTrigger;
        
        
        protected override void Start()
        {
            base.Start();
            _rigidbody = GetComponent<Rigidbody>();
            //interactionName = "KickOver";
        }


        public override void Execute(PlayerEntityController playerEntityController)
        {
            _rigidbody.AddForce(-transform.forward*3, ForceMode.Impulse);
            
            base.Execute(playerEntityController);
            base.PlaySfx(sfxToPlayOnTrigger);
        }

        public override void Revert()
        {
            item.ResetPosition();
            item.ResetState();
            base.Revert();
        }
    }
}
