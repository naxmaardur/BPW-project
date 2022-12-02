using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerControler _player;
    [SerializeField]
    GameObject _healthSpriteObject;
    [SerializeField]
    GameObject _healthLingeringSpriteObject;
    [SerializeField]
    AnimationCurve _healthCurve;

    float _targethealth = 1;
    Coroutine _healthLingeringCoroutine;
    Coroutine _healthCoroutine;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerControler>();
        
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
   
    IEnumerator ScaleTransformOverTime(float a, float b, float t,Transform transform)
    {
        float counter = 0f;

        while (counter < t)
        {
            counter += Time.deltaTime;

            float val = Mathf.Lerp(a, b, counter / t);
            transform.localScale = new Vector3(val, transform.localScale.y, transform.localScale.z);
            yield return null;
        }
    }


    // The curve makes it so that the health displayed is lower then then actual health you have, making the last 25% of the bar the last 50% of your health.
    // this is to increase tension 
    void ScaleHealthBasedOnValue(float health)
    {
        float size = _healthCurve.Evaluate(health / _player.MaxHealth);
        _targethealth = size;
        if (_healthLingeringCoroutine != null) { StopCoroutine(_healthLingeringCoroutine); }
        if(_healthCoroutine != null) { StopCoroutine(_healthCoroutine); }
        _healthLingeringCoroutine = StartCoroutine(ScaleTransformOverTime(_healthLingeringSpriteObject.transform.localScale.x, _targethealth, 2,_healthLingeringSpriteObject.transform));
        _healthCoroutine = StartCoroutine(ScaleTransformOverTime(_healthSpriteObject.transform.localScale.x, _targethealth, 0.5f, _healthSpriteObject.transform));
    }

    public void OnPlayerHealthUpdated(float health)
    {
        ScaleHealthBasedOnValue(health);
    }


    public void OnSpellUpdated(MagicContainer magicContainer)
    {

    }
}
