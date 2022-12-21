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
    Camera _cam;
    PlayerControler _player;
    public GameObject RespawnCamera;
    public EquipableItemContainer EquipableItemContainer { get { return _equipableItemContainer; } }
    public PatrolPathMaster PatrolPathMaster { get { return _patrolPathMaster; } }
    public PlayerControler Player { get { return _player; } }
    public Camera Cam { get { return _cam; } }
    public delegate void GameMasterEvent();
    public delegate void GameMasterPauseEvent(bool gameover);
    public static event GameMasterEvent OnUpdateGlobalHitboxes;
    public static event GameMasterEvent OnUpdateAI;
    public static event GameMasterPauseEvent OnPause;
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
    public void PauseGame(bool gameOver = false)
    {
        if (_gameMasterStateMachine.CurrentState != _gameMasterStateMachine.States.Game()) { return; }
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Menu();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Enable();
        OnPause?.Invoke(gameOver);
    }
    public void UnPauseGame()
    {
        if (_gameMasterStateMachine.CurrentState != _gameMasterStateMachine.States.Menu()) { return; }
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.Game();
        _gameMasterStateMachine.CurrentState.EnterState();
        _input.UI.Disable();
        OnUnPause?.Invoke();
        RespawnCamera.SetActive(false);
    }
    public void RestartGameScene(bool gameOver = false)
    {
        if (gameOver)
        {
            Player?.DisableInput();
            RespawnCamera.SetActive(true);
            PauseGame(true);
        }
        OnRestartGameScene?.Invoke();
    }
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
        _gameMasterStateMachine.CurrentState = (GameMasterBaseState)_gameMasterStateMachine.States.CutScene();
        CutSceneState cutSceneState = (CutSceneState)_gameMasterStateMachine.CurrentState;
        RestartGameScene();
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
