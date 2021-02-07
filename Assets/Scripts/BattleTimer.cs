using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer
{
    private List<Unit> combatants;
    private Dictionary<Unit, int> ticks;
    private Stack<Unit> stack;
    public readonly int timeNeeded = 1009;

    public BattleTimer(List<Unit> combatants)
    {
        this.combatants = combatants;
        ticks = new Dictionary<Unit, int>();
        foreach(Unit unit in combatants)
        {
            ticks.Add(unit, Random.Range(0, 10));
        }
        this.stack = new Stack<Unit>();
    }

    public Unit NextToGo()
    {
        if (stack.Count > 0)
        {
            return stack.Pop();
        }

        List<Unit> aboveValue = new List<Unit>();
        int minStep = timeNeeded + 1;

        foreach(Unit unit in combatants)
        {
            if (!unit.IsDead())
            {
                int stepsRequired = (int)Mathf.Ceil((timeNeeded - ticks[unit]) / (float)unit.GetStat(StatsEnum.SPEED));
                if (stepsRequired < minStep)
                {
                    minStep = stepsRequired;
                }
            }
        }

        string debugString = "";
        foreach(Unit unit in combatants)
        {
            if (!unit.IsDead())
            {
                int totalTicks = ticks[unit] + unit.GetStat(StatsEnum.SPEED) * minStep;
                if (totalTicks >= timeNeeded)
                {
                    totalTicks -= (timeNeeded + Random.Range(0, 5));
                    totalTicks = Mathf.Max(totalTicks, 0);
                    aboveValue.Add(unit);
                }
                ticks[unit] = totalTicks;
                debugString += unit.GetName() + " " + totalTicks.ToString() + "; ";
            }
        }

        Debug.Log(debugString);
        Debug.Log(string.Join(", ", aboveValue));

        if (aboveValue.Count == 1)
        {
            return aboveValue[0];
        }

        return TieBreaker(aboveValue);
    }

    private Unit TieBreaker(List<Unit> aboveValue)
    {
        Debug.Log("We have a tie!");
        Dictionary<int, List<Unit>> speedsToUnits = new Dictionary<int, List<Unit>>();
        foreach(Unit unit in aboveValue)
        {
            int speed = ticks[unit];
            if (!speedsToUnits.ContainsKey(speed))
            {
                speedsToUnits.Add(speed, new List<Unit>());
            }
            speedsToUnits[speed].Add(unit);
        }
        List<int> speeds = new List<int>(speedsToUnits.Keys);
        speeds.Sort();

        foreach(int speed in speeds)
        {
            List<Unit> units = speedsToUnits[speed];
            Utils.Shuffle(units);
            units.ForEach(o => stack.Push(o));
        }

        return stack.Pop();
    }
}
