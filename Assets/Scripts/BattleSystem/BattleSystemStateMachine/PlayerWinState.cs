using System.Collections;
using UnityEngine;

internal class PlayerWinState : BattleStateClass
{
    public PlayerWinState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void Begin()
    {
        BattleSystem.UpdateStatBlocks();
        BattleSystem.StartCoroutine(ProcessVictory());
    }

    private IEnumerator ProcessVictory()
    {
        yield return new WaitForSeconds(1f);
        BattleSystem.round++;
        BattleSystem.UpdateGameDescriptionEvent.Invoke($"Now for round {BattleSystem.round}.");
        //guiLayer.UpdateGameDescriptionWithoutTyping($"Now for round {round}.");
        ChangeState();
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BeginNewRoundState(BattleSystem));
    }
}