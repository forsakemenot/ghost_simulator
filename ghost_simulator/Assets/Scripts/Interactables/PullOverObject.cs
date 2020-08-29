using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Rigidbody))]
    public class PullOverObject : ItemInteraction
    {

        private Rigidbody _rigidbody;
        [SerializeField] private float _strength = 1;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            interactionName = "Pull Over";
        }
    
        public override void Execute(PlayerEntityController playerEntityController)
        {
            _rigidbody.AddForce(transform.forward * _strength, ForceMode.Impulse);      
        }
    }
}
