using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HitBox : MonoBehaviour
{
    /*
        This Hitbox script is to do collision detection with out the need of ridgidbodies which slightly saves on performance.
        it also doubles as the only scripted needed to ever deal damage to a IDamagble.
    */

    [SerializeField]
    protected float _damage = 10f;
    protected BoxCollider _collider;
    protected GameObject _owner;
    [SerializeField]
    protected bool _HitboxEnabled;
    protected List<GameObject> _HaveBeenHit = new();
    public GameObject Owner { get { return _owner; } set { if(_owner == null) { _owner = value; } } }
    protected Vector3 _scale;
    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        CalculateScale();
    }

    protected void CalculateScale()
    {
        _scale = new Vector3(_collider.size.x * transform.lossyScale.x,
            _collider.size.y * transform.lossyScale.y,
            _collider.size.z * transform.lossyScale.z);
    }
    public virtual void OnUpdate()
    {
        if (!_HitboxEnabled) { return; }

        Collider[] hits;

        hits = Physics.OverlapBox(_collider.center + transform.position, _scale, transform.rotation);
        foreach(Collider col in hits)
        {
            if (col.gameObject == _owner) continue;
            if (_HaveBeenHit.Contains(col.gameObject)) continue;
            IDamageable dam = col.gameObject.GetComponent<IDamageable>();
            if (dam != null)
            {
                OnHit(dam, col);
            }
        }
    }

    protected virtual void OnHit(IDamageable dam, Collider col)
    {
        dam.Damage(_damage);
        _HaveBeenHit.Add(col.gameObject);
    }

    public void ResetHitList()
    {
        _HaveBeenHit.Clear();
    }

    public void DisableHitBox()
    {
        ResetHitList();
        _HitboxEnabled = false;
    }
    public void EnableHitBox()
    {
        _HitboxEnabled = true;
    }
}
