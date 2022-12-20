using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectSpell : MagicSpell
{
    GameObject CastingEffect;
    public SpawnEffectSpell(string ResourcePath, Color color)
    {
        this.color = color;
        CastingEffect = (GameObject)Resources.Load(ResourcePath, typeof(GameObject));
    }
    public override void Cast(GameObject gameObject, Transform castingLocation)
    {
        GameObject effect = Object.Instantiate(CastingEffect, castingLocation.position, castingLocation.rotation);
        HitBox hitBox = effect.GetComponent<HitBox>();
        hitBox.Owner = gameObject;
        hitBox.EnableHitBox();
    }
}
