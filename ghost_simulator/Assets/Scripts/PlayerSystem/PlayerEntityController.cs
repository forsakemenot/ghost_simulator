using StatminaSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerEntityController : MonoBehaviour
    {
        private StaminaController _staminaController;

        private void Start()
        {
            _staminaController = GetComponent<StaminaController>();   
        }

        public void DeductStamina(float changes)
        {
            _staminaController.UseStamina(changes);
        }
    }
}
