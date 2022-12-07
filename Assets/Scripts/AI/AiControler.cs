using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(NavMeshAgent))]

public class AiControler : MonoBehaviour
{
    AiStateMachine _stateMachine;
    AiAnimatorManager _animatorManager;
    NavMeshAgent _agent;
    NavMeshObstacle _navMeshObstacle;


    bool _hasAttackToken;

    public bool HasAttackToken { get { return _hasAttackToken; } }

    public AiAnimatorManager AnimatorManager { get { return _animatorManager; } }


    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _stateMachine = new AiStateMachine(this);
    }

    public void OnUpdate()
    {
        _stateMachine.OnUpdate();
    }

    private void OnAnimatorMove()
    {
        _stateMachine.OnAnimatorMoveState();
    }


    public bool RequestAttackToken()
    {
        _hasAttackToken = GameMaster.Instance.RequestAttackToken();
        return _hasAttackToken;
    }

    public void ReturnAttackToken()
    {
        _hasAttackToken = false;
        GameMaster.Instance.ReturnAttackToken();
    }

    public Vector3[] CalculatePath(Vector3 position)
    {
        NavMeshPath path = null;
        _navMeshObstacle.enabled = false;
        _agent.enabled = true;
        _agent.CalculatePath(position, path);
        _agent.enabled = false;
        _navMeshObstacle.enabled = true;

        return path.corners;
    }
}
