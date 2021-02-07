using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityComponent : ScriptableObject
{
    public bool targetSelf;
    public abstract string UseComponent(Unit caster, Unit target);

    public abstract string GetDescription();

    protected int BasicDamageComputation(Unit caster, Unit target, int baseDamage, bool magic)
    {
        float firstElem = ((2 * (float)caster.level / 5) + 2);
        float secondElem = firstElem * baseDamage;
        float thirdElem;
        if (magic)
        {
            thirdElem = ((float)caster.GetStat(StatsEnum.MAGICATTACK) / target.GetStat(StatsEnum.MAGICDEFENSE));
        } else
        {
            thirdElem = ((float)caster.GetStat(StatsEnum.ATTACK) / target.GetStat(StatsEnum.DEFENSE));
        }
        int totalDamage = Mathf.RoundToInt((secondElem * thirdElem) / 50 + 2);
        return totalDamage;
    }
}
