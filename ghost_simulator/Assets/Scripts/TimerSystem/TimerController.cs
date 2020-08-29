using System;
using System.Timers;
using UnityEngine;

namespace TimerSystem
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private float totalTimeLimit;
        
        
        private float _accumulate;
        private bool _isTicking;
        private bool _isCompleted;

        private void Start()
        {
            Reset();
        }
        
        
        public void SetTimerEnable( bool isEnable)
        {
            _isTicking = isEnable;
        }

        public float GetAccumulateTime()
        {
            return _accumulate;
        }

        public bool GetTimerStatus()
        {
            return _isCompleted;
        }

        public void Reset()
        {
            _accumulate = totalTimeLimit;
        }
         
        private void FixedUpdate()
        {
            if (_isTicking)
            {
                _accumulate -= Time.deltaTime;
                if (_accumulate <= 0)
                    _isCompleted = true;
            }
        }
    }
}
