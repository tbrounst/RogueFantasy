internal class InitializeBattleState : BattleStateClass
{
    public InitializeBattleState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void Begin()
    {
        //BattleSystem.InitializeParty(BattleSystem.aus.allPlayerUnits, BattleSystem.playerParty);
        foreach(Unit unit in BattleSystem.playerParty.partyMembers)
        {
            unit.InitializeStats();
        }

        if (BattleSystem.playerLevels == -1)
        {
            BattleSystem.playerLevels = BattleSystem.playerParty.GetPartyMemeber(0).level;
        }
        else
        {
            foreach (Unit unit in BattleSystem.playerParty.partyMembers)
            {
                unit.level = BattleSystem.playerLevels;
            }
        }

        if (BattleSystem.enemyLevels == -1)
        {
            BattleSystem.enemyLevels = BattleSystem.enemyParty.GetPartyMemeber(0).level;
        }
        else
        {
            foreach (Unit unit in BattleSystem.enemyParty.partyMembers)
            {
                unit.level = BattleSystem.enemyLevels;
            }
        }

        ChangeState();
    }

    public override void ChangeState()
    {
        BattleSystem.SetState(new BeginNewRoundState(BattleSystem));
    }
}