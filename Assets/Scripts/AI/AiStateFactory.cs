using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateFactory: StateFactory
{
    //Root states Start with a capital letter
    public AiStateFactory(AiStateMachine currentContext)
    {
        _states["idle"] = new AiIdleState(currentContext);
        _states["wander"] = new AiWanderState(currentContext);
        _states["patrol"] = new AiPatrolState(currentContext);
        _states["follow"] = new AiFollowState(currentContext);
        _states["fighting"] = new AIFightingState(currentContext);
        _states["attack"] = new AiAttackingState(currentContext);


        _states["Default"] = new AIDefaultState(currentContext);
        _states["Combat"] = new AiCombatState(currentContext);
        _states["Spawning"] = new AiSpawningState(currentContext);
        _states["Dying"] = new AiDyingState(currentContext);
        _states["Stunded"] = new AiStundedState(currentContext);
    }


    public BaseState Idle()
    {
        return _states["idle"];
    }


    public BaseState Wander()
    {
        return _states["wander"];
    }

    public BaseState Patrol()
    {
        return _states["patrol"];
    }

    public BaseState Fighting()
    {
        return _states["fighting"];
    }

    public BaseState Follow()
    {
        return _states["follow"];
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
        return _states["attack"];
    }
    public BaseState Stunded()
    {
        return _states["Stunded"];
    }
}
