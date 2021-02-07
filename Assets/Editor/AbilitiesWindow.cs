using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AbilitiesWindow : EditorWindow
{
    readonly int nameWidth = 150;
    readonly int typeWidth = 100;
    readonly int damageWidth = 75;
    List<Ability> abilities;
    List<List<string>> table;

    List<string> indexing;
    readonly string ABILITYNAME = "AbilityName";
    readonly string ABILITYTYPE = "AbilityType";
    readonly string ABILITYDAMAGE = "AbilityDamage";
    readonly string ABILITYDESCRIPTION = "AbilityDescription";


    [MenuItem("Window/Abilties")]
    public static void ShowWindow() 
    {
        GetWindow<AbilitiesWindow>("Abilities");
    }

    public void OnEnable()
    {
        string[] guids1 = AssetDatabase.FindAssets("t:Ability");
        
        abilities = new List<Ability>();
        table = new List<List<string>>();
        indexing = new List<string>
        {
            ABILITYNAME,
            ABILITYTYPE,
            ABILITYDAMAGE,
            ABILITYDESCRIPTION
        };

        foreach (string guid1 in guids1)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid1);
            Ability ability = AssetDatabase.LoadAssetAtPath<Ability>(path);
            ability.SetValues();
            abilities.Add(ability);

            List<string> tableRow = new List<string>();
            foreach (string value in indexing)
            {
                tableRow.Add(value);
            }
            table.Add(tableRow);
        }
        UpdateInternalTable();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Name", GUILayout.Width(nameWidth)))
        {
            abilities = abilities.OrderBy(o => o.abilityName).ToList();
            UpdateInternalTable();
        }
        if (GUILayout.Button("Type", GUILayout.Width(typeWidth)))
        {
            abilities = abilities.OrderBy(o => o.type.ToString()).ThenBy(o => o.totalDamage).ToList();
            UpdateInternalTable();
        }
        if (GUILayout.Button("Damage", GUILayout.Width(damageWidth)))
        {
            abilities = abilities.OrderBy(o => o.totalDamage).ToList();
            UpdateInternalTable();
        }
        GUILayout.EndHorizontal();

        //string[] guids1 = AssetDatabase.FindAssets("t:Ability");
        //foreach( string guid1 in guids1)
        for (int ii = 0; ii < abilities.Count; ii++)
        {
            List<string> tableRow = table[ii];
            GUILayout.BeginHorizontal();
            GUILayout.Label(tableRow[indexing.IndexOf(ABILITYNAME)], GUILayout.Width(nameWidth));
            GUILayout.Label(tableRow[indexing.IndexOf(ABILITYTYPE)], GUILayout.Width(typeWidth));
            GUILayout.Label(tableRow[indexing.IndexOf(ABILITYDAMAGE)], GUILayout.Width(damageWidth));
            GUILayout.Label(tableRow[indexing.IndexOf(ABILITYDESCRIPTION)]);
            GUILayout.EndHorizontal();

            //string path = AssetDatabase.GUIDToAssetPath(guid1);
            //Ability ability = AssetDatabase.LoadAssetAtPath<Ability>(path);
            //ability.SetValues();
            //GUILayout.BeginHorizontal();
            //GUILayout.Label(ability.abilityName, GUILayout.Width(nameWindow));
            ////GUILayout.Label(ability.GenerateToolTipText());
            //GUILayout.Label(ability.type.ToString(), GUILayout.Width(typeWindow));
            //GUILayout.Label(ability.totalDamage.ToString(), GUILayout.Width(damageWindow));
            //GUILayout.Label(ability.description);
            //GUILayout.EndHorizontal();
        }
    }

    private void UpdateInternalTable()
    {
        for (int ii = 0; ii < abilities.Count; ii++)
        {
            Ability ability = abilities[ii];
            List<string> tableRow = table[ii];
            tableRow[indexing.IndexOf(ABILITYNAME)] = ability.abilityName;
            tableRow[indexing.IndexOf(ABILITYTYPE)] = ability.type.ToString();
            tableRow[indexing.IndexOf(ABILITYDAMAGE)] = ability.totalDamage.ToString();
            tableRow[indexing.IndexOf(ABILITYDESCRIPTION)] = ability.description;
        }
    }

}
