using System.Collections.Generic;
using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    public class RoofLightSwitch : ItemInteraction
    {
        [SerializeField] private List<Light> lights;
        public override bool Revertable { get { return true; } }
        private bool _isLightEnabled;

        protected override void Start()
        {
            base.Start();
            interactionName = "Turn Off";
            staminaCost = 2;
            _isLightEnabled = true;
        }

        public override void Execute(PlayerEntityController playerEntityController)
        {
            base.Execute(playerEntityController);
            EnableLight(!_isLightEnabled);   
        }

        private void EnableLight(bool isEnable)
        {
            foreach (var lightElement in lights) 
                lightElement.intensity = isEnable ? 3.2f : 0;
        
        
            _isLightEnabled = !_isLightEnabled;  
            UpdateInteractionName(_isLightEnabled);   
        }

        private void UpdateInteractionName(bool isLightEnabled)
        {
            interactionName = isLightEnabled ? "Turn Off" : "Turn On";
        }

        public override void Revert()
        {
            base.Revert();
            EnableLight(true);
        }
    }
}
