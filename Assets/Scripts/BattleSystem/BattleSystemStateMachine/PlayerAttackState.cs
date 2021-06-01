using System.Collections;
using UnityEngine;

internal class PlayerAttackState : BattleStateClass
{
    private bool isActing = false;

    public PlayerAttackState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void QueueAttack(int attackNum)
    {
        if (isActing)
        {
            return;
        }
        isActing = true;
        BattleSystem.queuedAttack = BattleSystem.activeUnit.currentAbilities[attackNum];

        if (BattleSystem.queuedAttack.requiresTarget)
        {
            BattleSystem.UpdateGameDescriptionEvent.Invoke("Select a target");
            //guiLayer.UpdateGameDescriptionWithoutTyping("Select a target");
        }
        else
        {
            BattleSystem.PerformAttack(BattleSystem.queuedAttack, BattleSystem.activeUnit, null);
            //ChangeState();
        }

    }

    public override void Target(Unit target)
    {
        if (BattleSystem.queuedAttack == null)
        {
            return;
        }
        BattleSystem.PerformAttack(BattleSystem.queuedAttack, BattleSystem.activeUnit, target);
        //ChangeState();
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BetweenState(BattleSystem));
    }
}