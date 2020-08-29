using UnityEngine;

namespace Interactables
{
    public class ItemInteraction : MonoBehaviour
    {
        public string interactionName;
        public float staminaCost;
    
        public virtual void Execute()
        {   
        }
    }
}
