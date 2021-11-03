using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginNewRoundState : BattleStateClass
{
    public BeginNewRoundState(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    public override void Begin()
    {
        BattleSystem.combatants = new List<Unit>();
        BattleSystem.combatants.AddRange(BattleSystem.playerContoller.GetUnits());
        BattleSystem.combatants.AddRange(BattleSystem.enemyController.GetUnits());

        foreach (Unit unit in BattleSystem.playerContoller.GetUnits())
        {
            unit.ResetAllStats();
        }

        foreach (Unit unit in BattleSystem.enemyController.GetUnits())
        {
            unit.level++;
        }

        BattleSystem.InitializeParty(AllPossibleUnits.allPlayerUnits, BattleSystem.enemyController.unitParty);

        BattleSystem.activeUnit = BattleSystem.playerContoller.unitParty.GetPartyMemeber(0);
        BattleSystem.battleTimer = new BattleTimer(BattleSystem.combatants);

        BattleSystem.StartBattleEvent.Invoke();
        BattleSystem.UpdateStatBlocks();

        ChangeState();
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BetweenState(BattleSystem));
    }
}
