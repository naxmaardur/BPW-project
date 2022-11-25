using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpManager
{
    PlayerControler _context;

    public PlayerPickUpManager(PlayerControler ctx)
    {
        _context = ctx;
    }


    public bool PickUpItem(PickUpItem item)
    {
        switch (item.TypeId)
        {
            case 0:
                return PickUpHealth(item);
            case 1:
                return PickUpWeapon(item);
            case 2:
                return PickUpMagic(item);
        }

        return false;
    }


    public bool PickUpMagic(PickUpItem item)
    {

        return false;
    }

    public bool PickUpHealth(PickUpItem item)
    {
        _context.AddHealth(item.Count);
        return true;
    }

    public bool PickUpWeapon(PickUpItem item)
    {
        if (item.ItemId > _context.AnimatorListContainer.AnimatorsCount() - 1) return false;
        return _context.playerAnimator.ChangeRunTimeAnimatorTo(item.ItemId);
    }


}
