using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawnCam : MonoBehaviour
{
    private void Awake()
    {
        GameMaster.Instance.RespawnCamera = gameObject;
    }
}
