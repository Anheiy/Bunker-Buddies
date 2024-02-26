using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Item> slotlist = new List<Item>();
    public List<int> charges = new List<int>();
    public int maxItems = 10;
    public HotkeyManager hotkey;
    public Item HeldItem;
    public int HeldItemCharge;
    private void Update()
    {
        if(hotkey.currentlySelectedHotKey >= 0)
        HeldItem = slotlist[hotkey.currentlySelectedHotKey];
    }
    private void Start()
    {
        hotkey = this.GetComponent<HotkeyManager>();
        for (int i = 1; i <= maxItems; i++)
        {
            slotlist.Add(null);
            charges.Add(0);
        }
    }

    public bool CheckInventory(Item item, int charge)
    {
        foreach (Item itemTest in slotlist)
        {
            if (itemTest == null)
            {
                AddInventory(item, charge);
                return true;
            }
        }
        return false;
    }
    public bool CheckInventory(Item item)
    {
        foreach (Item itemTest in slotlist)
        {
            if (itemTest == null)
            {
                AddInventory(item);
                return true;
            }
        }
        return false;
    }
    private void AddInventory(Item item, int charge)
    {
        if (hotkey.currentlySelectedHotKey != -1 && HeldItem == null)
        {
            slotlist[hotkey.currentlySelectedHotKey] = item;
            charges[hotkey.currentlySelectedHotKey] = charge;
            RefreshIcons();
            RefreshNames();
            return;
        }
        else
        {
            for (int i = 0; i < slotlist.Count; i++)
            {
                if (slotlist[i] == null)
                {
                    slotlist[i] = item;
                    charges[i] = charge;
                    RefreshIcons();
                    RefreshNames();
                    return;
                }
            }
        }

    }
    private void AddInventory(Item item)
    {
        if (hotkey.currentlySelectedHotKey != -1 && HeldItem == null)
        {
            slotlist[hotkey.currentlySelectedHotKey] = item;
            RefreshIcons();
            RefreshNames();
            return;
        }
        else
        {
            for (int i = 0; i < slotlist.Count; i++)
            {
                if (slotlist[i] == null)
                {
                    slotlist[i] = item;
                    RefreshIcons();
                    RefreshNames();
                    return;
                }
            }
        }

    }
    public void RemoveItem(int index)
    {
        slotlist[index] = null;
        charges[index] = 0;
        hotkey.Deselect();
        RefreshIcons();
        RefreshNames();
    }
    private void RefreshIcons()
    {
        Debug.Log("Refreshing Icons");
        for (int i = 0; i < slotlist.Count; i++)
        {
            if (slotlist[i] != null)
            {
                if (i <= hotkey.Slots.Length)
                {
                    hotkey.Slots[i].gameObject.SetActive(true);
                    hotkey.Slots[i].sprite = slotlist[i].SendIcon();
                }
            }
            else
            {
                if (i <= hotkey.Slots.Length)
                {
                    hotkey.Slots[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void RefreshNames()
    {
        for (int i = 0; i < hotkey.Slots.Length; i++)
        {
            if (slotlist[i] != null)
            {
                hotkey.SlotNames[i].text = slotlist[i].SendName();
            }
            else
            {
                hotkey.SlotNames[i].text = "";
            }
        }
    }

    
    

   
}
