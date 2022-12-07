using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateFactory: StateFactory
{
    public AiStateFactory(AiStateMachine currentContext)
    {
        _states["idle"] = new AiIdleState(currentContext);
        _states["Wander"] = new AiWanderState(currentContext);
        _states["Patrol"] = new AiPatrolState(currentContext);
        _states["Follow"] = new AiFollowState(currentContext);
        _states["Attack"] = new AiAttackingState(currentContext);
        _states["Default"] = new AIDefaultState(currentContext);
        _states["Combat"] = new AiCombatState(currentContext);
        _states["Spawning"] = new AiSpawningState(currentContext);
        _states["Dying"] = new AiDyingState(currentContext);
    }


    public BaseState Idle()
    {
        return _states["idle"];
    }


    public BaseState Wander()
    {
        return _states["Wander"];
    }

    public BaseState Patrol()
    {
        return _states["Patrol"];
    }

    public BaseState Follow()
    {
        return _states["Follow"];
    }

    public BaseState Default()
    {
        return _states["Default"];
    }

    public BaseState Combat()
    {
        return _states["Combat"];
    }
    public BaseState Spawning()
    {
        return _states["Spawning"];
    }
    public BaseState Dying()
    {
        return _states["Dying"];
    }
    public BaseState Attack()
    {
        return _states["Attack"];
    }
}
