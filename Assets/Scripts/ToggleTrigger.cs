using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject _toToggle;

    [SerializeField]
    bool _TurnOn;

    private void OnTriggerEnter(Collider other)
    {
        _toToggle.SetActive(_TurnOn);
    }

}
