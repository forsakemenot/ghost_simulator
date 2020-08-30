using ScoreSystem;
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
        private ScoreController _scoreController;
        private SkillController _skillController;

        private void Start()
        {
            _staminaController = GetComponent<StaminaController>();

            _scoreController = FindObjectOfType<ScoreController>();
            _scoreController.ResetScore();

            _skillController = FindObjectOfType<SkillController>();
            
            _timer = GetComponent<TimerController>();
            _timer.StartTimer();
            HandleSkillUnlockCheck();
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

            HandleAddToScore();

        }


        private void HandleAddToScore()
        {
            //TODO:: Please add to score from enemy spooked event or something;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddToScore(100);
                HandleSkillUnlockCheck();
            }


        }

        private void HandleSkillUnlockCheck()
        {
            
            //TODO:: Handle Skill threshold here
            _skillController.CheckIfSkillUnlocked(_scoreController.GetCurrentScore());
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

        private void AddToScore(float change)
        {
            _scoreController.AddToScore(change);
        } 
    }
}