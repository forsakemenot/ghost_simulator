using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private List<SkillData> skillList = new List<SkillData>();
    [SerializeField] private CameraRaycaster rayCaster;



    public void CheckIfSkillUnlocked(float currentScore)
    {
        var unlockedSkill = new List<SkillData>();
        foreach (var skillData in skillList)
        {
            if (skillData.scoreToUnlock < currentScore)
            {
                unlockedSkill.Add(skillData);
            }
        }

        UpdateInteractionAbility(unlockedSkill);
    }

    private void UpdateInteractionAbility(List<SkillData> unlockedSkill)
    {
        foreach (var skillData in unlockedSkill)
        {
            switch (skillData.skillType)
            {
                case SkillType.Interaction:
                    rayCaster.EnableInteraction();
                    break;
                case SkillType.PickupObject:
                    rayCaster.EnablePickup();
                    break;
                case SkillType.SelfReveal :
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }    
    }
}
