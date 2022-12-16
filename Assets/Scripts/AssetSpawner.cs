using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _ToSpawn;
    [SerializeField]
    bool _spawnAtStart;
    bool _HasSpawned;


    private void Awake()
    {
        GameMaster.OnRestartGameScene += ResetGameScene;
        if (_spawnAtStart) { Spawn(); }
    }

    private void OnDisable()
    {
        GameMaster.OnRestartGameScene -= ResetGameScene;
    }

    private void Spawn()
    {
        if(_HasSpawned) { return; }
        _HasSpawned = true;
        Instantiate(_ToSpawn, transform.position, transform.rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        Spawn();
    }

    private void ResetGameScene()
    {
        _HasSpawned = false;
        if(_spawnAtStart) { Spawn(); }
    }

}
