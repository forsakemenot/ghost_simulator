using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateSystem
{
    public class GameStateController : MonoBehaviour
    {
         private UiPanelController _uiPanelController;

         private void Start()
         {
             _uiPanelController = FindObjectOfType<UiPanelController>();
         }


        public void GameOver()
        {
            _uiPanelController.ShowSkillTree();  
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
