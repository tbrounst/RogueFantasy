using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUILayer : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    public BattleSystem battleSystem;

    public Text turnMarker;
    public Text gameDescription;
    public Text roundText;

    public AttackToolTip att;

    public List<GameObject> unitPanels;

    public List<Text> unitStatBlocks = new List<Text>();

    public List<Button> attackButtons;
    public Button resetButton;
    //public GameObject attackDescriptionPanel;
    

    public void SetUp()
    {
        List<Unit> combatants = battleSystem.combatants;
        for (int ii = 0; ii < combatants.Count; ii++)
        {
            GameObject obj = unitPanels[ii];
            GameObject stats = obj.transform.GetChild(0).gameObject;
            unitStatBlocks.Add(stats.GetComponent<Text>());
            GameObject button = obj.transform.GetChild(1).gameObject;
            button.SetActive(true);
            Image img = button.GetComponent<Image>();
            Color tempColor = img.color;
            tempColor.a = 1f;
            img.color = tempColor;
            img.sprite = combatants[ii].baseUnit.image;
        }
    }

    public void AnimateAttackListener(Ability attack, List<string> attackOutputs, Unit caster, Unit target)
    {
        StartCoroutine(AnimateAttack(attack, attackOutputs, caster, target));
    }

    private IEnumerator AnimateAttack(Ability attack, List<string> attackOutputs, Unit caster, Unit target)
    {
        HideToolTip();
        EnableAttackButtons(false);
        yield return UnitAttackAnimation(caster);
        yield return new WaitForSeconds(.4f);
        if (attack.requiresTarget)
        {
            yield return UnitDamageAnimation(target);
        }
        yield return UpdateUIFromAbility(attackOutputs);

        if (target != null && target.IsDead())
        {
            yield return UnitDeathAnimation(target);
        }
        battleSystem.ChangeState();
        //battleSystem.state = BattleState.BETWEEN;
    }

    public IEnumerator TypeDialog(string textToUse, Text textField)
    {
        textField.text = "";
        foreach (char letter in textToUse.ToCharArray())
        {
            textField.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        yield return new WaitForSeconds(.5f);
    }

    public IEnumerator UnitAttackAnimation(Unit unit)
    {
        int unitIndex = battleSystem.combatants.IndexOf(unit);
        GameObject button = unitPanels[unitIndex].transform.GetChild(1).gameObject;

        var sequence = DOTween.Sequence();
        if (battleSystem.playerParty.InParty(unit))
        {
            sequence.Append(button.transform.DOLocalMoveX(button.transform.localPosition.x + 50f, .2f));
        } else
        {
            sequence.Append(button.transform.DOLocalMoveX(button.transform.localPosition.x - 50f, .2f));
        }
        sequence.Append(button.transform.DOLocalMoveX(button.transform.localPosition.x, .2f));
        yield return null;
    }

    public IEnumerator UnitDamageAnimation(Unit unit)
    {
        int unitIndex = battleSystem.combatants.IndexOf(unit);
        GameObject button = unitPanels[unitIndex].transform.GetChild(1).gameObject;
        Image image = button.GetComponent<Image>();
        Color originalColor = image.color;

        var sequence = DOTween.Sequence();
        sequence.Append(image.DOColor(Color.gray, 0.1f));
        sequence.Append(image.DOColor(originalColor, 0.1f));
        yield return null;
    }

    public IEnumerator UnitDeathAnimation(Unit unit)
    {
        int unitIndex = battleSystem.combatants.IndexOf(unit);
        GameObject button = unitPanels[unitIndex].transform.GetChild(1).gameObject;

        yield return button.GetComponent<Image>().DOFade(0f, 0.55f).WaitForCompletion();
        button.SetActive(false);
    }

    public IEnumerator UpdateUIFromAbility(List<string> texts)
    {
        foreach (string text in texts)
        {
            yield return TypeDialog(text, gameDescription);
        }
        UpdateUI();
    }

    public void UpdateGameDescriptionWithoutTyping(string text)
    {
        gameDescription.text = text;
    }

    public void UpdateUI()
    {
        //if (battleSystem.state == BattleState.WIN)
        if (battleSystem.GetStateClass() == typeof(PlayerWinState))
        {
            Debug.Log("You won");
            gameDescription.text = "A winner is you!";
            return;
        }
        //else if (battleSystem.state == BattleState.LOSE)
        if (battleSystem.GetStateClass() == typeof(PlayerLoseState))
        {
            gameDescription.text = "You lost";
            return;
        }

        UpdateAllStatBlocks(battleSystem.combatants, unitStatBlocks);

        for (int ii = 0; ii < attackButtons.Count; ii++)
        {
            Button attackButton = attackButtons[ii];
            attackButton.GetComponentInChildren<Text>().text = battleSystem.activeUnit.currentAbilities[ii].abilityName;
            //attackButton.gameObject.SetActive(battleSystem.state == BattleState.PLAYERTURN);
            attackButton.gameObject.SetActive(battleSystem.GetStateClass() == typeof(PlayerAttackState));
        }

        turnMarker.text = $"Active turn is: {battleSystem.activeUnit.GetName()}";
    }

    private void UpdateAllStatBlocks(List<Unit> combatants, List<Text> statblock)
    {
        for (int ii = 0; ii < combatants.Count; ii++)
        {
            UpdateOneUnit(combatants[ii], statblock[ii]);
        }
    }

    private void UpdateOneUnit(Unit unit, Text text)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(unit.GetName()).Append("\n");
        foreach (StatsEnum statType in System.Enum.GetValues(typeof(StatsEnum)))
        {
            if (statType == StatsEnum.HP)
            {
                sb.Append($"{statType}: {unit.GetCurrentHP()}/{unit.GetStat(statType).ToString()}\n\n");
            } else
            {
                sb.Append($"{statType}: {unit.GetStat(statType).ToString()}\n");
            }
        }
        text.text = sb.ToString();
    }

    public void EnableAttackButtons(bool enable)
    {
        foreach (Button button in attackButtons)
        {
            button.gameObject.SetActive(enable);
        }
    }

    public void CreateAttackToolTip(string description)
    {
        att.ShowToolTip(description);
    }

    public void HideToolTip()
    {
        att.HideToolTip();
    }

    public void SetRoundText()
    {
        int round = battleSystem.GetRound();
        roundText.text = $"Round: {round}";
    }

    public void ToggleResetButton(bool toggle)
    {
        resetButton.gameObject.SetActive(toggle);
    }
}
