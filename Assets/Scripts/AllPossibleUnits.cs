using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPossibleUnits : MonoBehaviour
{
    public static List<UnitBase> allPlayerUnits;
    //public List<UnitBase> allEnemyUnits;
    public List<UnitBase> allUnitsSetter;

    public void Awake()
    {
        allPlayerUnits = allUnitsSetter;
    }

    public static UnitBase GetOneUnit()
    {
        int k = Random.Range(0, allPlayerUnits.Count);
        return allPlayerUnits[k];
    }

    public static List<UnitBase> GetListOfUnits(int num)
    {
        List<UnitBase> output = new List<UnitBase>();
        for (int ii = 0; ii < num; ii++)
        {
            output.Add(GetOneUnit());
        }
        return output;
    }
}
