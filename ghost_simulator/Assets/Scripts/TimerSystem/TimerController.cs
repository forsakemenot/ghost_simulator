using System.Diagnostics;
using UnityEngine;

namespace TimerSystem
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private float totalTimeLimitMs;
        
        private bool _isCompleted;
        private Stopwatch _timerStopwatch;
        private bool _isTicking;
        private float _currentRemainingTime;

        private void Awake()
        {
            _timerStopwatch = new Stopwatch();
        }

        private void FixedUpdate()
        {
            if (!_isTicking) return;
            if (_currentRemainingTime > 0)
                _currentRemainingTime = totalTimeLimitMs - GetTimerElaspsedMs();
            else
            {
                _isCompleted = true;
                StopTimer();
            }
        }

        public float GetCurrentRemainingTime()
        {
            return _currentRemainingTime;
        }
        
        
        

        private long GetTimerElaspsedMs()
        {
            return _isTicking ? _timerStopwatch.ElapsedMilliseconds : long.MaxValue;
        }

        public void StartTimer()
        {
            _currentRemainingTime = totalTimeLimitMs*100;
            _timerStopwatch.Reset();
            _timerStopwatch.Start();
            _isTicking = true;
        }

        public void StopTimer()
        {
            _timerStopwatch.Stop();
            _timerStopwatch.Reset();
            _isTicking = true;
        }

        public bool GetTimerStatus()
        {
            return _isCompleted;
        }
    }
}
