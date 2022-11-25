using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.IsShouldDodgeSet)
        {
            SwitchState(_states.Dodge());
            return true;
        }
        if (_context.IsShouldSneakSet)
        {
            SwitchState(_states.Sneak());
            return true;
        }
        if (!_context.IsMovementPressed)
        {
            SwitchState(_states.Idel());
            return true;
        }
        if (_context.IsRunPressed)
        {
            SwitchState(_states.Run());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
       // throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
      //  throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
        CheckSwitchStates();
    }
}
