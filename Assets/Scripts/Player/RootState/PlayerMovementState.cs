using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    PlayerStateMachine _context;
    PlayerStateFactory _states;

    Vector3 _rootMotion;
    public PlayerMovementState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        _IsRootState = true;
        _context = currentContext;
        _states = playerStateFactory;
    }

    public override bool CheckSwitchStates()
    {
        if (_context.IsShouldAttackSet && GetSubState != _states.Dodge())
        {
            SwitchState(_states.Attacking());
            return true;
        }

        return false;
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        _rootMotion = Vector3.zero;
        InitializeSubState();
    }

    public override void InitializeSubState()
    {
        if (!_context.IsMovementPressed)
        {
            SetSubState(_states.Idel());
            _states.Idel().EnterState();
        }
        else if (_context.IsRunPressed)
        {
            SetSubState(_states.Run());
            _states.Run().EnterState();
        }
        else if (_context.IsShouldSneakSet)
        {
            SetSubState(_states.Sneak());
            _states.Sneak().EnterState();
        }
        else
        {
            SetSubState(_states.Walk());
            _states.Walk().EnterState();
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
        Vector3 deltaPos = _context.playerAnimator.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        if (CheckSwitchStates()) {return;}
        if (GetSubState != _states.Idel())
        {
            HandleRotation();
        }
        _context.Move(_rootMotion);
        _rootMotion = Vector3.zero;

    }

    public void HandleRotation()
    {
        float movementfloat = Mathf.Clamp01(Mathf.Abs(_context.GetCurrentMovement.x) + Mathf.Abs(_context.GetCurrentMovement.y));
        float turningMod = 0.2f;
        if(GetSubState == _states.Run()) { turningMod = 0.5f; }
        float turnSpeed = _context.TurningSpeed + turningMod * movementfloat;

        float targetAngle = Mathf.Atan2(_context.GetCurrentMovement.x, _context.GetCurrentMovement.y) * Mathf.Rad2Deg + _context.GetCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(_context.transform.eulerAngles.y, targetAngle, ref _context.turnSmoothVelocity, turnSpeed);
        _context.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
