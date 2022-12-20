using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HiddingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();
        if (player != null)
        {
            player.InHiddingZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();
        if (player != null)
        {
            player.InHiddingZone = false;
        }
    }
}
