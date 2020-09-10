using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SkillEntryDisplayController : MonoBehaviour
    {
        [SerializeField] private Image skillImage;
        [SerializeField] private Text headerText;
        [SerializeField] private Text descriptionText;
        private Color _unlockedColor = Color.white;
        private Color _lockedColor = Color.gray;

        public void SetUnlocked(bool isUnlocked) 
        {
            if (isUnlocked)
                SetVisualElementsColor(_unlockedColor);
            else
                SetVisualElementsColor(_lockedColor);
        }

        private void SetVisualElementsColor(Color color)
        {
            headerText.color = color;
            descriptionText.color = color;
            skillImage.color = color;
        }
    }
}
