using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineHandler : MonoBehaviour
{
    public void CutsceneEnd()
    {
        GameMaster.Instance.CutSceneEnd();
    }
}
