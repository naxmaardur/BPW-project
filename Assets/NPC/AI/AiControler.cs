using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(NavMeshObstacle))]

public class AiControler : MonoBehaviour, IDamageable
{
    [SerializeField]
    AudioClip _hitSound;
    [SerializeField]
    AudioClip _deathSound;
    AudioSource _audioSource;
    AiStateMachine _stateMachine;
    AiAnimatorManager _animatorManager;
    NavMeshObstacle _navMeshObstacle;
    CharacterController _characterController;
    Transform _playerTransform;
    public PatrolPath patrolPath;
    public bool patrol;
    HitBox _hitBox;
    bool _hasAttackToken;
    [SerializeField]
    float _turningSpeed = 1;
    public float turnSmoothVelocity = 0;
    float _idleTime = 2;
    public bool HasAttackToken { get { return _hasAttackToken; } }
    public float TurningSpeed { get { return _turningSpeed; } }
    public float IdleTime { get { return _idleTime; } }
    public AiAnimatorManager AnimatorManager { get { return _animatorManager; } }
    public Transform PlayerTransfrom { get { if (_playerTransform == null) { _playerTransform = GameMaster.Instance.GetPlayerTransfrom(); } return _playerTransform; } }
    public HitBox HitBox { get { return _hitBox; } }
    float _lastHitTime;
    [SerializeField]
    GameObject _deathDropObject;
    [SerializeField]
    float _health = 100;
    [SerializeField]
    float _maxHealth = 100;
    [SerializeField]
    float _poise = 100;
    [SerializeField]
    float _maxPoise = 100;
    [SerializeField]
    float _lookDistance = 8;
    public float LookDistance { get { return _lookDistance; } }
    public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0, _maxHealth); OnHealthUpdate?.Invoke(_health, _maxHealth); } }
    public float MaxHealth { get { return _maxHealth; } }
    public float Poise { get { return _poise; } set { _poise = Mathf.Clamp(value, 0, _maxPoise); } }
    public delegate void HealthUpdate(float health, float max);
    public event HealthUpdate OnHealthUpdate;
    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _hitBox = GetComponentInChildren<HitBox>();
        if (_hitBox != null) { _hitBox.Owner = this.gameObject; }
        GetComponentInChildren<EnemyUIHandler>()?.SetContext(this);
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _stateMachine = new AiStateMachine(this);
        _animatorManager = new AiAnimatorManager(GetComponent<Animator>());
        _characterController = GetComponent<CharacterController>();
        GameMaster.OnUpdateAI += OnUpdate;
        GameMaster.OnRestartGameScene += DestroySelf;
    }
    public void OnUpdate()
    {
        _stateMachine.OnUpdate();
        _stateMachine.OnCheckSwitchStates();
        if (_poise != _maxPoise && _lastHitTime + 6 < Time.time) { _poise = _maxPoise; }
    }
    private void OnAnimatorMove()
    {
        _stateMachine.OnAnimatorMoveState();
    }
    public bool RequestAttackToken()
    {
        if (_hitBox == null) { return false; }
        if (Time.time - _lastHitTime < 1) { return false; }
        _hasAttackToken = GameMaster.Instance.RequestAttackToken();
        return _hasAttackToken;
    }
    public void DisableCollision()
    {
        _characterController.enabled = false;
    }
    public void ReturnAttackToken()
    {
        _hasAttackToken = false;
        GameMaster.Instance.ReturnAttackToken();
    }
    public Vector3[] CalculatePath(Vector3 position, float maxDistance = 3)
    {
        _navMeshObstacle.enabled = false;
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(position, out myNavHit, maxDistance, -1)) { position = myNavHit.position; }
        else { _navMeshObstacle.enabled = true; return null; }
        NavMeshPath path = new NavMeshPath();
        if (!NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, path)) { _navMeshObstacle.enabled = true; return null; }
        _navMeshObstacle.enabled = true;

        return path.corners;
    }
    public void Move(Vector3 velocity)
    {
        velocity.y = -9 * Time.deltaTime;
        _characterController.Move(velocity);
    }
    public void OnDisable()
    {
        GameMaster.OnUpdateAI -= OnUpdate;
        GameMaster.OnRestartGameScene -= DestroySelf;
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    public void Damage(float damage, float poiseDamage = 0)
    {
        Health -= _stateMachine.CurrentState == _stateMachine.States.Combat() ? damage : damage * 2.5f;
        Poise -= poiseDamage;
        _lastHitTime = Time.time;
        _animatorManager.TriggerImpact();
        if (damage > 0)
        {
            if (Health <= 0)
            {
                _audioSource.clip = _deathSound;
                _audioSource.pitch = 1;
                _audioSource.Play();
            }
            else
            {
                _audioSource.clip = _hitSound;
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.Play();
            }
        }
    }
    public void AddHealth(float value)
    {
        Health += value;
    }
    public void SpawnDeathDropObject()
    {
        Instantiate(_deathDropObject, transform.position, Quaternion.identity);
    }
}
