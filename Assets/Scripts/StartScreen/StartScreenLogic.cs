using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreenLogic : MonoBehaviour
{
    public List<Unit> party = new List<Unit>();
    [HideInInspector]
    private int partySize = 2;
    [HideInInspector]
    public List<Unit> allUnitChoices;
    public int numUnitChoices;
    public int cash;
    public int cost;

    public PlayerController playerController;
    [HideInInspector]
    public int globalLevel;

    [SerializeField] public UnityEvent StartScreenStartEvent;
    [SerializeField] public AttemptLevelUpEvent AttemptLevelUpEvent;
    [SerializeField] public SetupFinishEvent SetupFinishEvent;

    public void Start()
    {
        globalLevel = playerController.level;
        cash = playerController.cash;
        MakeAllUnits();
        ResetParty();
        StartScreenStartEvent.Invoke();
    }


    private void MakeAllUnits()
    {
        if (allUnitChoices.Count == 0)
        {
            for (int ii = 0; ii < numUnitChoices; ii++)
            {
                Unit unit = this.gameObject.AddComponent(typeof(Unit)) as Unit;
                allUnitChoices.Add(unit);
            }
        }
        List<UnitBase> unitBases = AllPossibleUnits.GetListOfUnits(numUnitChoices);
        for (int ii = 0; ii < numUnitChoices; ii++)
        {
            allUnitChoices[ii].baseUnit = unitBases[ii];
        }
    }

    public void ResetParty()
    {
        party = new List<Unit>();
    }

    public void InitializeChoicesList()
    {
        for (int ii = 0; ii < numUnitChoices; ii++)
        {
            Unit unit = this.gameObject.AddComponent(typeof(Unit)) as Unit;
            allUnitChoices.Add(unit);
        }
    }

    public void InitializeChoices(List<UnitBase> allPossibleUnits)
    {
        List<UnitBase> allPossibleUnitsCopy = new List<UnitBase>(allPossibleUnits);
        foreach (Unit unit in allUnitChoices)
        {
            int index = Random.Range(0, allPossibleUnitsCopy.Count);
            unit.baseUnit = allPossibleUnitsCopy[index];
            allPossibleUnitsCopy.RemoveAt(index);
        }
    }

    public void IncreaseGlobalLevel()
    {
        AttemptLevelUpEvent.Invoke(cost);
    }

    public void AddUnitToParty(Unit unit)
    {
        party.Add(unit);
        if (party.Count >= partySize)
        {
            Finish();
        }

    }

    public void Finish()
    {
        foreach (Unit unit in party)
        {
            unit.level = globalLevel;
            unit.InitializeStats();
        }
        playerController.unitParty.partyMembers = party;
        SetupFinishEvent.Invoke(party, globalLevel);
    }
}

[System.Serializable]
public class SetupFinishEvent : UnityEvent<List<Unit>, int>
{

}

[System.Serializable]
public class AttemptLevelUpEvent : UnityEvent<int>
{

}
