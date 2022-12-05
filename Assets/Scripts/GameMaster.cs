using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    [SerializeField]
    EquipableItemContainer _equipableItemContainer;

    public EquipableItemContainer EquipableItemContainer { get { return _equipableItemContainer;} }

    PlayerControler _player;

    public delegate void UpdateHitboxes();
    public static event UpdateHitboxes OnUpdateGlobalHitboxes;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _equipableItemContainer = new();
        _player = FindObjectOfType<PlayerControler>();
        _player?.onAwake();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _player?.OnUpdate();
        OnUpdateGlobalHitboxes?.Invoke();
    }
}
