using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleObjectScript : MonoBehaviour, IDamageable
{
    [SerializeField]
    float _health = 0;
    [SerializeField]
    GameObject _destroyedVersion;

    MeshRenderer _meshRenderer;
    AudioSource _audioSource;
    Collider _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }
    public float Health { get { return _health; } set { _health = value; } }
    public float Poise { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AddHealth(float value)
    {
        throw new System.NotImplementedException();
    }

    public void Damage(float damage, float poiseDamage = 0)
    {
        Health -= damage;
        if(Health <= 0)
        {
            _meshRenderer.enabled = false;
            _audioSource.Play();
            _destroyedVersion.SetActive(true);
            _collider.enabled = false;
            this.enabled = false;
        }
    }
}
