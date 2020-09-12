using UnityEngine;

namespace FearSystem
{
    public class FearController : MonoBehaviour
    {

        [SerializeField] private float maxFear;
        [SerializeField] private FearGaugeDisplayController fearGaugeDisplayController;
        private float _currentFear;

        private void Start()
        {
            fearGaugeDisplayController.Setup(maxFear);
        }
        
        public void FearIncrement(float changes)
        {
            _currentFear += changes;
            fearGaugeDisplayController.RefreshDisplay(_currentFear);
        } 
       
    }
}
