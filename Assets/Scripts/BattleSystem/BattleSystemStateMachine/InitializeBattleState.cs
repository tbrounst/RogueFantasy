﻿internal class InitializeBattleState : BattleStateClass
{
    public InitializeBattleState(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override void Begin()
    {
        //BattleSystem.InitializeParty(BattleSystem.aus.allPlayerUnits, BattleSystem.playerParty);
        //foreach(Unit unit in BattleSystem.playerContoller.GetUnits())
        //{
        //    unit.InitializeStats();
        //}

        //TODO double check all this logic and clean up
        if (BattleSystem.playerLevels == -1)
        {
            BattleSystem.playerLevels = BattleSystem.playerContoller.unitParty.GetPartyMemeber(0).level;
        }
        else
        {
            foreach (Unit unit in BattleSystem.playerContoller.GetUnits())
            {
                unit.level = BattleSystem.playerLevels;
            }
        }

        if (BattleSystem.enemyController.GetUnits().Count == 0)
        {
            for (int ii = 0; ii < BattleSystem.playerContoller.GetUnits().Count; ii++)
            {
                Unit unit = BattleSystem.enemyController.gameObject.AddComponent(typeof(Unit)) as Unit;
                BattleSystem.enemyController.GetUnits().Add(unit);
            }
        }

        if (BattleSystem.enemyLevels == -1)
        {
            BattleSystem.enemyLevels = BattleSystem.enemyController.unitParty.GetPartyMemeber(0).level;
        }
        else
        {
            foreach (Unit unit in BattleSystem.enemyController.GetUnits())
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