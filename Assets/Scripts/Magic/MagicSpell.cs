using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicSpell
{
    public Color color = Color.gray;
    public abstract void Cast(GameObject gameObject, Transform castingLocation);
}
