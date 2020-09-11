using UnityEngine;
using UnityEngine.UI;

namespace FearSystem
{
    public class FearGaugeDisplayController : MonoBehaviour
    {
        [SerializeField] private Image fearFill;
        private float _maxFear;
        private float _currentFear;


        public void Setup(float maxFear)
        {
            _maxFear = maxFear;
            _currentFear = 0;
            fearFill.fillAmount = 0;
        }


        public void RefreshDisplay(float currentFear)
        {
            var prevFear = _currentFear;
            LeanTween.value(gameObject, FearValueCallback, prevFear, currentFear, 0.2f);
        }

        private void FearValueCallback( float val,float ratio)
        {
            _currentFear = val;
            fearFill.fillAmount = _currentFear / _maxFear;
            Debug.LogError("FEARFILL : "+fearFill.fillAmount);
        }
    }
}
