using GameStateSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class UiPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject skillTreeCanvasObject;
        [SerializeField] private Button continueButton;

        private GameStateController _gameStateController;
        
        private void Start()
        {

            _gameStateController = FindObjectOfType<GameStateController>();
            
            continueButton.onClick.RemoveAllListeners();
            skillTreeCanvasObject.SetActive(false);
        }

        public void ShowSkillTree()
        {
            continueButton.onClick.AddListener(ReloadGame);
            skillTreeCanvasObject.SetActive(true);
        }

        private void ReloadGame()
        {
            //TODO:: Reload then continue next session
            //TOOO:: SAVE DATA!! 
            _gameStateController.ReloadGameplaySession();
        }
    }
}