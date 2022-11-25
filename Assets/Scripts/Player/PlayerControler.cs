using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControler : MonoBehaviour
{
    PlayerStateMachine _PlayerStateMachine;
    CharacterController _characterController;
    PlayerAnimatorManager _playerAnimator;
    [SerializeField]
    RunTimeAnimatorListContainer _animatorListContainer = new();

    PlayerInput _input;
    Vector2 _currentMovement;
    Vector2 _currentLookInput;
    bool _runPressed;
    [SerializeField]
    bool _movementPressed;
    bool _shouldSneak;
    bool _attackPressed;
    bool _shouldAttack;
    bool _lastAttackCompleted = true;
    bool _canAttack = true;
    [SerializeField]
    bool _shouldDodge;
    float _turningSpeed = 0.05f;
    public float turnSmoothVelocity = 0;
    Camera _camera;


    public PlayerAnimatorManager playerAnimator { get { return _playerAnimator; } }
    public bool IsMovementPressed { get { return _movementPressed; } }
    public bool IsRunPressed { get { return _runPressed; } }
    public bool IsShouldSneakSet { get { return _shouldSneak; } }
    public bool IsShouldDodgeSet { get { return _shouldDodge; } }
    public void ResetShouldDodge() { _shouldDodge = false; }
    public void ResetCanAttack() { _canAttack = false; }
    public void ResetShouldAttack() { _shouldAttack = false; _lastAttackCompleted = true; }
    public Vector2 GetCurrentMovement { get { return _currentMovement; } }
    public Vector2 GetCurrentLookInput { get { return _currentLookInput; } }
    public float TurningSpeed { get { return _turningSpeed; } }
    public Transform GetCam { get { return _camera.transform; } }

    public bool IsShouldAttackSet { get { return _shouldAttack; } }


    private void Awake()
    {
        _PlayerStateMachine = new PlayerStateMachine(this);
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
        StartCoroutine(playerAnimator.Updatefloats());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackPressed && !_shouldAttack && _canAttack && _lastAttackCompleted) { _shouldAttack = true; _canAttack = false; }
        _PlayerStateMachine.OnUpdate();
        _PlayerStateMachine.OnCheckSwitchStates();
    }

    private void OnAnimatorMove()
    {
        _PlayerStateMachine.OnAnimatorMoveState();
    }

    public void Move(Vector3 velocity)
    {
        _characterController.Move(velocity);
    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }
}
