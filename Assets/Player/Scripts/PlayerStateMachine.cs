using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    PlayerControler _context;
    public PlayerControler ControlerContext { get { return _context; } }
    public new PlayerStateFactory States { get { return (PlayerStateFactory)_states; } }
    public PlayerStateMachine(PlayerControler ctx) : base()
    {
        _context = ctx;
        _states = new PlayerStateFactory(this);
        _currentState = States.Movement();
        _currentState.EnterState();
    }
}
