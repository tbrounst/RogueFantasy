using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private enum State { START_SCREEN, BATTLE_SCREEN};

    public BattleSystem battleSystem;
    public StartScreenLogic startScreen;

    public void Awake()
    {
        SwitchToStart();
    }

    public void SwitchToStart()
    {
        battleSystem.gameObject.SetActive(false);
        startScreen.gameObject.SetActive(true);
        startScreen.Start();
    }

    public void SwitchToBattle(List<Unit> party, int level)
    {
        battleSystem.playerContoller.unitParty.partyMembers = party;
        battleSystem.playerLevels = level;
        startScreen.gameObject.SetActive(false);
        battleSystem.gameObject.SetActive(true);
        battleSystem.SetState(new InitializeBattleState(battleSystem));
    }

    public void BattleResetListener(int round)
    {
        startScreen.playerController.cash += round;
        SwitchToStart();
    }
}
