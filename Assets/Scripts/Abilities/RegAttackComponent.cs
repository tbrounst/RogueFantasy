using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/AttackComponent")]
public class RegAttackComponent : AbilityComponent
{
    public int baseDamage = 10;
    public double accuracy = 1.0;

    public override string UseComponent(Unit caster, Unit target)
    {
        //float firstElem = ((2 * (float) caster.level / 5) + 2);
        //float secondElem = firstElem * baseDamage * ((float) caster.GetStat(StatsEnum.ATTACK) / target.GetStat(StatsEnum.DEFENSE));
        //Debug.Log($"First elem: {firstElem} second elem: {secondElem}");
        //int totalDamage = Mathf.RoundToInt(secondElem / 50 + 2);
        //int totalDamage = Mathf.Max(baseDamage + caster.GetStat(StatsEnum.ATTACK) - target.GetStat(StatsEnum.DEFENSE), 1);
        int totalDamage = BasicDamageComputation(caster, target, baseDamage, false);
        string result = "This attack did " + totalDamage + " damage";
        target.TakeDamage(totalDamage);
        Debug.Log("<color=red>" + result + "</color>");
        return result;
    }

    public override string GetDescription()
    {
        return $"Regular attack with {baseDamage} base damage.";
    }

}
