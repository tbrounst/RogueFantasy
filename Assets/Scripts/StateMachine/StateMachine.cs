using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected BattleStateClass State;

    public void SetState(BattleStateClass state)
    {
        State = state;
        StartCoroutine(State.Start());
    }
}
