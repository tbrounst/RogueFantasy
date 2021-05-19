using System.Collections;

internal class BetweenState : BattleStateClass
{
    public BetweenState(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    public override IEnumerator Start()
    {
        BattleSystem.activeUnit = BattleSystem.battleTimer.NextToGo();
        BattleSystem.UpdateStatBlocks();
        if (BattleSystem.enemyParty.InParty(BattleSystem.activeUnit))
        {
            BattleSystem.SetState(new EnemyAttackState(BattleSystem));
        }
        else
        {
            StartPlayerTurnEvent.Invoke();
            BattleSystem.SetState(new PlayerAttackState(BattleSystem));

            //guiLayer.EnableAttackButtons(true);
            //guiLayer.UpdateGameDescriptionWithoutTyping("Select an attack");
        }
    }
}