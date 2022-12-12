using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    AiControler _context;
    HealthBar _healthBar;
    float _lastHealthUpdate;
    CanvasGroup _canvasGroup;
    Coroutine _fadeOut;

    public void SetContext(AiControler context)
    {
        _context = context;
    }




    private void Awake()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    //TODO: link to event
    public void OnHealthUpdate(float health, float max)
    {
        _healthBar.ScaleHealthBasedOnValue(health, max);
        _lastHealthUpdate = Time.time;
        if(_fadeOut != null)
        {
            StopCoroutine(_fadeOut);
            _fadeOut = null;
        }
        _canvasGroup.alpha = 1;
    }


    private void Update()
    {
        //transform.LookAt(gameObject);
        if(_fadeOut == null && _lastHealthUpdate < Time.time - 6)
        {
            _fadeOut = StartCoroutine(FadeOutUI());
        }
    }


    IEnumerator FadeOutUI()
    {
        float alphaMinus = 0.25f;
        while (_canvasGroup.alpha != 0)
        {
            yield return new WaitForEndOfFrame();
            _canvasGroup.alpha -= alphaMinus * Time.deltaTime;
        }
    }



}
