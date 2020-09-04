using Interactables;
using TMPro;
using UnityEngine;

namespace PlayerSystem
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private Transform holdObjectTransform;

        private PlayerEntityController _playerEntityController;
        private bool _isInteractionEnable;
        private bool _isPickupEnable;
        private GameObject _pickedObject;

        private void Start()
        {
            _playerEntityController = GetComponent<PlayerEntityController>();
        }


        private void FixedUpdate()
        {

            if (Physics.Raycast(transform.position, transform.forward, out var rayCastHit, 10))
            {
                var hitItem = rayCastHit.transform.GetComponent<ItemInteraction>();
                if (!hitItem)
                {
                    HideOption();
                    return;
                }

                if (_isPickupEnable)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        var pickupItemRigidbody = hitItem.GetComponent<Rigidbody>();
                        if (_pickedObject == null)
                            PickupObject(hitItem.gameObject, pickupItemRigidbody);
                    }
                }

                if (!_isInteractionEnable) return;
                ShowDisplayOption(hitItem);
                if (Input.GetKeyDown(KeyCode.E))
                    hitItem.Execute(_playerEntityController);
            }
            
            if (_isPickupEnable)
            {
                if (_pickedObject != null)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                        ThrowObject(_pickedObject,
                            _pickedObject.GetComponent<Rigidbody>());
                }
            }

            
        }

        private void PickupObject(GameObject hitItemGameObject, Rigidbody pickupItemRigidbody)
        {
            pickupItemRigidbody.useGravity = false;
            pickupItemRigidbody.isKinematic = true;

            var hitItemTransform = hitItemGameObject.transform;
            hitItemTransform.SetParent(holdObjectTransform);
            hitItemTransform.localPosition = Vector3.zero;
            hitItemGameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

            _pickedObject = hitItemGameObject.gameObject;
        }

        private void ThrowObject(GameObject hitItemGameObject, Rigidbody pickupItemRigidbody)
        {
            hitItemGameObject.transform.SetParent(null);
            hitItemGameObject.transform.rotation = Quaternion.Euler(Vector3.up);

            hitItemGameObject.gameObject.AddComponent<DisablePhysicOnGroundHit>();

            Debug.LogError("ThrowObject");
            pickupItemRigidbody.isKinematic = false;
            pickupItemRigidbody.useGravity = true;
            pickupItemRigidbody.AddForce(transform.forward * 2, ForceMode.Impulse);
            _pickedObject = null;
        }

        private void HideOption()
        {
            interactText.text = "";
            interactText.gameObject.SetActive(false);
        }

        private void ShowDisplayOption(ItemInteraction hitItemInteraction)
        {
            interactText.text = hitItemInteraction.interactionName;
            interactText.gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.forward * 10);
        }

        public void EnableInteraction()
        {
            _isInteractionEnable = true;
            Debug.LogError("interaction Enabled");
        }

        public void EnablePickup()
        {
            _isPickupEnable = true;
            Debug.LogError("Pickup Enabled");
        }
    }
}