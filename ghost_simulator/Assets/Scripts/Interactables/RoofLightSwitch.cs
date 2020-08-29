using System;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class RoofLightSwitch : ItemInteraction
{
    [SerializeField] private List<Light> lights;
    private bool _isLightEnabled;

    private void Start()
    {
        interactionName = "Turn Off";
    }

    public override void  Execute()
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
