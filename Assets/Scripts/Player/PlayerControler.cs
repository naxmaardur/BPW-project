using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControler : MonoBehaviour, IDamageable
{
    Transform _PlayerSpawnPoint;

    PlayerStateMachine _PlayerStateMachine;
    CharacterController _characterController;
    PlayerAnimatorManager _playerAnimator;
    [SerializeField]
    RunTimeAnimatorListContainer _animatorListContainer = new();
    PlayerPickUpManager _pickUpManager;
    InteractionEventManager _interactionEventManager;

    HitBox _currentWeaponHitBox;
    [SerializeField]
    HitBox _rollHitBox;
    MagicContainer _magicContainer;

    [SerializeField]
    Transform _weaponPoint;
    [SerializeField]
    Transform _magicPoint;
    Transform _weaponTransform;
    Transform _magicTransform;
    AudioSource _source;

    PlayerInput _input;
    Vector2 _currentMovement;
    Vector2 _currentLookInput;
    bool _runPressed;
    [SerializeField]
    bool _movementPressed;
    bool _shouldSneak;
    bool _attackPressed;
    bool _shouldAttack;
    bool _CastPressed;
    bool _shouldCast;
    bool _canCast = true;
    bool _lastAttackCompleted = true;
    bool _canAttack = true;
    [SerializeField]
    bool _shouldDodge;
    float _turningSpeed = 0.05f;
    public float turnSmoothVelocity = 0;
    Camera _camera;
    bool _hasAwoken;
    [SerializeField]
    bool _inHiddingZone;

    public bool invincible = false;

    public delegate void HealthUpdate(float health);
    public HealthUpdate OnHealthUpdate;

    public delegate void SpellUpdate(MagicContainer container);
    public SpellUpdate OnSpellUpdate;


    public Transform PlayerSpawnPoint { get { return _PlayerSpawnPoint; } }
    public HitBox CurrentWeaponHitBox { get { return _currentWeaponHitBox; } }
    public HitBox CurrentRollHitBox { get { return _rollHitBox; } }
    public MagicContainer MagicContainer {get { return _magicContainer; } }
    public RunTimeAnimatorListContainer AnimatorListContainer { get { return _animatorListContainer; } }
    public PlayerAnimatorManager playerAnimator { get { return _playerAnimator; } }
    public bool IsMovementPressed { get { return _movementPressed; } }
    public bool IsRunPressed { get { return _runPressed; } }
    public bool IsShouldSneakSet { get { return _shouldSneak; } }
    public bool IsShouldDodgeSet { get { return _shouldDodge; } }
    public void ResetShouldDodge() { _shouldDodge = false; }
    public void ResetCanAttack() { _canAttack = false; }
    public void ResetShouldAttack() { _shouldAttack = false; _lastAttackCompleted = true; }
    public void ResetShouldCast() { _shouldCast = false; }
    public Vector2 GetCurrentMovement { get { return _currentMovement; } }
    public Vector2 GetCurrentLookInput { get { return _currentLookInput; } }
    public float TurningSpeed { get { return _turningSpeed; } }
    public Transform GetCam { get { return _camera.transform; } }

    public bool IsShouldAttackSet { get { return _shouldAttack; } }
    public bool IsShouldCastSet { get { return _shouldCast; } }

    public bool InHiddingZone { get { return _inHiddingZone; } set { _inHiddingZone = value; } }

    [SerializeField]
    float _health;
    float _maxHealth = 100;

        
    public float Health { get { return _health; } set { _health = value; Mathf.Clamp(_health, 0, _maxHealth); OnHealthUpdate?.Invoke(Health); } }
    public float MaxHealth { get { return _maxHealth; } }

    public float Poise { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    

    public void onAwake()
    {
        if (_hasAwoken) { return; }
        _hasAwoken = true;
        _source = GetComponent<AudioSource>();
        _interactionEventManager = new InteractionEventManager();
        //Health = _maxHealth;
        _PlayerStateMachine = new PlayerStateMachine(this);
        _pickUpManager = new PlayerPickUpManager(this);
        _playerAnimator = new PlayerAnimatorManager(GetComponent<Animator>(), _animatorListContainer);
        _characterController = GetComponent<CharacterController>();
        _camera = FindObjectOfType<Camera>();
        _input = new PlayerInput();
        _input.Player.Move.performed += ctx =>
        {
            _currentMovement = ctx.ReadValue<Vector2>();
            _movementPressed = true;
            playerAnimator.SetMovementXWithDamp(_currentMovement.x);
            playerAnimator.SetMovementYWithDamp(_currentMovement.y);

        };
        _input.Player.Move.canceled += ctx =>
        {
            _movementPressed = false;
            playerAnimator.SetMovementXWithDamp(0);
            playerAnimator.SetMovementYWithDamp(0);
        };
        _input.Player.Look.performed += ctx => { _currentLookInput = ctx.ReadValue<Vector2>(); };

        _input.Player.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _input.Player.Run.canceled += ctx => _runPressed = false;
        _input.Player.Sneak.started += ctx => { _shouldSneak = !_shouldSneak; };
        _input.Player.Dodge.started += ctx => { _shouldDodge = true; };
        _input.Player.Fire.performed += ctx => { _attackPressed = true; };
        _input.Player.Fire.canceled += ctx => { _attackPressed = false; _canAttack = true; };
        _input.Player.Interact.started += ctx => { _interactionEventManager.TriggerEvent(this.gameObject); };

        _input.Player.Cast.performed += ctx => { _CastPressed = true; };
        _input.Player.Cast.canceled += ctx => { _CastPressed = false; _canCast = true; };
        _input.Player.Pause.started += ctx => { GameMaster.Instance.PauseGame(); };
        EnableInput();
        StartCoroutine(playerAnimator.Updatefloats()); 
        ChangeWeaponTo(0);
        GameObject MCO = (GameObject)Resources.Load("Prefabs/spells/Spellcontainer", typeof(GameObject));
        MCO = Instantiate(MCO, Vector3.zero, Quaternion.identity);
        _magicTransform = MCO.transform;
        _magicContainer = MCO.GetComponent<MagicContainer>();
        _magicContainer.Owner = this.gameObject;
        GameObject castingPoint = Instantiate(new GameObject(), transform);
        castingPoint.transform.localPosition = new Vector3(0.095f, 0.5f, 0.987f);
        castingPoint.name = "CastingPoint";
        _magicContainer.CastingPosition = castingPoint.transform;
        _magicContainer.DisableContainer();
        OnSpellUpdate?.Invoke(_magicContainer);

        _PlayerSpawnPoint = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        _PlayerSpawnPoint.gameObject.name = "PLayerSpawnPoint";
        _weaponTransform.position = _weaponPoint.position;
        _weaponTransform.rotation = _weaponPoint.rotation;
        OnHealthUpdate?.Invoke(Health);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnUpdate()
    {
        if (_attackPressed && !_shouldAttack && _canAttack && _lastAttackCompleted && !_shouldCast)
        { _shouldAttack = true; _canAttack = false; }
        if (_CastPressed && _canCast && !_shouldCast && !_shouldAttack)
        { _shouldCast = true; _canCast = false; }

        _PlayerStateMachine.OnUpdate();
        _PlayerStateMachine.OnCheckSwitchStates();

        AlignEquipedObjectsWithPoints();
    }

    private void OnAnimatorMove()
    {
        _PlayerStateMachine?.OnAnimatorMoveState();
    }

    private void AlignEquipedObjectsWithPoints()
    {
        _weaponTransform.position = _weaponPoint.position;
        _weaponTransform.rotation = _weaponPoint.rotation;
        if(_magicTransform == null){ return; }
        _magicTransform.position = _magicPoint.position;
        _magicTransform.rotation = _magicPoint.rotation;
    }

    public void Move(Vector3 velocity)
    {
        velocity.y = -9  * Time.deltaTime;
        _characterController.Move(velocity);
    }

    private void OnEnable()
    {
        if (_hasAwoken) { EnableInput(); }
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void PlaySource()
    {
        _source.Play();
    }

    public void EnableInput()
    {
        _input?.Player.Enable();
    }

    public void DisableInput()
    {
        _input?.Player.Disable();
    }

    public void Damage(float damage,float poiseDamage = 0)
    {
        if (invincible) { return; }
        Health -= damage;
    }


    public bool PickUpItem(PickUpItem item)
    {
        return _pickUpManager.PickUpItem(item);
    }

    public void ChangeWeaponTo(int id)
    {
        GameObject tempNewWeaponContainer = Instantiate(GameMaster.Instance.EquipableItemContainer.GetWeaponById(id), Vector3.zero, Quaternion.identity);
        _currentWeaponHitBox = tempNewWeaponContainer.GetComponent<HitBox>();
        if (_weaponTransform != null){Destroy(_weaponTransform.gameObject);}
        _weaponTransform = tempNewWeaponContainer.transform;
        _currentWeaponHitBox.Owner = gameObject;
    } 

    public void AddHealth(float value)
    {
        Health += value;
    }


    public bool IsSneaking()
    {
        return _PlayerStateMachine.CurrentState == _PlayerStateMachine.States.Sneak();
    }

    public void SetHeight(float value, Vector3 offset)
    {
        _characterController.height = value;
        _characterController.center = offset;
    }

    /*private void OnGUI()
    {
        GUILayout.Label("1" + _PlayerStateMachine.CurrentState.GetType());
        GUILayout.Label("2" + _PlayerStateMachine.CurrentState.GetSubState.GetType());
        GUILayout.Label("_shouldAttack " + _shouldAttack);
        GUILayout.Label("_canAttack " + _canAttack);
        GUILayout.Label("_attackPressed " + _attackPressed);
        GUILayout.Label("_lastAttackCompleted " + _lastAttackCompleted);

    }*/
}