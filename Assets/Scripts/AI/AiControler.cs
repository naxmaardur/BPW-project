using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(NavMeshAgent)),RequireComponent(typeof(NavMeshObstacle))]

public class AiControler : MonoBehaviour, IDamageable
{
    AiStateMachine _stateMachine;
    AiAnimatorManager _animatorManager;
    NavMeshAgent _agent;
    NavMeshObstacle _navMeshObstacle;
    CharacterController _characterController;
    public PatrolPath patrolPath;
    public bool patrol;


    bool _hasAttackToken;
    float _turningSpeed = 1;
    public float turnSmoothVelocity = 0;
    float _idleTime = 2;

    public bool HasAttackToken { get { return _hasAttackToken; } }
    public float TurningSpeed {  get { return _turningSpeed; } }
    public float IdleTime { get { return _idleTime; } }

    public AiAnimatorManager AnimatorManager { get { return _animatorManager; } }

    float _lastHitTime;

    [SerializeField]
    float _health = 100;
    [SerializeField]
    float _maxHealth = 100;

    [SerializeField]
    float _poise = 100;
    [SerializeField]
    float _maxPoise = 100;


    public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0, _maxHealth); } }
    public float MaxHealth { get { return _maxHealth; } }

    public float Poise { get { return _poise; } set { _poise = Mathf.Clamp(value, 0, _maxPoise); } }

    public void Update()
    {
        //deleteme
        OnUpdate();
    }

    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _agent.enabled = false;
        _stateMachine = new AiStateMachine(this);
        _animatorManager = new AiAnimatorManager(GetComponent<Animator>());
        _characterController = GetComponent<CharacterController>();
    }

    public void OnUpdate()
    {
        _stateMachine.OnUpdate();
        _stateMachine.OnCheckSwitchStates();
        if (_poise != _maxPoise && _lastHitTime + 6 < Time.time){ _poise = _maxPoise; }
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
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(position, out myNavHit, 100, -1)) { position = myNavHit.position; }
        else { return null; }
        NavMeshPath path = new NavMeshPath();
        _navMeshObstacle.enabled = false;
        _agent.enabled = true;
        _agent.CalculatePath(position, path);
        _agent.enabled = false;
        _navMeshObstacle.enabled = true;

        return path.corners;
    }

    public void Move(Vector3 velocity)
    {
        _characterController.Move(velocity);
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    private void OnGUI()
    {
        GUILayout.Label("1" + _stateMachine.CurrentState.GetType());
        if(_stateMachine.CurrentState.GetSubState != null)
        GUILayout.Label("2" + _stateMachine.CurrentState.GetSubState.GetType());
    }

    public void Damage(float damage, float poiseDamage = 0)
    {
        Health -= damage;
        Poise -= poiseDamage;
        _animatorManager.TriggerImpact();
    }

    public void AddHealth(float value)
    {
        throw new System.NotImplementedException();
    }

   
}
