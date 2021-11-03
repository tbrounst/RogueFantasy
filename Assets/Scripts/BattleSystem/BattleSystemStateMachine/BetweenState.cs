using System.Collections;
using UnityEngine;

internal class BetweenState : BattleStateClass
{
    public BetweenState(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    public override void Begin()
    {
        ChangeState();
    }

    public override void ChangeState()
    {
        if (BattleSystem.playerContoller.unitParty.IsPartyDefeated())
        {
            BattleSystem.SetState(new PlayerLoseState(BattleSystem));
        }
        else if (BattleSystem.enemyController.unitParty.IsPartyDefeated())
        {
            Debug.Log("Enemy is dead");
            BattleSystem.SetState(new PlayerWinState(BattleSystem));
        } else
        {
            BattleSystem.activeUnit = BattleSystem.battleTimer.NextToGo();
            BattleSystem.UpdateStatBlocks();
            if (BattleSystem.enemyController.unitParty.InParty(BattleSystem.activeUnit))
            {
                BattleSystem.SetState(new EnemyAttackState(BattleSystem));
            }
            else
            {
                BattleSystem.StartPlayerTurnEvent.Invoke();
                BattleSystem.SetState(new PlayerAttackState(BattleSystem));
            }
        } 
    }
}