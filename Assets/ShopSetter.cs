using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSetter : MonoBehaviour
{
    public Item[] PossibleItems;
    public GameObject ShopItemPrefab;
    public GameObject ItemsHolder;
    private TerminalShop terminalShop;
    private void Start()
    {
        terminalShop = this.GetComponent<TerminalShop>();
        SetupDefaultShop();
    }
    public void SetupDefaultShop()
    {
        for (var i = ItemsHolder.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ItemsHolder.transform.GetChild(i).gameObject);
        }
        foreach (Item item in PossibleItems)
        {
            GameObject currentShopItem = Instantiate(ShopItemPrefab,ItemsHolder.transform);
            currentShopItem.GetComponent<Image>().sprite = item.SendIcon();
            int ItemCost = (int)(item.SendCost() * Random.Range(item.SendMinMultiplier(), item.SendMaxMultiplier()));
            currentShopItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemCost.ToString();
            currentShopItem.GetComponent<TerminalInteractable>().onUIClicked.AddListener(() => terminalShop.TurnOnConfirmationWindow(item, ItemCost));
            terminalShop.ResetWindow();
        }
    }
}
