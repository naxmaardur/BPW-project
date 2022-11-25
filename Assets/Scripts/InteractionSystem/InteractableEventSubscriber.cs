using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is an edited version of an old Interaction system I build
/// </summary>
public class InteractableEventSubscriber : MonoBehaviour
{
    Vector3 lastposition;
    [SerializeField] float reach;
    [SerializeField] LayerMask layerMask;




    Interactable currentInteractable;
    // Start is called before the first frame update
    void Start()
    {
        lastposition = transform.position;
        
    }



    void OnEnable()
    {
            StartCoroutine(CheckInteractables());
    }
    IEnumerator CheckInteractables()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => lastposition == transform.position);
            lastposition = transform.position;

            RaycastHit[] sphereHits;
            sphereHits = Physics.SphereCastAll(transform.position, reach, transform.forward, 200, layerMask);
            
            if (sphereHits.Length == 0)
            {
                if(currentInteractable != null)
                {
                    currentInteractable.Isactive(false);
                    currentInteractable = null;
                }
                continue;
            }

            RaycastHit currentClostesthit = sphereHits[0];
            foreach (RaycastHit hit in sphereHits)
            {
                if (hit.transform.gameObject == gameObject) continue;
                //if (hit.transform.tag != "Interactable") continue;
                if (DistancefromMe(hit.transform.position) > DistancefromMe(currentClostesthit.transform.position)) continue;
                currentClostesthit = hit;
            }
            Interactable interactable = currentClostesthit.transform.gameObject.GetComponent<Interactable>();
            if (interactable == currentInteractable) continue;
            if (currentInteractable != null)
                currentInteractable.Isactive(false);
            currentInteractable = interactable;
            currentInteractable.Isactive(true);
        }

    }

    float DistancefromMe(Vector3 v)
    {
        return Vector3.Distance(v, transform.position);
    }

    



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, reach);
        
    }
}
