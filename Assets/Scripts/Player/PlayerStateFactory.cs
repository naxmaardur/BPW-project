using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory : StateFactory
{
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _states["idle"] = new PlayerIdleState(currentContext);
        _states["walk"] = new PlayerWalkState(currentContext);
        _states["run"] = new PlayerRunState(currentContext);
        _states["sneak"] = new PlayerSneakState(currentContext);
        _states["dodge"] = new PlayerDodgeState(currentContext);
        _states["Movement"] = new PlayerMovementState(currentContext);
        _states["Attacking"] = new PlayerAttackingState(currentContext);
        _states["Casting"] = new PlayerCastingState(currentContext);
        _states["Dying"] = new PlayerDyingState(currentContext);
        _states["Respawn"] = new PlayerRespawningState(currentContext);
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
    public BaseState Casting()
    {
        return _states["Casting"];
    }
    public BaseState Dying()
    {
        return _states["Dying"];
    }
    public BaseState Respawn()
    {
        return _states["Respawn"];
    }
    public BaseState Dodge()
    {
        return _states["dodge"];
    }
}
