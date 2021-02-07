using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitsWindow : EditorWindow
{
    int nameWidth = 75;
    int statWidth = 60;
    List<UnitBase> units;
    List<List<string>> table;

    List<string> indexing;
    readonly string UNITNAME = "UnitName";
    readonly string TOTALSTATS = "TotalStats";
    readonly string RANDOMSTATS = "RandomStats";

    [MenuItem("Window/Units")]
    public static void ShowWindow()
    {
        GetWindow<UnitsWindow>("Units");
    }

    private void OnEnable()
    {
        string[] guids1 = AssetDatabase.FindAssets("t:UnitBase");

        units = new List<UnitBase>();
        table = new List<List<string>>();
        indexing = new List<string>
        {
            UNITNAME,
            TOTALSTATS
        };
        foreach (StatsEnum stat in System.Enum.GetValues(typeof(StatsEnum)))
        {
            indexing.Add(stat.ToString());
        }
        indexing.Add(RANDOMSTATS);


        foreach (string guid1 in guids1)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid1);
            UnitBase unit = AssetDatabase.LoadAssetAtPath<UnitBase>(path);
            units.Add(unit);

            List<string> tableRow = new List<string>();
            foreach (string value in indexing)
            {
                tableRow.Add(value);
            }
            table.Add(tableRow);
            //tableRow.Add(unit.nameOfUnit);
            //tableRow.Add(unit.GetTotalStats().ToString());
            //foreach (int stat in unit.stats)
            //{
            //    tableRow.Add(stat.ToString());
            //}
            //tableRow.Add(unit.randomStats.ToString());
            //table.Add(tableRow);
        }
        UpdateInternalTable();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Name", GUILayout.Width(nameWidth)))
        {
            units = units.OrderBy(o => o.nameOfUnit).ToList();
            UpdateInternalTable();
        }
        if (GUILayout.Button("Total", GUILayout.Width(statWidth)))
        {
            units = units.OrderBy(o => o.GetTotalStats()).ToList();
            UpdateInternalTable();
        }
        foreach (StatsEnum stat in System.Enum.GetValues(typeof(StatsEnum)))
        {
            if (GUILayout.Button(stat.ToString(), GUILayout.Width(statWidth)))
            {
                units = units.OrderBy(o => o.stats[(int)stat]).ToList();
                UpdateInternalTable();
            }
        }
        if (GUILayout.Button("Random", GUILayout.Width(statWidth)))
        {
            units = units.OrderBy(o => o.randomStats).ToList();
            UpdateInternalTable();
        }
        GUILayout.EndHorizontal();
              
        for (int ii = 0; ii < units.Count; ii++)
        {
            List<string> tableRow = table[ii];
            GUILayout.BeginHorizontal();
            GUILayout.Label(tableRow[indexing.IndexOf(UNITNAME)], GUILayout.Width(nameWidth));
            GUILayout.Label("   " + tableRow[indexing.IndexOf(TOTALSTATS)], GUILayout.Width(statWidth));
            foreach (StatsEnum stat in System.Enum.GetValues(typeof(StatsEnum)))
            {
                GUILayout.Label("   " + tableRow[indexing.IndexOf(stat.ToString())], GUILayout.Width(statWidth));
            }
            GUILayout.Label("   " + tableRow[indexing.IndexOf(RANDOMSTATS)], GUILayout.Width(statWidth));
            GUILayout.EndHorizontal();
        }
    }

    private void UpdateInternalTable()
    {
        for (int ii = 0; ii < units.Count; ii++)
        {
            UnitBase unit = units[ii];
            List<string> tableRow = table[ii];
            tableRow[indexing.IndexOf(UNITNAME)] = unit.nameOfUnit;
            tableRow[indexing.IndexOf(TOTALSTATS)] = unit.GetTotalStats().ToString();
            foreach (StatsEnum stat in System.Enum.GetValues(typeof(StatsEnum)))
            {
                tableRow[indexing.IndexOf(stat.ToString())] = unit.stats[(int)stat].ToString();
            }
            
            //for (int jj = 0; jj < unit.stats.Length; jj++)
            //{
            //    tableRow[jj + 2] = unit.stats[jj].ToString();
            //}
            tableRow[indexing.IndexOf(RANDOMSTATS)] = unit.randomStats.ToString(); 


            //GUILayout.BeginHorizontal();
            //GUILayout.Label(unit.nameOfUnit, GUILayout.Width(nameWidth));
            //int total = 0;
            //foreach (int stat in unit.stats)
            //{
            //    GUILayout.Label(stat.ToString(), GUILayout.Width(statWidth));
            //    total += stat;
            //}
            //GUILayout.Label(unit.randomStats.ToString(), GUILayout.Width(statWidth));
            //total += unit.randomStats;
            //GUILayout.Label(total.ToString(), GUILayout.Width(statWidth));
            //GUILayout.EndHorizontal();
        }
    }

}
