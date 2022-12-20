using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public float Poise { get; set; }
    void Damage(float damage, float poiseDamage = 0);
    void AddHealth(float value);
}
