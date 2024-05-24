using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TerminalShop : NetworkBehaviour
{
    public Transform deliveryLocation;
    public GameObject ConfirmationPopup;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;
    public int Amount = 0;
    public Item itemholder;
    public Item CurrentItem;
    private int CurrentCost;
    public ScrapManager ScrapManager;
    public Scene spawnScene;

    [ServerRpc(RequireOwnership = false)]
    public void PurchaseItem(NetworkObject nobj)
    {
        spawnScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Bunker");
        NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(nobj, deliveryLocation.position, deliveryLocation.rotation, true);
        InstanceFinder.NetworkManager.ServerManager.Spawn(nob,scene: spawnScene);
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
        Debug.Log("PURCHASED");
        for (int i = 0; i < Amount; i++)
        {
            PurchaseItem(CurrentItem.SendObject());
        }
        ScrapManager.ModifyScrap(-(CurrentCost * Amount));
        ResetWindow();
        Debug.Log("TODAY");

    }
    public void DenyPurchase()
    {
       ResetWindow();   
    }

    public void ResetWindow()
    {
        ConfirmationPopup.SetActive(false);
        Amount = 0;
        itemAmount.text = Amount.ToString();
    }
}
