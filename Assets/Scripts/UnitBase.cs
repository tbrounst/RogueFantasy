using System.Collections;
using System.Collections.Generic;
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
}
