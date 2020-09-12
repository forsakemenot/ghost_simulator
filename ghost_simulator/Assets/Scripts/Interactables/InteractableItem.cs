using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public enum ItemState
    {
        None,
        Normal, // AI doesn't react to it
        Modified // AI should react to it and be scared, amount of fear is specified by the interaction used
    }

    public class InteractableItem : MonoBehaviour
    {

        public float DetectionDistance;
        public float FearValue { get; set; }
        public Outline Outline { get; private set; }


        public ItemInteraction LastRevertableInteraction;

        [Header("Debug")]
        public ItemState currentState;

        private Vector3 basePosition;

        private void Start()
        {
            Debug.Log(name + "    ");
            Outline = GetComponent<Outline>();
            Outline.enabled = false;
            basePosition = transform.position;
        }

        public void ApplyInteractionValues(ItemState state, float fear, float detectionDistance = -1)
        {
            currentState = state;
            FearValue = fear;

            if (detectionDistance != -1) // i don't think we'll need it, but this allows ItemInteraction to change the detection distance for this item ¯\_(ツ)_/¯
                DetectionDistance = detectionDistance; // otherwise we need to add a way to change it back to normal too ?
        }

        public void ResetState()
        {
            currentState = ItemState.Normal;
            FearValue = 0;
        }

        public void ResetPosition()
        {
            transform.position = basePosition;
            transform.eulerAngles = Vector3.zero;
        }
    }
}

