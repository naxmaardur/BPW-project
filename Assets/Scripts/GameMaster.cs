using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : Singleton<GameMaster>
{
    GameMasterStateMachine _gameMasterStateMachine;
    [SerializeField]
    EquipableItemContainer _equipableItemContainer;
    PatrolPathMaster _patrolPathMaster = new();
    PlayerInput _input;

    public EquipableItemContainer EquipableItemContainer { get { return _equipableItemContainer;} }
    public PatrolPathMaster PatrolPathMaster { get { return _patrolPathMaster; } }

    PlayerControler _player;
    public PlayerControler Player { get { return _player; } }
    Camera _cam;

    public Camera Cam {  get { return _cam; } }

    public delegate void UpdateHitboxes();
    public static event UpdateHitboxes OnUpdateGlobalHitboxes;

    public delegate void UpdateAI();
    public static event UpdateAI OnUpdateAI;
    public delegate void Pause();
    public static event Pause OnPause;
    public static event Pause OnUnPause;

    int _attackTokens = 5;


    private void Awake()
    {
        _input = new();
        _input.UI.Pause.started += ctx => { if (_gameMasterStateMachine.CurrentState != _gameMasterStateMachine.States.Default()) { UnPauseGame(); } };
        _input.UI.Enable();
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _gameMasterStateMachine = new(this);
        _equipableItemContainer = new();
        SceneManager.sceneLoaded += LocateNewReferences;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LocateNewReferences(Scene current, LoadSceneMode mode)
    {
        _gameMasterStateMachine.CurrentState.OnChangeScene();
    }


    public void StartGame()
    {
        _input.UI.Disable();
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Game();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void PauseGame()
    {
        if(_gameMasterStateMachine.CurrentState == _gameMasterStateMachine.States.Menu())
        {
            return;
        }
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Menu();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Enable();
        OnPause?.Invoke();
    }

    public void UnPauseGame()
    {
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Game();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Disable();
        OnUnPause?.Invoke();
    }

    


    // Update is called once per frame
    void Update()
    {
        _gameMasterStateMachine.OnUpdate();
    }

    public void GetNewPlayer()
    {
        _player = FindObjectOfType<PlayerControler>();
        _player?.onAwake();
        _cam = FindObjectOfType<Camera>();
    }

    public void InvokeUpdateGlobalHitboxes()
    {
        OnUpdateGlobalHitboxes?.Invoke();
    }

    public void InvokeUpdateAI()
    {
        OnUpdateAI?.Invoke();
    }
    public bool RequestAttackToken()
    {
        if(_attackTokens <= 0) { return false; }
        _attackTokens--;
        return true;
    }

    public void ReturnAttackToken()
    {
        _attackTokens++;
    }


    public bool IsPlayerSneaking()
    {
        bool isSneaking = false;
        if(_player != null && _player.IsSneaking()) { isSneaking = true; }
        return isSneaking;
    }
    
    public bool IsPlayerInHidingZone()
    {
        bool inHidingZone = false;
        if(_player != null && _player.InHiddingZone) { inHidingZone = true; }
        return inHidingZone;
    }

    public Transform GetPlayerTransfrom()
    {
        return _player?.transform;
    }
}
