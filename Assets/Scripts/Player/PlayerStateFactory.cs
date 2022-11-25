using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : StateFactory
{
    new PlayerStateMachine _stateMachine;
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _stateMachine = currentContext;
        _states["idle"] = new PlayerIdleState(_stateMachine, this);
        _states["walk"] = new PlayerWalkState(_stateMachine, this);
        _states["run"] = new PlayerRunState(_stateMachine, this);
        _states["sneak"] = new PlayerSneakState(_stateMachine, this);
        _states["dodge"] = new PlayerDodgeState(_stateMachine, this);
        _states["Movement"] = new PlayerMovementState(_stateMachine, this);
        _states["Attacking"] = new PlayerAttackingState(_stateMachine, this);
        _states["Dying"] = new PlayerDyingState(_stateMachine, this);
    }


    public BaseState Idle()
    {
        return _states["idle"];
    }


    public BaseState Walk()
    {
        return _states["walk"];
    }

    public BaseState Run()
    {
        return _states["run"];
    }

    public BaseState Sneak()
    {
        return _states["sneak"];
    }

    public BaseState Movement()
    {
        return _states["Movement"];
    }

    public BaseState Attacking()
    {
        return _states["Attacking"];
    }
    public BaseState Dying()
    {
        return _states["Dying"];
    }
    public BaseState Dodge()
    {
        return _states["dodge"];
    }
}
