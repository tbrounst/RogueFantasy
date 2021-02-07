using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/MAttackComponent")]
public class MAttackComponent : AbilityComponent
{
    public int baseDamage = 10;
    public double accuracy = 1.0;

    public override string UseComponent(Unit caster, Unit target)
    {
        //int totalDamage = Mathf.Max(baseDamage + caster.GetStat(StatsEnum.MAGICATTACK) - target.GetStat(StatsEnum.MAGICDEFENSE), 1);
        int totalDamage = BasicDamageComputation(caster, target, baseDamage, false);
        string result = "This attack did " + totalDamage + " damage";
        target.TakeDamage(totalDamage);
        Debug.Log("<color=red>" + result + "</color>");
        return result;
    }

    public override string GetDescription()
    {
        return $"Magic attack with {baseDamage} base damage.";
    }

}
