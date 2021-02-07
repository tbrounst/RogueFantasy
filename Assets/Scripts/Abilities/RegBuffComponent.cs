using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BuffComponent")]

public class RegBuffComponent : AbilityComponent
{
    public StatsEnum stat;
    public int baseAmount = 10;

    public override string UseComponent(Unit caster, Unit target)
    {
        string result = "Boosted stat " + stat.ToString() + " by " + baseAmount;
        StatModifier mod = new StatModifier(baseAmount, StatModType.FLAT, true);
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
        return $"Boost {targetString} {stat} by {baseAmount}.";
    }

}
