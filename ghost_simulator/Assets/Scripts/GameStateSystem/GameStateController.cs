using PlayerSystem;
using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace GameStateSystem
{
    public class GameStateController : MonoBehaviour
    {
         private UiPanelController _uiPanelController;
         private CameraRaycaster _raycaster;
         private FirstPersonController _firstPersonController;
         private AnotherPauseMenu _anotherPauseMenu; 
         
         
         private void Start()
         {
             _uiPanelController = FindObjectOfType<UiPanelController>();
             _firstPersonController = FindObjectOfType<FirstPersonController>();
             _anotherPauseMenu = FindObjectOfType<AnotherPauseMenu>();
             
             
         }


        public void GamePlaySessionOver()
        {
            _firstPersonController.IsPause = true;
            _uiPanelController.ShowSkillTree();
            _anotherPauseMenu.IsGameOver = true;
         }

        public void ReloadGameplaySession()
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
