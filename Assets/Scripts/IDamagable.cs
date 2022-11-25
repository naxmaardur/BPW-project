using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get;  set;}

    void Damage(float damage);

    void AddHealth(float value);
}
