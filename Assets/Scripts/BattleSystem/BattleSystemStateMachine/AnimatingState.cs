internal class AnimatingState : BattleStateClass
{
    public AnimatingState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BetweenState(BattleSystem));
    }
}