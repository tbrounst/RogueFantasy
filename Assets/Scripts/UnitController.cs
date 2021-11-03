using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    public Party unitParty;

    public List<Unit> GetUnits()
    {
        return unitParty.partyMembers;
    }
}
