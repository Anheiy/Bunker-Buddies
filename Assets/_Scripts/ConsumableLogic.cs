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
                if (inventory.HeldItem.SendItemType() == "consumable")
                {
                    if (inventory.HeldItem.SendconsumableHeal() != 0) 
                    CheckHealth();
                    if (inventory.HeldItem.SendconsumableStamina() != 0)
                    CheckStamina();
                    if (inventory.HeldItem.SendconsumableThirst() != 0)
                    CheckThirst();
                    if (inventory.HeldItem.SendconsumableHunger() != 0)
                    CheckHunger();
                    if (oneActive)
                    {
                        inventory.RemoveItem(hotkey.currentlySelectedHotKey);
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
            health.modifyHealth(inventory.HeldItem.SendconsumableHeal(), inventory.HeldItem.SendlengthofConsume(), inventory.HeldItem.SendtickAmount(), inventory.HeldItem.SendtickTime());
            oneActive = true;
        }
    }
    public void CheckStamina()
    {
        if(stamina.stamina < stamina.maxStamina)
        {
            stamina.ModifyStamina(inventory.HeldItem.SendconsumableStamina(), inventory.HeldItem.SendlengthofConsume());
            oneActive = true;
        }
    }
    public void CheckThirst()
    {
        if( thirst.thirst < thirst.maxThirst)
        {
            thirst.ModifyThirst(inventory.HeldItem.SendconsumableThirst(), inventory.HeldItem.SendlengthofConsume());
            oneActive = true;
        }
    
    }
    public void CheckHunger()
    {
        if(hunger.hunger < hunger.maxHunger)
        {
            hunger.ModifyHunger(inventory.HeldItem.SendconsumableHunger(), inventory.HeldItem.SendlengthofConsume());
            oneActive = true;
        }
    }

    
}





