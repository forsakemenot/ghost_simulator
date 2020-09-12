using PlayerSystem;
using UnityEngine;

public enum InteractionType
{
    None,
    Instant,
    Permanent
}

namespace Interactables
{
    public class ItemInteraction : MonoBehaviour
    {
        public bool OnlyOnce = false;
        public string interactionName;
        public float staminaCost;
        public int FearValue;
        public InteractionType Type;

        public virtual bool Revertable { get { return false; } }

        protected InteractableItem item;
        protected bool alreadyUsed;


        protected virtual void Start()
        {
            item = GetComponent<InteractableItem>();
        }

        public virtual void Execute(PlayerEntityController playerEntityController)
        {
            if(item == null)
                item = GetComponent<InteractableItem>(); // juste in case we forgot to call base.Start in child class

            SetHighlighted(false);

            switch (Type)
            {
                case InteractionType.Instant:
                    item.ApplyInteractionValues(ItemState.Modified, FearValue);
                    NPCController.TestNPCsReaction(item);
                    item.ResetState();
                    break;

                case InteractionType.Permanent:
                    item.ApplyInteractionValues(ItemState.Modified, FearValue);
                    NPCController.AddItemToWatch(item);
                    break;

                default:
                    Debug.LogError("State " + Type + " is not handled !");
                    return;
            }

            playerEntityController.DeductStamina(staminaCost);
            alreadyUsed = true;

            if(Revertable)
                item.LastRevertableInteraction = this;
        }

        public bool CheckLimitedUse()
        {
            return OnlyOnce && alreadyUsed;
        }

        public virtual void PlaySfx(AudioClip sfxClip)
        {
          var source = CreateSfxObjectInstance();
          source.PlayOneShot(sfxClip);
        }

        private AudioSource CreateSfxObjectInstance()
        {    
            var sfxGo = new GameObject();
            var audioSource = sfxGo.AddComponent<AudioSource>();
            return audioSource;

        }


        public virtual void Revert()
        {
            if(Revertable)
            {
                item.LastRevertableInteraction = null;
                alreadyUsed = false;
            }
        }

        public virtual void SetHighlighted( bool highlighted)
        {
            Debug.Log(name + "  try HL  ");
            Debug.Log(item + "  is item  ");
            // Debug.Log(item.Outline + "  is outline  ");

            item.Outline.enabled = highlighted;
        }
    }
}
