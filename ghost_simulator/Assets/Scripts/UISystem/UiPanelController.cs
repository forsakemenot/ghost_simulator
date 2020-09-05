using UnityEngine;

namespace UISystem
{
    public class UiPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject skillTreeCanvasObject;


        private void Start()
        {
            skillTreeCanvasObject.SetActive(false);
        }

        public void ShowSkillTree()
        {
            skillTreeCanvasObject.SetActive(true);
        }
        
    }
}