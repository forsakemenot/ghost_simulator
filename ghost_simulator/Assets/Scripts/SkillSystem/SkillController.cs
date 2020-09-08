using System;
using System.Collections.Generic;
using CurrencySystem;
using PlayerSystem;
using UnityEngine;

namespace SkillSystem
{
    public class SkillController : MonoBehaviour
    {
        [SerializeField] private List<SkillData> skillList = new List<SkillData>();
        private CameraRaycaster _rayCaster;

        private float _currentSkillCurrency; 
        
        

        private void OnEnable()
        {
            _rayCaster = FindObjectOfType<CameraRaycaster>();

            _currentSkillCurrency = 0;
            var previousRunCurrency= CurrencyController.Instance.GetCurrentCurrency(); 
            CheckIfSkillUnlocked(previousRunCurrency);
        }


        public void CheckIfSkillUnlocked(float currentScore)
        {
            _currentSkillCurrency += currentScore;
            
            var unlockedSkill = new List<SkillData>();
            foreach (var skillData in skillList)
                if (skillData.scoreToUnlock < currentScore)
                    unlockedSkill.Add(skillData);

            UpdateInteractionAbility(unlockedSkill);
        }

        private void UpdateInteractionAbility(List<SkillData> unlockedSkill)
        {
            foreach (var skillData in unlockedSkill)
            {
                switch (skillData.skillType)
                {
                    case SkillType.Interaction:
                        _rayCaster.EnableInteraction();
                        break;
                    case SkillType.PickupObject:
                        _rayCaster.EnablePickup();
                        break;
                    case SkillType.SelfReveal :
                        
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }    
        }
    }
}
