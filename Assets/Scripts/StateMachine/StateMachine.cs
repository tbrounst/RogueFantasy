using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BattleStateClass state;

    public void SetState(BattleStateClass state)
    {
        this.state = state;
        this.state.Start();
    }

    public System.Type GetStateClass()
    {
        return state.GetType();
    }

    public void ChangeState()
    {
        state.ChangeState();
    }
}
