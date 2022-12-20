using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShatterHitBox : HitBox
{
    [SerializeField]
    ParticleSystem _earthEffect;
    protected override void OnAwake()
    {
        base.OnAwake();
        GameMaster.OnUpdateGlobalHitboxes += OnUpdate;
        StartCoroutine(changeValueOverTime());
        StartCoroutine(waitForEndofPartical());
    }
    private void OnDestroy()
    {
        GameMaster.OnUpdateGlobalHitboxes -= OnUpdate;
    }
    IEnumerator changeValueOverTime()
    {
        float counter = 0f;

        while (counter < 1f)
        {
            counter += Time.deltaTime;

            float val = Mathf.Lerp(0.2673199f, 5.963368f, counter / 1f);
            float val2 = Mathf.Lerp(1.53464f, 12.92674f, counter / 1f);
            _collider.center = new Vector3(0.6180193f, 0, val);
            _collider.size = new Vector3(5.068484f, 1, val2);
            CalculateScale();
            yield return null;
        }

        float counter2 = 0f;

        while (counter2 < 0.8f)
        {
            counter2 += Time.deltaTime;
            yield return null;
        }

        float counter3 = 0f;

        while (counter3 < 1.11f)
        {
            counter3 += Time.deltaTime;

            float val = Mathf.Lerp(5.963368f, 11.95312f, counter3 / 1.11f);
            float val2 = Mathf.Lerp(12.92674f, 0.9472284f, counter3 / 1.11f);
            _collider.center = new Vector3(0.6180193f, 0, val);
            _collider.size = new Vector3(5.068484f, 1, val2);
            CalculateScale();
            yield return null;
        }
        _HitboxEnabled = false;
    }
    IEnumerator waitForEndofPartical()
    {
        yield return new WaitWhile(() => _earthEffect != null);
        DestroySelf();
    }
    void DestroySelf()
    {
        GameMaster.OnUpdateGlobalHitboxes -= OnUpdate;
        Destroy(this.gameObject);
    }
}
