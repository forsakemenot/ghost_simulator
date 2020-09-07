using CurrencySystem;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreController : MonoBehaviour
    {
        private float _currentScore;
        private ScoreDisplay _scoreDisplay;
        
        private void Start()
        {
            _scoreDisplay = FindObjectOfType<ScoreDisplay>();
            ResetScore();
        }

        public void AddToScore(float change)
        {
            _currentScore += change;
            CurrencyController.Instance.AddToCurrency(change); 
            _scoreDisplay.UpdateScoreDisplay(_currentScore);
            
        }

        public void ResetScore()
        {
            _currentScore = 0;
        }

        public float GetCurrentScore()
        {
            return _currentScore;
        }
    }
}
