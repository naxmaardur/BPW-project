using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    [SerializeField]
    GameObject _healthSpriteObject;
    [SerializeField]
    GameObject _healthLingeringSpriteObject;
    [SerializeField]
    AnimationCurve _healthCurve;

    float _targethealth = 1;
    Coroutine _healthLingeringCoroutine;
    Coroutine _healthCoroutine;


    IEnumerator ScaleTransformOverTime(float a, float b, float t, Transform transform)
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
    public void ScaleHealthBasedOnValue(float health, float maxHealth)
    {
        float size = _healthCurve.Evaluate(health / maxHealth);
        _targethealth = size;
        if (_healthLingeringCoroutine != null) { StopCoroutine(_healthLingeringCoroutine); }
        if (_healthCoroutine != null) { StopCoroutine(_healthCoroutine); }
        _healthLingeringCoroutine = StartCoroutine(ScaleTransformOverTime(_healthLingeringSpriteObject.transform.localScale.x, _targethealth, 2, _healthLingeringSpriteObject.transform));
        _healthCoroutine = StartCoroutine(ScaleTransformOverTime(_healthSpriteObject.transform.localScale.x, _targethealth, 0.5f, _healthSpriteObject.transform));
    }
}
