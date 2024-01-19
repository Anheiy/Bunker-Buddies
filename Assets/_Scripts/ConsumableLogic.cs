using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableLogic : MonoBehaviour
{
    
    public Health health;
    private Stamina stamina;
    private Thirst thirst;
    private Hunger hunger;
    private bool oneActive = false;
    private ServerPlayer sp;
    private InventoryManager inventory;
    private HotkeyManager hotkey;

    
    private void Start()
    {
        inventory = this.GetComponent<InventoryManager>();
        hotkey = this.GetComponent<HotkeyManager>();
    }
    public void Update()
    {
        if (!sp)
        {
            sp = this.GetComponent<ServerPlayer>();
        }
        else if (!health || !stamina || !thirst || !hunger)
        {
            health = sp.player.GetComponent<Health>();
            stamina = sp.player.GetComponent<Stamina>();
            thirst = sp.player.GetComponent<Thirst>();
            hunger = sp.player.GetComponent<Hunger>();
        }
        
        if (hotkey.currentlySelectedHotKey >= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendItemType() == "consumable")
                {
                    if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableHeal() != 0) 
                    CheckHealth();
                    if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableStamina() != 0)
                    CheckStamina();
                    if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableThirst() != 0)
                    CheckThirst();
                    if (inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableHunger() != 0)
                    CheckHunger();
                    if (oneActive)
                    {
                        inventory.RemoveItem(hotkey.currentlySelectedHotKey + 1);
                        oneActive = false;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
    public void CheckHealth()
    {
        if (health.hp < health.maxhp)
        {
            health.modifyHealth(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableHeal(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendlengthofConsume(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendtickAmount(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendtickTime());
            oneActive = true;
        }
    }
    public void CheckStamina()
    {
        if(stamina.stamina < stamina.maxStamina)
        {
            stamina.ModifyStamina(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableStamina(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendlengthofConsume());
            oneActive = true;
        }
    }
    public void CheckThirst()
    {
        if( thirst.thirst < thirst.maxThirst)
        {
            thirst.ModifyThirst(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableThirst(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendlengthofConsume());
            oneActive = true;
        }
    
    }
    public void CheckHunger()
    {
        if(hunger.hunger < hunger.maxHunger)
        {
            hunger.ModifyHunger(inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendconsumableHunger(), inventory.slotlist[hotkey.currentlySelectedHotKey + 1].SendlengthofConsume());
            oneActive = true;
        }
    }

    
}





