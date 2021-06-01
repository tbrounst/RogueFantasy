using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/RegUnit")]
public class UnitBase : ScriptableObject
{

    public string nameOfUnit;
    public Sprite image;

    [EnumNamedArray(typeof(StatsEnum))]
    public int[] stats = new int[System.Enum.GetNames(typeof(StatsEnum)).Length];
    public int randomStats;

    public List<Ability> potentialAbilities;

    public int GetTotalStats()
    {
        int total = 0;
        foreach(int stat in stats)
        {
            total += stat;
        }
        total += randomStats;
        return total;
    }

    override
    public string ToString()
    {
        StringBuilder sb = new StringBuilder(nameOfUnit + "\n");
        foreach (int ii in System.Enum.GetValues(typeof(StatsEnum)))
        {
            sb.Append($"{System.Enum.GetName(typeof(StatsEnum), ii)}: {stats[ii]}\n");
        }
        sb.Append($"Random stats: {randomStats}\n");
        sb.Append($"Total stats: {GetTotalStats()}");
        return sb.ToString();
    }
}
