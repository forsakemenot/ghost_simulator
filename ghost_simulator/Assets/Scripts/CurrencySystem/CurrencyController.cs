using System;
using UnityEngine;

namespace CurrencySystem
{
    public class CurrencyController : MonoBehaviour
    {
        private float _currentCurrency;

        private static CurrencyController _instance;
        public static CurrencyController Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CurrencyController>();
             
                    if (_instance == null)
                    {
                        var container = new GameObject("CController");
                        _instance = container.AddComponent<CurrencyController>();
                    }
                }
     
                return _instance;
            }
        }
        
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad( gameObject );
        }
        
        public void AddToCurrency(float change)
        {
            _currentCurrency += change;
        }

        public float GetCurrentCurrency()
        {
            return _currentCurrency;
        }
        
    }
}
