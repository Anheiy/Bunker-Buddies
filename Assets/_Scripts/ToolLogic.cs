using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolLogic : MonoBehaviour
{
    private InventoryManager inventory;
    private HotkeyManager hotkey;
    private bool isActive;
    private float time;
    private Item selectedItem;
    void Start()
    {
        inventory = this.GetComponent<InventoryManager>();
        hotkey = this.GetComponent<HotkeyManager>();
    }

    void Update()
    {
        //if swapping disable isActive
        if (selectedItem != inventory.HeldItem)
        {
            selectedItem = inventory.HeldItem;
            isActive = false;
        }
        
        if (hotkey.currentlySelectedHotKey >= 0)
        {
            if (selectedItem.SendItemType() == "tool")
            {
                //logic for isActive tools
                if(isActive)
                {
                    ChargeUpdate(selectedItem);
                }
                if(Input.GetKeyDown(KeyCode.Mouse0)) 
                {
                    if (isActive)
                        isActive = false;
                    else
                        isActive = true;
                }
                //need logic for held down tools
            }


        }
    }
    public void ChargeUpdate(Item selectedItem)
    {
        time += Time.deltaTime;
        if (time > selectedItem.SendtoolChargeTick())
        {
            if (inventory.charges[hotkey.currentlySelectedHotKey] > 0)
            {
                inventory.charges[hotkey.currentlySelectedHotKey] = inventory.charges[hotkey.currentlySelectedHotKey] - 1;
            }
            time = 0;
        }
    }
}

