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

    public EquipableItemContainer EquipableItemContainer { get { return _equipableItemContainer; } }
    public PatrolPathMaster PatrolPathMaster { get { return _patrolPathMaster; } }

    PlayerControler _player;
    public PlayerControler Player { get { return _player; } }
    Camera _cam;

    public Camera Cam { get { return _cam; } }

    public delegate void GameMasterEvent();
    public static event GameMasterEvent OnUpdateGlobalHitboxes;

    public static event GameMasterEvent OnUpdateAI;
    public static event GameMasterEvent OnPause;
    public static event GameMasterEvent OnUnPause;
    public static event GameMasterEvent OnRestartGameScene;
    public static event GameMasterEvent OnCutSceneEnd;

    int _attackTokens = 5;


    private void Awake()
    {
        _input = new();
        _input.UI.Pause.started += ctx => { UnPauseGame(); };
        if (Instance != null) { Destroy(this.gameObject); return; }
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
        CutSceneStart();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Quit();
        _gameMasterStateMachine.CurrentState.EnterState();
    }


    public void PauseGame()
    {
        if (_gameMasterStateMachine.CurrentState != _gameMasterStateMachine.States.Game()) { return; }
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Menu();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Enable();
        OnPause?.Invoke();
    }

    public void UnPauseGame()
    {
        if (_gameMasterStateMachine.CurrentState != _gameMasterStateMachine.States.Menu()) { return; }
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Game();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Disable();
        OnUnPause?.Invoke();
    }

    public void RestartGameScene()
    {
        OnRestartGameScene?.Invoke();
    }


    // Update is called once per frame
    void Update()
    {
        _gameMasterStateMachine.OnUpdate();
    }


    public void CutSceneEnd()
    {
        OnCutSceneEnd?.Invoke();
    }

    public void CutSceneStart(bool EndCutScene = false)
    {
        RestartGameScene();
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.CutScene();
        CutSceneState cutSceneState = (CutSceneState)_gameMasterStateMachine.CurrentState;
        if (EndCutScene)
        {
            cutSceneState.NextState = _gameMasterStateMachine.States.Quit();
        }
        else { cutSceneState.NextState = _gameMasterStateMachine.States.Game(); }
        cutSceneState.EnterState();

    }

    public void GetNewPlayer()
    {
        _player = FindObjectOfType<PlayerControler>();
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
        if (_attackTokens <= 0) { return false; }
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
        if (_player != null && _player.IsSneaking()) { isSneaking = true; }
        return isSneaking;
    }

    public bool IsPlayerInHidingZone()
    {
        bool inHidingZone = false;
        if (_player != null && _player.InHiddingZone) { inHidingZone = true; }
        return inHidingZone;
    }

    public Transform GetPlayerTransfrom()
    {
        return _player?.transform;
    }
}
