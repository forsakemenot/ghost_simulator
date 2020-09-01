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

                        if (holdObjectTransform.childCount == 0)
                        {
                            pickupItemRigidbody.useGravity = false;
                            pickupItemRigidbody.isKinematic = true;
                            hitItem.transform.SetParent(holdObjectTransform);
                            hitItem.transform.localPosition = Vector3.zero;
                        }
                        else
                        {
                            pickupItemRigidbody.useGravity = true;
                            hitItem.transform.SetParent(null);

                            ThrowObject(pickupItemRigidbody);
                        }
                    }
                }

                if (!_isInteractionEnable) return;
                ShowDisplayOption(hitItem);
                if (Input.GetKeyDown(KeyCode.E))
                    hitItem.Execute(_playerEntityController);
            }
        }

        private void ThrowObject(Rigidbody pickupItemRigidbody)
        {
            pickupItemRigidbody.AddForce(Vector3.one, ForceMode.Impulse);
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