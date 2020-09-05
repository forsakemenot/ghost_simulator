using System;
using UnityEngine;

namespace PlayerSystem
{
    public class DisablePhysicOnGroundHit: MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Physics.Raycast(transform.position, Vector3.down,out var hit,0.05f);
            if (hit.transform == null) return;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            Destroy(this);
        }
    }
}