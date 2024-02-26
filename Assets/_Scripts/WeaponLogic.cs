using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    private InventoryManager inventory;
    private HotkeyManager hotkey;
    private PlayerAttack pAttack;
    public ServerPlayer sp;
    void Start()
    {
        sp = this.GetComponent<ServerPlayer>();
        inventory = this.GetComponent<InventoryManager>();
        hotkey = this.GetComponent<HotkeyManager>();
    }

    void Update()
    {
        if(!pAttack)
            pAttack = sp.player.GetComponent<PlayerAttack>();
           
        if (hotkey.currentlySelectedHotKey >= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (inventory.HeldItem.SendItemType() == "weapon")
                {
                    pAttack.AttackRay(inventory.HeldItem.SendweaponDamage(), inventory.HeldItem.SendweaponCooldown(), inventory.HeldItem.SendweaponRange(), inventory.HeldItem.SendweaponLayer());
                }
                else
                {
                    return;
                }
            }

        }
    }
}
