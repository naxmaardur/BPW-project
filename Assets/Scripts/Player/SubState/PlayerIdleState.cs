using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;
    public PlayerIdleState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {

        if (_context.ControlerContext.IsShouldDodgeSet)
        {
            if (GetSuperState != _context.States.Casting())
            {
                SwitchState(_context.States.Dodge());
                return true;
            }
            else
            {
                _context.ControlerContext.ResetShouldDodge();
            }
        }





        if (_context.ControlerContext.IsShouldSneakSet)
        {
            SwitchState(_context.States.Sneak());
            return true;
        }
        if (_context.ControlerContext.IsMovementPressed)
        {
            if (_context.ControlerContext.IsRunPressed)
                SwitchState(_context.States.Run());
            else
                SwitchState(_context.States.Walk());
            return true;
        }
        return false;
    }

    public override void EnterState()
    {
      //  throw new System.NotImplementedException();
    }

    public override void InitializeSubState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
      //  throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
    }
}
