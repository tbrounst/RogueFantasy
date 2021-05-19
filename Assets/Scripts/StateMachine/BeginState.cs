using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginState : BattleStateClass
{
    public BeginState(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    // Start is called before the first frame update
    public override IEnumerator Start()
    {
        BattleSystem.combatants = new List<Unit>();
        BattleSystem.combatants.AddRange(BattleSystem.playerParty.partyMembers);
        BattleSystem.combatants.AddRange(BattleSystem.enemyParty.partyMembers);

        foreach (Unit unit in BattleSystem.playerParty.partyMembers)
        {
            unit.unitStats.ResetAllStats();
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

        BattleSystem.SetState(new BetweenState(BattleSystem));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
