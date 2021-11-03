using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitalizeEnemyParty(int level)
    {
        foreach (Unit unit in unitParty.partyMembers)
        {
            unit.Initialize(level);
        }
    }
}
