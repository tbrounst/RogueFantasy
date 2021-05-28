﻿using System.Collections;

internal class PlayerLoseState : BattleStateClass
{
    public PlayerLoseState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void Start()
    {
        BattleSystem.UpdateStatBlocks();
        BattleSystem.StartCoroutine(BattleSystem.PlayerLose());
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new InitializeBattleState(BattleSystem));
    }

    public override void Reset()
    {
        BattleSystem.Start();
    }
}