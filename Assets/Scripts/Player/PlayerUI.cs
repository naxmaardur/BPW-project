using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerControler _player;
    [SerializeField]
    GameObject _healthSprite;
    [SerializeField]
    AnimationCurve _healthCurve;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerControler>();
        ScaleHealthBasedOnValue(80);
    }

    private void OnEnable()
    {
        PlayerControler.OnHealthUpdate += OnPlayerHealthUpdated;
        PlayerControler.OnSpellUpdate += OnSpellUpdated;
    }

    private void OnDisable()
    {
        PlayerControler.OnHealthUpdate -= OnPlayerHealthUpdated;
        PlayerControler.OnSpellUpdate -= OnSpellUpdated;
    }

    // The curve makes it so that the health displayed is lower then then actual health you have, making the last 25% of the bar the last 50% of your health.
    // this is to increase tension 
    void ScaleHealthBasedOnValue(float health)
    {
        float size = _healthCurve.Evaluate(health / _player.MaxHealth);
        _healthSprite.transform.localScale = new Vector3(size, _healthSprite.transform.localScale.y, _healthSprite.transform.localScale.z);
    }

    public void OnPlayerHealthUpdated(float health)
    {
        ScaleHealthBasedOnValue(health);
    }


    public void OnSpellUpdated(MagicContainer magicContainer)
    {

    }
}
