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
        private ItemInteraction lastHitItem;

        private void Start()
        {
            _playerEntityController = GetComponent<PlayerEntityController>();
        }


        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var rayCastHit, 10))
            {
                if (_isPickupEnable&& _pickedObject!= null)
                {
                 
                    if (Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        _pickedObject.GetComponent<ThrowInteraction>()?.Execute(_playerEntityController);

                        ThrowObject(_pickedObject,
                            _pickedObject.GetComponent<Rigidbody>());
                    }   
                }
                
                
                var hitItem = rayCastHit.transform.GetComponent<ItemInteraction>();
                if (!hitItem)
                {
                    if(lastHitItem)
                    {
                        ResetLastHitItem();
                    }

                    HideOption();
                    return;
                }
                
                if (_isPickupEnable)
                    if (HandlePickUpObject(hitItem))
                        return;

            
                if (!_isInteractionEnable || hitItem.CheckLimitedUse()) return;

                if(lastHitItem != hitItem)
                    ShowDisplayOption(hitItem);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitItem.Execute(_playerEntityController);
                    HideOption();
                }

                lastHitItem = hitItem;
            }
        }

        private void ResetLastHitItem()
        {
            lastHitItem?.SetHighlighted(false);
            lastHitItem = null;
        }
        
        private bool HandlePickUpObject(ItemInteraction hitItem)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                var pickupItemRigidbody = hitItem.GetComponent<Rigidbody>();
                if (pickupItemRigidbody == null) return false;
                if (_pickedObject == null)
                {
                    PickupObject(hitItem.gameObject, pickupItemRigidbody);

                    _pickedObject.GetComponent<PickUpInteraction>()?.Execute(_playerEntityController);

                    return true;
                }
            }


            return false;
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
            hitItemInteraction.SetHighlighted(true);
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
        }

        public void EnablePickup()
        {
            _isPickupEnable = true;
        }
    }
}