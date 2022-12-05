using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerControler _player;
    HealthBar _healthBar;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerControler>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    private void OnEnable()
    {
        _player.OnHealthUpdate += OnPlayerHealthUpdated;
        _player.OnSpellUpdate += OnSpellUpdated;
    }

    private void OnDisable()
    {
        _player.OnHealthUpdate -= OnPlayerHealthUpdated;
        _player.OnSpellUpdate -= OnSpellUpdated;
    }
   
    

    public void OnPlayerHealthUpdated(float health)
    {
        _healthBar.ScaleHealthBasedOnValue(health,_player.MaxHealth);
    }


    public void OnSpellUpdated(MagicContainer magicContainer)
    {

    }
}
