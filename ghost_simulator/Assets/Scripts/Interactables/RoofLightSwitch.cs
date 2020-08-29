using System.Collections.Generic;
using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    public class RoofLightSwitch : ItemInteraction
    {
        [SerializeField] private List<Light> lights;
        private bool _isLightEnabled;

        private void Start()
        {
            interactionName = "Turn Off";
        }

        public override void Execute(PlayerEntityController playerEntityController)
        {
            EnableLight(!_isLightEnabled);   
        }

        private void EnableLight(bool isEnable)
        {
            foreach (var lightElement in lights) 
                lightElement.intensity = isEnable ? 1 : 0;
        
        
            _isLightEnabled = !_isLightEnabled;  
            UpdateInteractionName(_isLightEnabled);   
        }

        private void UpdateInteractionName(bool isLightEnabled)
        {
            interactionName = isLightEnabled ? "Turn Off" : "Turn On";
        }
    }
}
