using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenGUI : MonoBehaviour
{
    public StartScreenLogic startScreen;

    public AllPossibleUnits aus;
    public List<Button> characterButtons;
    public List<Unit> units;
    public Dictionary<Button, Unit> buttonToUnit = new Dictionary<Button, Unit>();

    public Text levelText;
    public Text statDescriptions;
    public Text unitStats;

    public void Start()
    {
        
    }

    public void Initialize()
    {
        //startScreen.Setup();
        int counter = 0;
        for(int ii = 0; ii < characterButtons.Count; ii++)
        {
            //Unit unit = button.gameObject.AddComponent(typeof(Unit)) as Unit;
            //unit.Initialize(aus.allPlayerUnits, 5);
            Unit unit = startScreen.allUnitChoices[ii];
            Button button = characterButtons[ii];
            button.gameObject.SetActive(true);
            buttonToUnit[button] = unit;
            Image img = button.GetComponent<Image>();
            Color tempColor = img.color;
            tempColor.a = 1f;
            img.color = tempColor;
            img.sprite = unit.baseUnit.image;
            counter++;
        }
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        levelText.text = $"Current level:{startScreen.globalLevel}\nCurrent money:{startScreen.cash}";
    }

    public void AddToParty(Button button)
    {
        button.gameObject.SetActive(false);
        startScreen.AddUnitToParty(buttonToUnit[button]);
    }

    public void ToggleUnitStatsOn(Button button)
    {
        statDescriptions.gameObject.SetActive(false);
        Unit unit = buttonToUnit[button];
        unitStats.text = unit.GetElaborateText();
        unitStats.gameObject.SetActive(true);
    }

    public void ToggleUnitStatsOff()
    {
        unitStats.gameObject.SetActive(false);
        statDescriptions.gameObject.SetActive(true);
    }

    public void CatchLevelUpEvent()
    {
        startScreen.globalLevel = startScreen.playerController.level;
        startScreen.cash = startScreen.playerController.cash;
        UpdateLevelText();
    }
}
