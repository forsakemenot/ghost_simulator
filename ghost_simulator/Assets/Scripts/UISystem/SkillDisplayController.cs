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
            foreach (var skillData in skillList)
                if (skillData.scoreToUnlock < _currentCurrency)
                    unlockedType.Add(skillData);

            foreach (var skillData in unlockedType)
            {
                EnableObjectFromSkillData(skillData);
            }
        }

        private void EnableObjectFromSkillData(SkillData skillData)
        {
            switch (skillData.skillType)
            {
                case SkillType.Interaction:
                    skillLevel_1.SetActive(true);
                    break;
                case SkillType.PickupObject:
                    skillLevel_2.SetActive(true);
                    break;
                case SkillType.SelfReveal:
                    skillLevel_3.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
