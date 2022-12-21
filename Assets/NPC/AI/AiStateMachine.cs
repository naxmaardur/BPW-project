using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine : StateMachine
{
    AiControler _context;
    public AiControler ControlerContext { get { return _context; } }
    public new AiStateFactory States { get { return (AiStateFactory)_states; } }
    public AiStateMachine(AiControler ctx) : base()
    {
        _context = ctx;
        _states = new AiStateFactory(this);
        _currentState = States.Spawning();
        _currentState.EnterState();
    }
}
