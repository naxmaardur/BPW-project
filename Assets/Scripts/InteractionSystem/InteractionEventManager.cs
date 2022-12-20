using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script is an edited version of an old Interaction system I build
/// </summary>
public class InteractionEventManager
{
    public delegate void InteractionEventHandler(GameObject interactor);
    public static event InteractionEventHandler interactionEvent;

    public void TriggerEvent(GameObject interactor)
    {
        if (interactionEvent == null) return;
        interactionEvent(interactor);
    }
}
