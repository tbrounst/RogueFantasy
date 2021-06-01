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

    public virtual void Begin()
    {
        return;
    }


    public virtual void QueueAttack(int attackNum)
    {
        return;
    }

    public virtual void Target(Unit target)
    {
        return;
    }

    public abstract void ChangeState();
}
