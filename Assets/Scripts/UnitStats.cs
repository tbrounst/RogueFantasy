using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum StatsEnum { HP, ATTACK, DEFENSE, MAGICATTACK, MAGICDEFENSE, SPEED }
public class UnitStats
{
    private readonly List<int> baseStats;
    private List<Stat> stats = new List<Stat>();
    private int currentHP;
    public int level;

    public UnitStats(List<int> stats, int level)
    {
        this.level = level;
        if (stats.Count != Enum.GetNames(typeof(StatsEnum)).Length)
        {
            Debug.Log("Stats supplied to UnitStats is not the right legnth");
            return;
        }
        baseStats = stats;
        SetStatsBasedOnLevel();

        ResetCurrentHP();
    }

    public int StatAtLevel(int stat, bool isHP)
    {
        if (isHP)
        {
            return ((int)Math.Floor((double)(stat * 2 * level) / 100) + 10 + level);
        }
        return ((int) Math.Floor((double) (stat * 2 * level)/100) + 5);
    }

    public void SetStatsBasedOnLevel()
    {
        stats.Clear();
        foreach (StatsEnum statEnum in Enum.GetValues(typeof(StatsEnum)))
        {
            int baseStatValue = baseStats[(int)statEnum];
            Stat newStat;
            if (statEnum == StatsEnum.HP)
            {
                newStat = new Stat(StatAtLevel(baseStatValue, true));
            } else
            {
                newStat = new Stat(StatAtLevel(baseStatValue, false));
            }
            stats.Add(newStat);
        }
    }

    public int GetStat(StatsEnum stat)
    {
        return stats[(int)stat].currentValue;
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public void ModifyStat(StatsEnum stat, StatModifier mod)
    {
        stats[(int)stat].AddModifier(mod);
    }

    public void ResetAllStats()
    {
        foreach (Stat stat in stats) {
            stat.ResetStat();
        }
        ResetCurrentHP();
    }

    public void ResetCurrentHP()
    {
        currentHP = GetStat(StatsEnum.HP);
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > stats[(int)StatsEnum.HP].baseValue)
        {
            currentHP = stats[(int)StatsEnum.HP].baseValue;
        }
    }
}
