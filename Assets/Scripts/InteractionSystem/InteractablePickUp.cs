using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is an edited version of an old Interaction system I build
/// </summary>
public class InteractablePickUp : Interactable
{


    [SerializeField] MeshRenderer[] highlights;
    PickUpContainer _pickUp;

    private void Start()
    {
        _pickUp = GetComponent<PickUpContainer>();
        originalScale = transform.localScale;
    }
    public override void Interaction(GameObject interactor)
    {
        if (_pickUp.PickUpWithoutDestroy(interactor.GetComponent<PlayerControler>()))
        {
            Isactive(false);
            Destroy(this.gameObject);
        }
    }
    public override void Isactive(bool b)
    {
        if (b)
        {
            transform.localScale = new Vector3(2, 2, 2);
            foreach (MeshRenderer m in highlights)
            {
                m.material.color = Color.red;

            }
        }
        else
        {
            foreach (MeshRenderer m in highlights)
            {
                m.material.color = Color.gray;
            }
            transform.localScale = originalScale;
        }


        base.Isactive(b);
    }
}
