using PlayerSystem;
using UnityEngine;

namespace Interactables
{
    public class AudioSourceSwitch : ItemInteraction
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip sfxOnTrigger;
        [SerializeField] private AudioClip sfxOnRevert;

        public override void Execute(PlayerEntityController playerEntityController)
        {
            base.Execute(playerEntityController);
            base.PlaySfx(sfxOnTrigger);
            audioSource.mute = true;
        }

        public override void Revert()
        {
            audioSource.mute = false;
            base.Revert();
            base.PlaySfx(sfxOnRevert);

        }
    }
}
