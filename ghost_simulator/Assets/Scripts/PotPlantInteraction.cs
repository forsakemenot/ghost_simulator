using System;
using UnityEngine;

public class PotPlantInteraction : ItemInteraction
{
    private  Rigidbody _rigidbody;
    
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        interactionName = "KickOver";
    }


    public override void Execute()
    {
        _rigidbody.AddForce(-transform.forward*3, ForceMode.Impulse);
    }
}
