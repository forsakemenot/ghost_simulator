using System;
using GameStateSystem;
using ScoreSystem;
using SkillSystem;
using StaminaSystem;
using TimerSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerEntityController : MonoBehaviour
    {
        private GameStateController _gameStateController;
        
        private StaminaController _staminaController;
        private TimerController _timer;
        private ScoreController _scoreController;
        private SkillController _skillController;
        private NPCController _npcController;
        private bool _isGameOver;

        private void Start()
        {
            _staminaController = GetComponent<StaminaController>();

            _scoreController = FindObjectOfType<ScoreController>();
            _scoreController.ResetScore();

            _skillController = FindObjectOfType<SkillController>();

            _npcController = FindObjectOfType<NPCController>();

            _npcController.OnNPCScared += HandleNPCScared; // Would need a registration, but...

            _timer = GetComponent<TimerController>();
            _timer.StartTimer();

            _gameStateController = FindObjectOfType<GameStateController>();
        }

        private void OnDestroy()
        {
            _npcController.OnNPCScared -= HandleNPCScared;
        }

        private void OnDisable()
        {
            _npcController.OnNPCScared -= HandleNPCScared;
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

            // cheat code
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AddToScore(100);
                HandleSkillUnlockCheck(100);
            }
        }


        private void HandleNPCScared(float score)
        {
            AddToScore(score);
            HandleSkillUnlockCheck(score);
        }

        private void HandleSkillUnlockCheck(float score=0)
        {
            //TODO:: Handle Skill threshold here
            _skillController.CheckIfSkillUnlocked(score);
        }

        private void HandleTimerCheck()
        {
            var isTimerComplete = _timer.GetTimerStatus();
            if (isTimerComplete) GameOver();
        }

        private void GameOver()
        {
            if (_isGameOver) return;
            
            _gameStateController.GamePlaySessionOver();
            _isGameOver = true;
        }

        private void AddToScore(float change)
        {
            _scoreController.AddToScore(change);
        } 
    }
}
