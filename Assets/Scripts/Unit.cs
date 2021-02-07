using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
    public UnitBase baseUnit;
    public UnitStats unitStats;
    public int level;

    public List<Ability> currentAbilities = new List<Ability>(4);

    public void Initialize()
    {
        List<float> randMods = new List<float>();
        float total = 0f;
        string debugString = "";
        List<int> scaledStats = new List<int>();

        for(int ii = 0; ii < baseUnit.stats.Length; ii++)
        {
            float scaledStat = Random.Range(0.0F, 1.0F);
            randMods.Add(scaledStat);
            total += scaledStat;
            debugString += string.Format("Random{0}: {1} ", ii.ToString(), scaledStat.ToString());
        }
        debugString += "Total: " + total.ToString();
        //Debug.Log(debugString);

        for(int ii = 0; ii < baseUnit.stats.Length; ii++)
        {
            scaledStats.Add((int)Mathf.Ceil((randMods[ii] / total) * baseUnit.randomStats + baseUnit.stats[ii]));
        }

        unitStats = new UnitStats(scaledStats, level);

        List<Ability> localPotentialAbilities = new List<Ability>(baseUnit.potentialAbilities);
        Utils.Shuffle<Ability>(localPotentialAbilities);
        currentAbilities = localPotentialAbilities.GetRange(0, 4);
    }

    public bool TakeDamage(int dmg)
    {
        return unitStats.TakeDamage(dmg);
    }

    public void Heal(int amount)
    {
        unitStats.Heal(amount);
    }

    public void BoostStat(StatsEnum stat, StatModifier mod)
    {
        unitStats.ModifyStat(stat, mod);
    }

    public int GetStat(StatsEnum stat)
    {
        return unitStats.GetStat(stat);
    }

    public void LevelUp()
    {
        LevelUp(1);
    }

    public void LevelUp(int levels)
    {
        level += levels;
        unitStats.level += levels;
        unitStats.SetStatsBasedOnLevel();
    }

    public int GetCurrentHP()
    {
        return unitStats.GetCurrentHP();
    }

    public bool IsDead()
    {
        return (unitStats.GetCurrentHP() < 1);
    }


    public string GetName()
    {
        return baseUnit.nameOfUnit;
    }
    
    override
    public string ToString()
    {
        return baseUnit.nameOfUnit;
    }
}
