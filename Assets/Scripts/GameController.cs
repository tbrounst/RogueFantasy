using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private enum State { START_SCREEN, BATTLE_SCREEN};
    //private State currentState = State.START_SCREEN;

    public BattleSystem battleSystem;
    public StartScreenLogic startScreen;

    //public int numUnitChoices;
    //public AllPossibleUnits apu;
    //private List<Unit> unitsRandomized;

    //public PlayerController playerController;

    public void Awake()
    {
        //MakeAllUnits();
        SwitchToStart();
    }

    //TODO: add to start screen logic
    //private void MakeAllUnits()
    //{
    //    unitsRandomized = new List<Unit>();
    //    foreach(UnitBase unitBase in apu.allPlayerUnits) {
    //        Unit unit = this.gameObject.AddComponent(typeof(Unit)) as Unit;
    //        unit.baseUnit = unitBase;
    //        unitsRandomized.Add(unit);
    //    }
    //}

    public void SwitchToStart()
    {
        //Utils.Shuffle<Unit>(unitsRandomized);
        //startScreen.allUnitChoices = unitsRandomized.GetRange(0, numUnitChoices);
        //startScreen.globalLevel = globalLevel;
        //startScreen.cash = cash;
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
        startScreen.playerController.cash += round;
        SwitchToStart();
    }
}
