using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory
{

    protected StateMachine _stateMachine;
    protected Dictionary<string, BaseState> _states = new Dictionary<string, BaseState>();
}
