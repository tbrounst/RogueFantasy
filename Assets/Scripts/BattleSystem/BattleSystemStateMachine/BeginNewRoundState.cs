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
        BattleSystem.combatants.AddRange(BattleSystem.playerParty.partyMembers);
        BattleSystem.combatants.AddRange(BattleSystem.enemyParty.partyMembers);

        foreach (Unit unit in BattleSystem.playerParty.partyMembers)
        {
            unit.ResetAllStats();
        }

        foreach (Unit unit in BattleSystem.enemyParty.partyMembers)
        {
            unit.level++;
        }

        BattleSystem.InitializeParty(BattleSystem.aus.allPlayerUnits, BattleSystem.enemyParty);

        BattleSystem.activeUnit = BattleSystem.playerParty.GetPartyMemeber(0);
        BattleSystem.battleTimer = new BattleTimer(BattleSystem.combatants);

        BattleSystem.StartBattleEvent.Invoke();
        //guiLayer.SetUp(combatants);
        //guiLayer.SetRoundText(round);
        //guiLayer.ToggleResetButton(false);
        BattleSystem.UpdateStatBlocks();

        ChangeState();
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BetweenState(BattleSystem));
    }
}
