using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GUILayer : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    public BattleSystem bs;

    public Text turnMarker;
    public Text gameDescription;

    public AttackToolTip att;

    public List<GameObject> unitPanels;

    public List<Text> unitStatBlocks = new List<Text>();

    public List<Button> attackButtons;
    //public GameObject attackDescriptionPanel;
    

    public void SetUp(List<Unit> combatants)
    {
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
        int unitIndex = bs.combatants.IndexOf(unit);
        GameObject button = unitPanels[unitIndex].transform.GetChild(1).gameObject;

        var sequence = DOTween.Sequence();
        if (bs.playerParty.InParty(unit))
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
        int unitIndex = bs.combatants.IndexOf(unit);
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
        int unitIndex = bs.combatants.IndexOf(unit);
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
        if (bs.state == BattleState.WIN)
        {
            Debug.Log("You won");
            gameDescription.text = "A winner is you!";
            return;
        }
        else if (bs.state == BattleState.LOSE)
        {
            gameDescription.text = "You lost";
            return;
        }

        UpdateAllStatBlocks(bs.combatants, unitStatBlocks);

        for (int ii = 0; ii < attackButtons.Count; ii++)
        {
            Button attackButton = attackButtons[ii];
            attackButton.GetComponentInChildren<Text>().text = bs.activeUnit.currentAbilities[ii].abilityName;
            attackButton.gameObject.SetActive(bs.state == BattleState.PLAYERTURN);
        }

        turnMarker.text = $"Active turn is: {bs.activeUnit.GetName()}";
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
}
