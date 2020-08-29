using UnityEngine;
using UnityEngine.UI;

namespace StaminaSystem
{
    public class StaminaDisplayController : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private float _maxStamina;
        private float _fillPercentage;
        private LTDescr _updateFillTween;

        
        public void Setup(float maxStamina)
        {
            _maxStamina = maxStamina;
            _fillPercentage = 1;
            InitializeFill();
        }

        private void InitializeFill()
        {
            fillImage.fillAmount = 1;
            _updateFillTween = LeanTween.value(gameObject, UpdateFillCallback, 1, _fillPercentage, 0.3f).setEaseInSine();
        }

        private void Refresh(float amount)
        {
            if (_updateFillTween != null)
                LeanTween.cancel(_updateFillTween.id);

            var preFillAmount = _fillPercentage;
            var targetFillAmount = amount / _maxStamina;
            _fillPercentage = targetFillAmount;

            _updateFillTween = LeanTween.value(gameObject, UpdateFillCallback, preFillAmount, targetFillAmount, 0.3f)
                .setEaseInSine();
        }

        private void UpdateFillCallback(float val)
        {
            fillImage.fillAmount = val;
        }

        public void RefreshStamina(float currentStamina)
        {
            Debug.LogError("fill % : "+_fillPercentage);
            Refresh(currentStamina);
        }
    }
}

