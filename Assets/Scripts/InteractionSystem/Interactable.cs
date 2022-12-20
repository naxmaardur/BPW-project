using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is an edited version of an old Interaction system I build
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    protected Vector3 originalScale;
    protected bool active;
    private void Start()
    {
        originalScale = transform.localScale;
    }


    public virtual void Isactive(bool b)
    {

        if (b)
        {
            active = true;
            transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 1.2f, originalScale.z * 1.2f);
            InteractionEventManager.interactionEvent += Interaction;
        }
        else
        {
            active = false;
            transform.localScale = originalScale;
            InteractionEventManager.interactionEvent -= Interaction;
        }
    }

    public abstract void Interaction(GameObject Interactor);
}
