using GameStateSystem;
using UnityEngine;
using UnityEngine.UI;

namespace FearSystem
{
    public class FearGaugeDisplayController : MonoBehaviour
    {
        [SerializeField] private Image fearFill;
        private float _maxFear;
        private float _currentFear;
        private GameStateController _gameStateController;


        private void Start()
        {
            _gameStateController = FindObjectOfType<GameStateController>();
        }
        
        
        public void Setup(float maxFear)
        {
            _maxFear = maxFear;
            _currentFear = 0;
            fearFill.fillAmount = 0;
        }


        public void RefreshDisplay(float currentFear)
        {
            var prevFear = _currentFear;
            LeanTween.value(gameObject, FearValueCallback, prevFear, currentFear, 0.2f).setOnComplete(() =>
            {
                if (!(_currentFear > _maxFear)) return;
                _gameStateController.GamePlaySessionOver();
                Debug.LogError("Fear Exceed max fear");
            });
        }

        private void FearValueCallback( float val,float ratio)
        {
            _currentFear = val;
            fearFill.fillAmount = _currentFear / _maxFear;
            Debug.LogError("FEARFILL : "+fearFill.fillAmount);
        }
    }
}
