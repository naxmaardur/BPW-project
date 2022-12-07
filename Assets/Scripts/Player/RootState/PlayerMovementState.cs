using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : BaseState
{
    PlayerStateMachine _context;

    Vector3 _rootMotion;
    public PlayerMovementState(PlayerStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }

    public override bool CheckSwitchStates()
    {
        if(_context.ControlerContext.IsShouldCastSet && GetSubState != _context.States.Dodge())
        {
            SwitchState(_context.States.Casting());
            return true;
        }
        if (_context.ControlerContext.IsShouldAttackSet && GetSubState != _context.States.Dodge())
        {
            SwitchState(_context.States.Attacking());
            return true;
        }
        if(_context.ControlerContext.Health <= 0)
        {
            SwitchState(_context.States.Dying());
            return true;
        }

        return false;
    }

    public override void EnterState()
    {
        _rootMotion = Vector3.zero;
        InitializeSubState();
    }

    public override void InitializeSubState()
    {
        if (!_context.ControlerContext.IsMovementPressed)
        {
            SetSubState(_context.States.Idle());
            _context.States.Idle().EnterState();
        }
        else if (_context.ControlerContext.IsRunPressed)
        {
            SetSubState(_context.States.Run());
            _context.States.Run().EnterState();
        }
        else if (_context.ControlerContext.IsShouldSneakSet)
        {
            SetSubState(_context.States.Sneak());
            _context.States.Sneak().EnterState();
        }
        else
        {
            SetSubState(_context.States.Walk());
            _context.States.Walk().EnterState();
        }
    }

    protected override void ExitState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void FixedUpdateState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        Vector3 rootMotion = _rootMotion;
        Vector3 deltaPos = _context.ControlerContext.playerAnimator.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        if (GetSubState != _context.States.Idle())
        {
            HandleRotation();
        }
        _context.ControlerContext.Move(_rootMotion);
        _rootMotion = Vector3.zero;

    }

    public void HandleRotation()
    {
        float movementfloat = Mathf.Clamp01(Mathf.Abs(_context.ControlerContext.GetCurrentMovement.x) + Mathf.Abs(_context.ControlerContext.GetCurrentMovement.y));
        float turningMod = 0.2f;
        if(GetSubState == _context.States.Run()) { turningMod = 0.5f; }
        float turnSpeed = _context.ControlerContext.TurningSpeed + turningMod * movementfloat;

        float targetAngle = Mathf.Atan2(_context.ControlerContext.GetCurrentMovement.x, _context.ControlerContext.GetCurrentMovement.y) * Mathf.Rad2Deg + _context.ControlerContext.GetCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(_context.ControlerContext.transform.eulerAngles.y, targetAngle, ref _context.ControlerContext.turnSmoothVelocity, turnSpeed);
        _context.ControlerContext.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

}
