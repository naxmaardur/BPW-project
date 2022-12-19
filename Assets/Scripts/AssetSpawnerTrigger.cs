using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSpawnerTrigger : MonoBehaviour
{

    [SerializeField]
    AssetSpawner[] _assetSpawners;

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerControler>() != null)
        {
            foreach (AssetSpawner spawner in _assetSpawners)
            {
                spawner.Spawn();
            }
        }
    }

}
