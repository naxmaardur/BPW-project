using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLanceHitBox : HitBox
{
    [SerializeField]
    ParticleSystem _shards;
    [SerializeField]
    ParticleSystem _mist;
    [SerializeField]
    MeshRenderer _meshRenderer;
    bool _hit;
    [SerializeField]
    float _speed = 15f;
    float _maxLifeTime = 5f;

    protected override void OnAwake()
    {
        base.OnAwake();
        GameMaster.OnUpdateGlobalHitboxes += OnUpdate;
        _maxLifeTime = _maxLifeTime + Time.time;
    }

    public override void OnUpdate()
    {
        if(_maxLifeTime <= Time.time && !_hit) { HasHitObject(); }
        if (!_hit) { transform.position += transform.forward * _speed * Time.deltaTime; }
        if (!_HitboxEnabled) { return; }

        Collider[] hits;

        hits = Physics.OverlapBox(_collider.center + transform.position, _scale, transform.rotation);
        foreach (Collider col in hits)
        {
            if (col.gameObject == _owner) continue;
            if (_HaveBeenHit.Contains(col)) continue;
            HasHitObject();
            IDamageable dam = col.gameObject.GetComponent<IDamageable>();
            if (dam != null)
            {
                OnHit(dam, col);
            }
        }
    }

    

    void HasHitObject()
    {
        _HitboxEnabled = false;
        _hit = true;
        _shards.Play();
        _mist.Play();
        _meshRenderer.enabled = false;
        StartCoroutine(waitForEndofPartical());
    }

    IEnumerator waitForEndofPartical()
    {
        yield return new WaitWhile(() => _shards != null && _mist != null);
        DestroySelf();
    }

    void DestroySelf()
    {
        GameMaster.OnUpdateGlobalHitboxes -= OnUpdate;
        Destroy(this.gameObject);
    }
}
