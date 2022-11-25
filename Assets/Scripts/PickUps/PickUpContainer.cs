using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpContainer : MonoBehaviour
{
    public bool automaticPickUP;
    public PickUpItem item;



    private void OnTriggerEnter(Collider other)
    {
        if (automaticPickUP)
        {
            PlayerControler player = other.GetComponent<PlayerControler>();
            if (player != null)
            {
                PickUp(player);
            }
        }
    }


    public void PickUp(PlayerControler player)
    {
        if (player.PickUpItem(item))
        {
            Destroy(this.gameObject);
        }
    }
}
