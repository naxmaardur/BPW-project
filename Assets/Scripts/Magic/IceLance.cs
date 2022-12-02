using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance : MagicSpell
{
    new public Color color = Color.blue;

    GameObject CastingEffect;

    public IceLance()
    {
        CastingEffect = (GameObject)Resources.Load("Prefabs/spells/IceLanceEffect", typeof(GameObject));
    }

    public override void Cast(GameObject gameObject, Transform castingLocation)
    {
        GameObject effect = Object.Instantiate(CastingEffect, castingLocation.position, castingLocation.rotation);
        HitBox hitBox = effect.GetComponent<HitBox>();
        hitBox.Owner = gameObject;
        hitBox.EnableHitBox();
    }
}
