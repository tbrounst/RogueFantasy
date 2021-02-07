using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType { FLAT, PERCENT }
public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    public readonly bool battleBuff;

    public StatModifier(float value, StatModType type, bool battleBuff)
    {
        this.value = value;
        this.type = type;
        this.battleBuff = battleBuff;
    }
}
