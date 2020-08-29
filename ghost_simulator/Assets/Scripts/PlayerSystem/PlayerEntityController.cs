using StaminaSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            var currentStamina =_staminaController.GetCurrentStamina();
            if (currentStamina - changes <= 0)
            {
                GameOver();
                return;
            }
            
            _staminaController.UseStamina(changes);
        }

        private void GameOver()
        {
            Debug.LogError("Reload Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
