using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Pick Up Item", order = 1)]
public class PickUpItem : ScriptableObject
{

    public int TypeId;
    public int ItemId;
    public int Count;
}
