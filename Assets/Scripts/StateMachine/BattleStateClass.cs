using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleStateClass
{
    protected BattleSystem BattleSystem;

    public BattleStateClass(BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }


    public virtual IEnumerator Attack()
    {
        yield break;
    }
}
