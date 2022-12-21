using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject _timeLine;
    [SerializeField]
    bool _endScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControler>() != null)
        {
            GameMaster.Instance.CutSceneStart(_endScene);
            _timeLine.SetActive(true);
        }
    }
}
