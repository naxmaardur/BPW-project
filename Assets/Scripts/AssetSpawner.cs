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
    [SerializeField]
    Transform _assetParent;
    GameObject _spawnedAsset;
    private void Awake()
    {
        GameMaster.OnRestartGameScene += ResetGameScene;
        if (_spawnAtStart) { Spawn(); }
    }
    private void OnDisable()
    {
        GameMaster.OnRestartGameScene -= ResetGameScene;
    }
    public void Spawn()
    {
        if (_HasSpawned || _spawnedAsset != null) { return; }
        _HasSpawned = true;
        _spawnedAsset = Instantiate(_ToSpawn, transform.position, transform.rotation);

        if (_assetParent != null)
        {
            _spawnedAsset.transform.SetParent(_assetParent);
        }
    }
    private void ResetGameScene()
    {
        _HasSpawned = false;
        if (_spawnedAsset != null) { Destroy(_spawnedAsset); }
        if (_spawnAtStart) { Spawn(); }
    }
}
