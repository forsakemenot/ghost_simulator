using StaminaSystem;
using TimerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerSystem
{
    public class PlayerEntityController : MonoBehaviour
    {
        private StaminaController _staminaController;
        private TimerController _timer;

        private void Start()
        {
            _staminaController = GetComponent<StaminaController>();
            _timer = GetComponent<TimerController>();
            _timer.StartTimer();
        }

        public void DeductStamina(float changes)
        {
            var currentStamina = _staminaController.GetCurrentStamina();
            if (currentStamina - changes <= 0)
            {
                GameOver();
                return;
            }

            _staminaController.UseStamina(changes);
        }

        private void Update()
        {
            HandleTimerCheck();
        }

        private void HandleTimerCheck()
        {
            var isTimerComplete = _timer.GetTimerStatus();
            if (isTimerComplete) GameOver();
        }

        private void GameOver()
        {
            Debug.LogError("Reload Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}