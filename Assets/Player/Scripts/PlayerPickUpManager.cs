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
        MagicSpell spell = GameMaster.Instance.EquipableItemContainer.GetSpellById(item.ItemId);
        if (_context.MagicContainer.Spell != null)
        {
            if (_context.MagicContainer.Spell != spell)
            {
                return false;
            }
        }
        _context.MagicContainer.Spell = spell;
        _context.MagicContainer.Color = _context.MagicContainer.Spell.color;
        _context.MagicContainer.AddSpellCharges(item.Count);
        _context.OnSpellUpdate?.Invoke(_context.MagicContainer);
        return true;
    }
    public bool PickUpHealth(PickUpItem item)
    {
        _context.AddHealth(item.Count);
        return true;
    }
    public bool PickUpWeapon(PickUpItem item)
    {
        if (item.ItemId > _context.AnimatorListContainer.AnimatorsCount() - 1) return false;
        _context.ChangeWeaponTo(item.ItemId);
        return _context.playerAnimator.ChangeRunTimeAnimatorTo(item.ItemId);
    }
}
