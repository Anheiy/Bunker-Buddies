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
                if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendItemType() == "weapon")
                {
                    pAttack.AttackRay(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendweaponDamage(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendweaponCooldown(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendweaponRange(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendweaponLayer());
                }
                else
                {
                    return;
                }
            }

        }
    }
}
