using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


//public enum BattleState { START, PLAYERTURN, ENEMYTURN, ACTING, BETWEEN, WIN, LOSE }
public class BattleSystem : StateMachine
{
    [HideInInspector]
    public BattleTimer battleTimer;

    public AllPossibleUnits aus;

    public PlayerController playerContoller;
    public EnemyController enemyController;

    public List<Unit> combatants;

    public Unit activeUnit;
    public Ability queuedAttack;

    [HideInInspector]
    public int round = 1;
    [HideInInspector]
    public int playerLevels = -1;
    [HideInInspector]
    public int enemyLevels = -1;

    //Events

    [SerializeField] public UpdateGameDescriptionEvent UpdateGameDescriptionEvent;
    [SerializeField] public UnityEvent StartBattleEvent;
    [SerializeField] public UnityEvent StartPlayerTurnEvent;
    [SerializeField] AttackEvent AttackEvent;
    [SerializeField] ToolTipEvent ToolTipEvent;
    [SerializeField] BattleLossEvent BattleLossEvent;
    [SerializeField] ResetEvent ResetEvent;
    [SerializeField] UnityEvent UpdateStatBlockEvent;

    //Getters

    public int GetRound()
    {
        return round;
    }

    //Setup game

    private void StartNewRound()
    {
        SetState(new BeginNewRoundState(this));
    }

    //TODO: Rework this with controllers
    public void InitializeParty(List<UnitBase> allPossibleUnits, Party party)
    {
        List<UnitBase> allPossibleUnitsCopy = new List<UnitBase>(allPossibleUnits);
        Utils.Shuffle<UnitBase>(allPossibleUnitsCopy);
        int index = 0;
        foreach (Unit unit in party.partyMembers)
        {
            unit.baseUnit = allPossibleUnitsCopy[index];
            index++;
            unit.InitializeStats();
        }
    }

    //Attack actions

    public void CreateToolTip(int attackNum)
    {
        Ability ability = activeUnit.currentAbilities[attackNum];
        string toolTipText = ability.GenerateToolTipText();
        ToolTipEvent.Invoke(toolTipText);
    }

    public void QueueAttack(int attackNum)
    {
        state.QueueAttack(attackNum);
        
    }

    public void PlayerAttack(Unit target)
    {
        state.Target(target);
    }

    public void PerformAttack(Ability attack, Unit caster, Unit target)
    {
        queuedAttack = null;
        Debug.Log(attack.abilityName);
        List<string> attackOutputs = attack.UseAbility(caster, target);
        AttackEvent.Invoke(attack, attackOutputs, caster, target);
    }

    public IEnumerator PlayerWin()
    {
        yield return new WaitForSeconds(1f);
        round++;
        UpdateGameDescriptionEvent.Invoke($"Now for round {round}.");
        StartNewRound();
    }

    public IEnumerator PlayerLose()
    {
        yield return new WaitForSeconds(1f);
        BattleLossEvent.Invoke($"You made it to round {round}.");
    }

    public void ThrowResetEvent()
    {
        ResetEvent.Invoke(round - enemyLevels);
    }

    public void UpdateStatBlocks()
    {
        UpdateStatBlockEvent.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}


//Event subclasses
[System.Serializable]
public class AttackEvent : UnityEvent<Ability, List<string>, Unit, Unit>
{

}

[System.Serializable]
public class BattleLossEvent : UnityEvent<string>
{

}

[System.Serializable]
public class ResetEvent : UnityEvent<int>
{

}

[System.Serializable]
public class UpdateGameDescriptionEvent : UnityEvent<string>
{

}

[System.Serializable]
public class ToolTipEvent : UnityEvent<string>
{

}