using UnityEngine;

namespace StatminaSystem
{
    public class StaminaController : MonoBehaviour
    {
        [SerializeField] private float maxStamina;
        private float _currentStamina; 
        private void Start()
        {
            _currentStamina = maxStamina;
        }

        public void UseStamina(float changes)
        {
            _currentStamina -= changes;
            if (_currentStamina <= 0)
            {
                //TODO :: GameOver
            }
        }    
    }
}
