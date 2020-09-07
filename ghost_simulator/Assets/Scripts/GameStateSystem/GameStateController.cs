using ScoreSystem;
using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace GameStateSystem
{
    public class GameStateController : MonoBehaviour
    {
         private UiPanelController _uiPanelController; 
         private FirstPersonController _firstPersonController;
         private AnotherPauseMenu _anotherPauseMenu;
         private ScoreController _scoreController;
         
         private void Start()
         {
             _uiPanelController = FindObjectOfType<UiPanelController>();
             _firstPersonController = FindObjectOfType<FirstPersonController>();
             _anotherPauseMenu = FindObjectOfType<AnotherPauseMenu>();
             _scoreController = FindObjectOfType<ScoreController>();
             PlayerPrefs.DeleteKey("TempCurrency");
         }


         public void GamePlaySessionOver()
         {
             _firstPersonController.IsPause = true;
             _uiPanelController.ShowSkillTree();
             _anotherPauseMenu.IsGameOver = true;
             PlayerPrefs.SetFloat("TempCurrency", _scoreController.GetCurrentScore());
         }

        public void ReloadGamePlaySession()
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
