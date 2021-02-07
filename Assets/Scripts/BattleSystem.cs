using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, ACTING, BETWEEN, WIN, LOSE }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    private BattleTimer battleTimer;

    public AllPossibleUnits aus;

    public Party playerParty;
    public Party enemyParty;

    //[HideInInspector]
    public List<Unit> combatants;

    public Unit activeUnit;
    private Ability queuedAttack;

    public GUILayer guiLayer;

    private int round = 0;

    //Unity functions

    private void Start()
    {
        if (guiLayer.attackButtons.Count != 4)
        {
            Debug.Log("Buttons not synced correctly");
            return;
        }
        InitializeParty(aus.allPlayerUnits, playerParty);
        
        StartNewRound();
    }

    private void Update()
    {
        if (state == BattleState.BETWEEN)
        {
            if (playerParty.IsPartyDefeated())
            {
                state = BattleState.LOSE;
                EndBattle();
                return;
            }
            if (enemyParty.IsPartyDefeated())
            {
                Debug.Log("Enemy is dead");
                state = BattleState.WIN;
                EndBattle();
                return;
            }

            activeUnit = battleTimer.NextToGo();
            UpdateStatBlocks();
            if (enemyParty.InParty(activeUnit))
            {
                EnemyAttack();
            } else
            {
                state = BattleState.PLAYERTURN;
                guiLayer.EnableAttackButtons(true);
                guiLayer.UpdateGameDescriptionWithoutTyping("Select an attack");
            }
        }
    }

    //Setup game

    private void StartNewRound()
    {
        state = BattleState.START;

        combatants = new List<Unit>();
        combatants.AddRange(playerParty.partyMembers);
        combatants.AddRange(enemyParty.partyMembers);

        foreach (Unit unit in playerParty.partyMembers)
        {
            unit.unitStats.ResetAllStats();
        }

        foreach (Unit unit in enemyParty.partyMembers)
        {
            unit.level++;
        }

        InitializeParty(aus.allEnemyUnits, enemyParty);

        activeUnit = playerParty.GetPartyMemeber(0);
        battleTimer = new BattleTimer(combatants);

        guiLayer.SetUp(combatants);
        UpdateStatBlocks();

        state = BattleState.BETWEEN;
    }

    private void InitializeParty(List<UnitBase> allPossibleUnits, Party party)
    {
        List<UnitBase> allPossibleUnitsCopy = new List<UnitBase>(allPossibleUnits);
        foreach (Unit unit in party.partyMembers)
        {
            int index = Random.Range(0, allPossibleUnitsCopy.Count);
            unit.baseUnit = allPossibleUnitsCopy[index];
            allPossibleUnitsCopy.RemoveAt(index);
            unit.Initialize();
        }
    }

    //Attack actions

    public void CreateToolTip(int attackNum)
    {
        Ability ability = activeUnit.currentAbilities[attackNum];
        string toolTipText = ability.GenerateToolTipText();
        guiLayer.CreateAttackToolTip(toolTipText);
    }

    public void QueueAttack(int attackNum)
    {
        if (state != BattleState.PLAYERTURN)
        {
            Debug.Log("It's not your turn");
            return;
        }

        state = BattleState.ACTING;
        queuedAttack = activeUnit.currentAbilities[attackNum];

        if (queuedAttack.requiresTarget)
        {
            guiLayer.UpdateGameDescriptionWithoutTyping("Select a target");
        } else
        {
            StartCoroutine(PerformAttack(queuedAttack, activeUnit, null));
        }
        
    }

    public void PlayerAttack(Unit target)
    {
        if (queuedAttack == null)
        {
            return;
        }
        StartCoroutine(PerformAttack(queuedAttack, activeUnit, target));
    }

    public void EnemyAttack()
    {
        state = BattleState.ACTING;
        int moveToUseInt = Random.Range(0, activeUnit.currentAbilities.Count);
        Ability moveToUse = activeUnit.currentAbilities[moveToUseInt];

        int enemyTarget = 0;
        if (moveToUse.requiresTarget)
        {
            enemyTarget = Random.Range(0, playerParty.PartySize());
            while (playerParty.GetPartyMemeber(enemyTarget).IsDead())
            {
                enemyTarget = Random.Range(0, playerParty.PartySize());
            }
        }

        StartCoroutine(PerformAttack(moveToUse, activeUnit, playerParty.GetPartyMemeber(enemyTarget)));
    }

    public IEnumerator PerformAttack(Ability attack, Unit caster, Unit target)
    {
        guiLayer.HideToolTip();
        guiLayer.EnableAttackButtons(false);
        queuedAttack = null;
        Debug.Log(attack.abilityName);
        List<string> attackOutputs = attack.UseAbility(caster, target);
        yield return StartCoroutine(AnimateAttack(attack, attackOutputs, caster, target));
        state = BattleState.BETWEEN;
    }

    public IEnumerator AnimateAttack(Ability attack, List<string> attackOutputs, Unit caster, Unit target) 
    {
        yield return guiLayer.UnitAttackAnimation(caster);
        yield return new WaitForSeconds(.4f);
        if (attack.requiresTarget)
        {
            yield return guiLayer.UnitDamageAnimation(target);
        }
        yield return guiLayer.UpdateUIFromAbility(attackOutputs);

        if (target != null && target.IsDead())
        {
            yield return guiLayer.UnitDeathAnimation(target);
        }  
    }

    private void EndBattle()
    {
        UpdateStatBlocks();
        if (state == BattleState.WIN)
        {
            StartCoroutine(PlayerWin());
        }
        if (state == BattleState.LOSE)
        {
            StartCoroutine(PlayerLose());
        }
    }

    private IEnumerator PlayerWin()
    {
        yield return new WaitForSeconds(1f);
        round++;
        guiLayer.UpdateGameDescriptionWithoutTyping($"Now for round {round}.");
        StartNewRound();
    }

    private IEnumerator PlayerLose()
    {
        yield return new WaitForSeconds(1f);
        guiLayer.UpdateGameDescriptionWithoutTyping($"You made it to round {round}.");
    }

    public void UpdateStatBlocks()
    {
        guiLayer.UpdateUI();
    }
}
