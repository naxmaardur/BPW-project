using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDefaultState : BaseState
{
    AiStateMachine _context;
    Vector3 _rootMotion;
    public AIDefaultState(AiStateMachine currentContext) : base(currentContext)
    {
        _IsRootState = true;
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if(_context.ControlerContext.Health == 0)
        {
            SwitchState(_context.States.Dying());
            return true;
        }
        if(_context.ControlerContext.Poise == 0)
        {
            SwitchState(_context.States.Stunded());
            return true;
        }
        if (CheckIfPlayerIsSeen())
        {
            SwitchState(_context.States.Combat());
            return true;
        }


        //throw new System.NotImplementedException();
        return false;
    }

    bool CheckIfPlayerIsSeen()
    {
        float CheckDistance = 8;
        float distance = Vector3.Distance(_context.ControlerContext.PlayerTransfrom.position, _context.ControlerContext.transform.position);
        if(distance > CheckDistance) { return false; }
        if (GameMaster.Instance.IsPlayerSneaking()) 
        {
            if (GameMaster.Instance.IsPlayerInHidingZone())
            {
                CheckDistance = 2;
            }
            else
            {
                CheckDistance = 4;
            }
        }
        if (distance > CheckDistance) { return false; }
        if (Vector3.Angle(_context.ControlerContext.transform.forward, _context.ControlerContext.PlayerTransfrom.position - _context.ControlerContext.transform.transform.position) > 80)
        {
            return false;
        }
        return true;
    }


    void OnHealthUpdate(float health,float max)
    {
        SwitchState(_context.States.Combat());
    }


    public override void EnterState()
    {
        _context.ControlerContext.OnHealthUpdate += OnHealthUpdate;
        InitializeSubState();
        _rootMotion = Vector3.zero;
    }

    public override void InitializeSubState()
    {
        if (_context.ControlerContext.patrol)
        {
            SetSubState(_context.States.Patrol());
            _context.States.Patrol().EnterState();
            return;
        }
        SetSubState(_context.States.Wander());
        _context.States.Wander().EnterState();
    }

    protected override void ExitState()
    {
        _context.ControlerContext.OnHealthUpdate -= OnHealthUpdate;
    }

    protected override void FixedUpdateState()
    {

    }

    protected override void OnAnimatorMoveState()
    {
        Vector3 rootMotion = _rootMotion;
        Vector3 deltaPos = _context.ControlerContext.AnimatorManager.GetDeltaPosition;
        deltaPos.y = 0;
        rootMotion += deltaPos;
        _rootMotion = rootMotion;
    }

    protected override void UpdateState()
    {
        _context.ControlerContext.Move(_rootMotion);
        _rootMotion = Vector3.zero;
    }
}
