using TMPro;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

namespace TimerSystem
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TimerController timerController;
        
     
        private void Update()
        {

            float convertedToSecond = timerController.GetAccumulateTime()/60;
            Debug.LogError(timerController.GetAccumulateTime() + " : "+timerController.GetAccumulateTime() /60);
            
            // timerController
            timerText.text =convertedToSecond.ToString("#.##");
        }
    }
}
