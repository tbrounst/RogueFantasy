using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DebuffComponent")]

public class RegDebuffComponent : AbilityComponent
{
    public StatsEnum stat;
    public int baseAmount = 10;

    public override string UseComponent(Unit caster, Unit target)
    {
        string result = "Lowered stat " + stat.ToString() + " by " + baseAmount;
        StatModifier mod = new StatModifier(-baseAmount, StatModType.FLAT, true);
        target.BoostStat(stat, mod);
        Debug.Log("<color=red>" + result + "</color>");
        return result;
    }

    public override string GetDescription()
    {
        string targetString = "enemy";
        if (targetSelf)
        {
            targetString = "your";
        }
        return $"Lowers {targetString} {stat} by {baseAmount}.";
    }
}
