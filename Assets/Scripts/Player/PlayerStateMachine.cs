using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{

    new PlayerStateFactory _states;
    //public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    PlayerControler _context;
    public PlayerControler ControlerContext { get { return _context; } }


    public PlayerStateMachine(PlayerControler ctx) : base()
    {
        _context = ctx;
        _states = new PlayerStateFactory(this);
        _currentState = _states.Movement();
        _currentState.EnterState();
    }

    public override void OnCheckSwitchStates()
    {
        _currentState.CheckSwitchStates();
    }
   

    public override void OnFixedUpdate()
    {
        _currentState.FixedUpdateStates();
    }

    public override void OnUpdate()
    {
        _currentState.UpdateStates();
    }

    public override void OnAnimatorMoveState()
    {
        _currentState.OnAnimatorMoveStates();
    }
}
