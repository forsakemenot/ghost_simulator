using UnityEngine;

namespace StatminaSystem
{
    public class StaminaController : MonoBehaviour
    {
        [SerializeField] private float maxStamina;
        [SerializeField] private StaminaDisplayController staminaDisplayController; 
        private float _currentStamina;
        
        private void Start()
        {
            _currentStamina = maxStamina;
            staminaDisplayController.Setup(_currentStamina);
        }

        public void UseStamina(float changes)
        {
            _currentStamina -= changes;
            staminaDisplayController.RefreshStamina(_currentStamina);
            if (_currentStamina <= 0)
            {
                //TODO :: GameOver
            }
        }    
    }
}
