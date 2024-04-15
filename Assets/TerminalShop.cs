using FishNet;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalShop : MonoBehaviour
{
    public Transform deliveryLocation;
    public GameObject ConfirmationPopup;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;
    public int Amount = 0;
    private Item CurrentItem;
    private int CurrentCost;
    public ScrapManager ScrapManager;
    public void PurchaseItem(Item item)
    {
        NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(item.SendObject(), deliveryLocation.position, deliveryLocation.rotation, true);
        InstanceFinder.NetworkManager.ServerManager.Spawn(nob);
    }

    public void TurnOnConfirmationWindow(Item item, int Cost)
    {
        itemImage.sprite = item.SendIcon();
        itemName.text = "Purchase " + item.SendName() + "?";
        CurrentItem = item;
        ConfirmationPopup.SetActive(true);
        CurrentCost = Cost;
    }

    public void IncreaseAmount()
    {
        if (CurrentCost * (Amount + 1) < ScrapManager.getScrap())
        {
            Amount++;
            itemAmount.text = Amount.ToString();
            Debug.Log(CurrentCost * Amount);
        }
    }
    public void DecreaseAmount()
    {
        if (Amount > 0)
        {
            Amount--;
            itemAmount.text = Amount.ToString();
            Debug.Log(CurrentCost * Amount);
        }
    }
    public void ConfirmPurchase()
    {
        for (int i = 0; i < Amount; i++)
        {
            PurchaseItem(CurrentItem);
        }
        ScrapManager.ModifyScrap(-(CurrentCost * Amount));
        ResetWindow();

    }
    public void DenyPurchase()
    {
       ResetWindow();   
    }
    public void ResetWindow()
    {
        CurrentItem = null;
        ConfirmationPopup.SetActive(false);
        Amount = 0;
        itemAmount.text = Amount.ToString();
    }
}
