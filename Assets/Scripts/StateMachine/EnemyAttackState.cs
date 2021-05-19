using UnityEngine;

internal class EnemyAttackState : BattleStateClass
{
    public EnemyAttackState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public virtual IEnumerator Attack()
    {
        int moveToUseInt = Random.Range(0, BattleSystem.activeUnit.currentAbilities.Count);
        Ability moveToUse = BattleSystem.activeUnit.currentAbilities[moveToUseInt];

        int enemyTarget = 0;
        if (moveToUse.requiresTarget)
        {
            enemyTarget = Random.Range(0, BattleSystem.playerParty.PartySize());
            while (BattleSystem.playerParty.GetPartyMemeber(enemyTarget).IsDead())
            {
                enemyTarget = Random.Range(0, BattleSystem.playerParty.PartySize());
            }
        }

        BattleSystem.PerformAttack(moveToUse, BattleSystem.activeUnit, BattleSystem.playerParty.GetPartyMemeber(enemyTarget));
        BattleSystem.SetState(new BetweenState(BattleSystem));
    }
}