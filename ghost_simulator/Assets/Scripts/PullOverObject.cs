using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    
    public override void Execute()
    {
        _rigidbody.AddForce(transform.forward * _strength, ForceMode.Impulse);      
    }
}
