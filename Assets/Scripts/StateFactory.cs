using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    protected PlayerStateMachine _context;
    protected Dictionary<string, BaseState> _states = new Dictionary<string, BaseState>();
}
