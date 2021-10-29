using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPossibleUnits : MonoBehaviour
{
    public List<UnitBase> allPlayerUnits;
    //public List<UnitBase> allEnemyUnits;

    public UnitBase GetOneUnit()
    {
        int k = Random.Range(0, allPlayerUnits.Count);
        return allPlayerUnits[k];
    }

    public List<UnitBase> GetListOfUnits(int num)
    {
        List<UnitBase> output = new List<UnitBase>();
        for (int ii = 0; ii < num; ii++)
        {
            output.Add(GetOneUnit());
        }
        return output;
    }
}
