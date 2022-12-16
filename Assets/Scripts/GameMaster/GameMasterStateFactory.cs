using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterStateFactory : StateFactory
{

    public GameMasterStateFactory(GameMasterStateMachine currentContext)
    {
        _states["Default"] = new DefaultState(currentContext);
        _states["Game"] = new GameState(currentContext);
        _states["Menu"] = new MenuState(currentContext);
    }


    public BaseState Default()
    {
        return _states["Default"];
    }

    public BaseState Game()
    {
        return _states["Game"];
    }
    public BaseState Menu()
    {
        return _states["Menu"];
    }

}
