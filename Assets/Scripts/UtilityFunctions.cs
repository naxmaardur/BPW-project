using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static Vector3 Vector3Direction(Vector3 from, Vector3 to)
    {
         return (to - from).normalized;
    }
}
