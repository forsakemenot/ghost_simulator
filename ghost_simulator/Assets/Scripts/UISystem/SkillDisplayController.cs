using System;
using System.Collections.Generic;
using CurrencySystem;
using SkillSystem;
using UnityEngine;

namespace UISystem
{
    public class SkillDisplayController : MonoBehaviour
    {
        private float _currentCurrency;
        [SerializeField] private SkillController skillController;
        [SerializeField] private GameObject skillLevel_1;
        [SerializeField] private GameObject skillLevel_2;
        [SerializeField] private GameObject skillLevel_3;
        [SerializeField] private GameObject skillLevel_4;
        [SerializeField] private GameObject skillLevel_5;
        private List<SkillData> _skillList = new List<SkillData>();

        //[SerializeField] private SkillData skillEntry

        public void Initialize()
        {
            _currentCurrency = CurrencyController.Instance.GetCurrentCurrency();
            _skillList = skillController.GetSkillData();

            skillLevel_1.SetActive(false);
                skillLevel_2.SetActive(false);
            skillLevel_3.SetActive(false);
                skillLevel_4.SetActive(false);
            skillLevel_5.SetActive(false);
            PopulateSkillEntry(_skillList);
        }

        private void PopulateSkillEntry(List<SkillData> skillList)
        {
            Debug.LogError(_currentCurrency);
            
            
            var unlockedType = new List<SkillData>();
            var lockedType = new List<SkillData>();
            foreach (var skillData in skillList)
                if (skillData.scoreToUnlock < _currentCurrency)
                    unlockedType.Add(skillData);
                else 
                    lockedType.Add(skillData);

            foreach (var skillData in unlockedType) EnableUnlockedObjectFromSkillData(skillData);

            foreach (var skillData in lockedType)
            {
                EnableLockedObjectFromSkillData(skillData);
            }


        }

        private void EnableUnlockedObjectFromSkillData(SkillData skillData)
        {
            CanvasGroup canvasGroup;
            SkillEntryDisplayController entryDisplayController;
            switch (skillData.skillType)
            {
                case SkillType.Interaction:
                    SetSkillVisualToUnlocked(skillLevel_1);
                    break;
                case SkillType.PickupObject:
                    SetSkillVisualToUnlocked(skillLevel_2);
                    break;
                case SkillType.DreamHaunt:
                    SetSkillVisualToUnlocked(skillLevel_3);
                    break;
                case SkillType.GhostShadow:
                    SetSkillVisualToUnlocked(skillLevel_4);
                    break;
                case SkillType.Terror:
                    SetSkillVisualToUnlocked(skillLevel_5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSkillVisualToUnlocked(GameObject skillVisualObject)
        {
            var canvasGroup = skillVisualObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            var entryDisplayController = skillVisualObject.GetComponentInChildren<SkillEntryDisplayController>();
            entryDisplayController.SetUnlocked(true);
            skillVisualObject.SetActive(true);
            LeanTween.alphaCanvas(canvasGroup, 1, 1.3f).setEaseOutExpo();
        }

        private void EnableLockedObjectFromSkillData(SkillData skillData)
        {
            CanvasGroup canvasGroup;
            SkillEntryDisplayController entryDisplayController;
            switch (skillData.skillType)
            {
                case SkillType.Interaction:
                    SetSkillVisualToLocked(skillLevel_1);
                    break;
                case SkillType.PickupObject:
                    SetSkillVisualToLocked(skillLevel_2);
                    break;
                case SkillType.DreamHaunt:
                    SetSkillVisualToLocked(skillLevel_3);
                    break;
                case SkillType.GhostShadow:
                    SetSkillVisualToLocked(skillLevel_4);
                    break;
                case SkillType.Terror:
                    SetSkillVisualToLocked(skillLevel_5);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSkillVisualToLocked(GameObject skillVisualObject)
        {
            var canvasGroup = skillVisualObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            var entryDisplayController = skillVisualObject.GetComponentInChildren<SkillEntryDisplayController>();
            entryDisplayController.SetUnlocked(false);
            skillVisualObject.SetActive(true);
            LeanTween.alphaCanvas(canvasGroup, 1, 1.5f).setEaseOutExpo();
        }
    }
}
