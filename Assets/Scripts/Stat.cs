using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    public int baseValue;
    public int currentValue { 
        get {
            if (isDirty)
            {
                _value = GetValue();
                isDirty = false;
            }
            return _value; 
        } 
    }

    private bool isDirty = true;
    private int _value;

    private readonly List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(int baseValue)
    {
        this.baseValue = baseValue;
        modifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        modifiers.Add(mod);
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return modifiers.Remove(mod);
    }

    private int GetValue()
    {
        float finalValue = baseValue;

        for (int ii = 0; ii < modifiers.Count; ii++)
        {
            StatModifier mod = modifiers[ii];

            float totalMult = 1.0F;

            if (mod.type == StatModType.FLAT)
            {
                finalValue += mod.value;
            }

            else if (mod.type == StatModType.PERCENT)
            {
                totalMult += mod.value;
            }

            finalValue *= totalMult;
        }
        return (int) Mathf.Max(Mathf.Round(finalValue), 1f);
    }

    public void ResetStat()
    {
        if (modifiers.Count == 0)
        {
            return;
        }

        for (int ii = modifiers.Count - 1; ii > -1; ii--)
        {
            StatModifier sm = modifiers[ii];
            if (sm.battleBuff)
            {
                RemoveModifier(sm);
            }
        }
        isDirty = true;
    }
}
