using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BattleSystem battleSystem;
    public StartScreenLogic startScreen;

    public int numUnitChoices;
    public AllPossibleUnits apu;
    private List<Unit> unitsRandomized;
    public int globalLevel;
    public int cash;

    public void Awake()
    {
        MakeAllUnits();
        SwitchToStart();
    }

    private void MakeAllUnits()
    {
        unitsRandomized = new List<Unit>();
        foreach(UnitBase unitBase in apu.allPlayerUnits) {
            Unit unit = this.gameObject.AddComponent(typeof(Unit)) as Unit;
            unit.baseUnit = unitBase;
            unitsRandomized.Add(unit);
        }
    }

    public void SwitchToStart()
    {
        Utils.Shuffle<Unit>(unitsRandomized);
        startScreen.allUnitChoices = unitsRandomized.GetRange(0, numUnitChoices);
        startScreen.globalLevel = globalLevel;
        startScreen.cash = cash;
        battleSystem.gameObject.SetActive(false);
        startScreen.gameObject.SetActive(true);
        startScreen.Start();
    }

    public void SwitchToBattle(List<Unit> party, int level)
    {
        battleSystem.playerParty.partyMembers = party;
        battleSystem.playerLevels = level;
        startScreen.gameObject.SetActive(false);
        battleSystem.gameObject.SetActive(true);
        battleSystem.SetState(new InitializeBattleState(battleSystem));
    }

    public void BattleResetListener(int round)
    {
        cash += round;
        SwitchToStart();
    }
}
