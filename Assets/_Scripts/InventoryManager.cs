using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<int,Item> slotlist = new Dictionary<int,Item>();
    public int maxItems = 10;
    public HotkeyManager hotkey;
    public Item HeldItem;
    private void Update()
    {
        HeldItem = slotlist[hotkey.currentlySelectedHotKey + 1];
    }
    private void Start()
    {
        hotkey = this.GetComponent<HotkeyManager>();
        for (int i = 1; i <= maxItems; i++)
        {
            slotlist.Add(i, null);
        }
    }

    public bool CheckInventory(Item item)
    {
        foreach (KeyValuePair<int, Item> itemTest in slotlist)
        {
            if (itemTest.Value == null)
            {
                AddInventory(item);
                return true;
            }
        }
        return false;
    }
    private void AddInventory(Item item)
    {
        if (hotkey.currentlySelectedHotKey != -1 && slotlist[hotkey.currentlySelectedHotKey + 1] == null)
        {
            slotlist[hotkey.currentlySelectedHotKey + 1] = item;
            RefreshIcons();
            RefreshNames();
            return;
        }
        else
        {
            foreach (KeyValuePair<int, Item> itemTest in slotlist)
            {
                if (itemTest.Value == null)
                {
                    slotlist[itemTest.Key] = item;
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
        hotkey.Deselect();
        RefreshIcons();
        RefreshNames();
    }
    private void RefreshIcons()
    {
        Debug.Log("Refreshing Icons");
        foreach (KeyValuePair<int, Item> itemTest in slotlist)
        {
            if (itemTest.Value != null)
            {
                if (itemTest.Key <= hotkey.Slots.Length)
                {
                    hotkey.Slots[itemTest.Key - 1].gameObject.SetActive(true);
                    hotkey.Slots[itemTest.Key - 1].sprite = slotlist[itemTest.Key].SendIcon();
                }
            }
            else
            {
                if (itemTest.Key <= hotkey.Slots.Length)
                {
                    hotkey.Slots[itemTest.Key - 1].gameObject.SetActive(false);
                }
            }
        }
    }

    private void RefreshNames()
    {
        for (int i = 0; i < hotkey.Slots.Length; i++)
        {
            if (slotlist[i +1] != null)
            {
                hotkey.SlotNames[i].text = slotlist[i+1].SendName();
            }
            else
            {
                hotkey.SlotNames[i].text = "";
            }
        }
    }

    
    

   
}
