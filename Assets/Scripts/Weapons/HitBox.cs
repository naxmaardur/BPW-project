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
    BoxCollider _collider;
    GameObject _owner;
    [SerializeField]
    bool _HitboxEnabled;
    List<Collider> _HaveBeenHit = new();
    public GameObject Owner { get { return _owner; } set { if(_owner == null) { _owner = value; } } }
    Vector3 _scale;
    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        CalculateScale();
    }
    void CalculateScale()
    {
        _scale = new Vector3(_collider.size.x * transform.lossyScale.x,
            _collider.size.y * transform.lossyScale.y,
            _collider.size.z * transform.lossyScale.z);
    }
    public void OnUpdate()
    {
        if (!_HitboxEnabled) { return; }

        Collider[] hits;

        hits = Physics.OverlapBox(_collider.center + transform.position, _scale, transform.rotation);
        foreach(Collider col in hits)
        {
            if (col.gameObject == _owner) continue;
            if (_HaveBeenHit.Contains(col)) continue;
            IDamageable dam = col.gameObject.GetComponent<IDamageable>();
            if (dam != null)
            {
                dam.Damage(_damage);
                _HaveBeenHit.Add(col);
            }
        }
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
