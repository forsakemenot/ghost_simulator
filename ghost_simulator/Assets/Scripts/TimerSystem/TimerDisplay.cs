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
            var convertedToSecond = timerController.GetAccumulateTime()/60;
            timerText.text =convertedToSecond.ToString("#.##");
        }
    }
}
