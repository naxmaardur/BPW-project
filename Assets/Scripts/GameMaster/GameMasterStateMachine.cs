using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterStateMachine : StateMachine
{
    GameMaster _context;
    public GameMaster ControlerContext { get { return _context; } }
    public new GameMasterBaseState CurrentState { get { return (GameMasterBaseState)_currentState; } set { _currentState = value; } }
    public new GameMasterStateFactory States { get { return (GameMasterStateFactory)_states; } }

    public GameMasterStateMachine(GameMaster ctx) : base()
    {
        _context = ctx;
        _states = new GameMasterStateFactory(this);
        _currentState = States.Default();
        _currentState.EnterState();
    }
}
