using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine _context;
    Dictionary<_StatesEnum, PlayerBaseState> _states = new Dictionary<_StatesEnum, PlayerBaseState>();


    enum _StatesEnum
    {
        //Sub states
        idel,
        walk,
        run,
        sneak,
        dodge,
        //Root States
        Movement,
        Attacking,
        Dying
    }



    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
        _states[_StatesEnum.idel] = new PlayerIdleState(_context, this);
        _states[_StatesEnum.walk] = new PlayerWalkState(_context, this);
        _states[_StatesEnum.run] = new PlayerRunState(_context, this);
        _states[_StatesEnum.sneak] = new PlayerSneakState(_context, this);
        _states[_StatesEnum.dodge] = new PlayerDodgeState(_context, this);
        _states[_StatesEnum.Movement] = new PlayerMovementState(_context, this);
        _states[_StatesEnum.Attacking] = new PlayerAttackingState(_context, this);
        _states[_StatesEnum.Dying] = new PlayerDyingState(_context, this);
    }


    public PlayerBaseState Idel()
    {
        return _states[_StatesEnum.idel];
    }


    public PlayerBaseState Walk()
    {
        return _states[_StatesEnum.walk];
    }

    public PlayerBaseState Run()
    {
        return _states[_StatesEnum.run];
    }

    public PlayerBaseState Sneak()
    {
        return _states[_StatesEnum.sneak];
    }

    public PlayerBaseState Movement()
    {
        return _states[_StatesEnum.Movement];
    }

    public PlayerBaseState Attacking()
    {
        return _states[_StatesEnum.Attacking];
    }
    public PlayerBaseState Dying()
    {
        return _states[_StatesEnum.Dying];
    }
    public PlayerBaseState Dodge()
    {
        return _states[_StatesEnum.dodge];
    }
}
