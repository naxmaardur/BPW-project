using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : Singleton<GameMaster>
{
    [SerializeField]
    EquipableItemContainer _equipableItemContainer;
    PatrolPathMaster _patrolPathMaster = new();
    public EquipableItemContainer EquipableItemContainer { get { return _equipableItemContainer;} }
    public PatrolPathMaster PatrolPathMaster { get { return _patrolPathMaster; } }

    PlayerControler _player;
    Camera _cam;

    public Camera Cam {  get { return _cam; } }

    public delegate void UpdateHitboxes();
    public static event UpdateHitboxes OnUpdateGlobalHitboxes;

    public delegate void UpdateAI();
    public static event UpdateAI OnUpdateAI;

    int _attackTokens = 5;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _equipableItemContainer = new();
        SceneManager.activeSceneChanged += LocateNewReferences;

        LocateNewReferences(SceneManager.GetActiveScene(),SceneManager.GetActiveScene());

    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LocateNewReferences(Scene current, Scene next)
    {
        _player = FindObjectOfType<PlayerControler>();
        _patrolPathMaster.SetPatrolPaths(FindObjectsOfType<PatrolPath>());
        _player?.onAwake();
        _cam = FindObjectOfType<Camera>();
        
    }


    // Update is called once per frame
    void Update()
    {
        _player?.OnUpdate();
        OnUpdateGlobalHitboxes?.Invoke();
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
