using System;
using TMPro;
using UnityEngine;

namespace TimerSystem
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TimerController timerController;
        
     
        private void Update()
        {
            var timeSpan = TimeSpan.FromMilliseconds( timerController.GetCurrentRemainingTime());
            timerText.text = timeSpan.ToString(@"mm\:ss");
        }
    }
}
