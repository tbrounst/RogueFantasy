using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Party
{
    public List<Unit> partyMembers;

    public Party(List<Unit> partyMembers)
    {
        this.partyMembers = partyMembers;
    }

    public Unit GetPartyMemeber(int index)
    {
        return partyMembers[index];
    }

    public bool InParty(Unit unit)
    {
        return partyMembers.Contains(unit);
    }

    public int PartySize()
    {
        return partyMembers.Count;
    }

    public bool IsPartyDefeated()
    {
        foreach (Unit unit in partyMembers)
        {
            if (!unit.IsDead())
            {
                return false;
            }
        }
        return true;
    }

}
