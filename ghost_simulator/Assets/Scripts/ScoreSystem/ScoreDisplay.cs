using TMPro;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextMesh;

        private void Start()
        {
            scoreTextMesh = GetComponentInChildren<TextMeshProUGUI>();
            scoreTextMesh.text = "Score : 0";
        }

        public void UpdateScoreDisplay(float currentScore)
        {
            scoreTextMesh.text = "Score : "+currentScore;
        }
    }
}
