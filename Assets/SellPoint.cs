using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPoint : MonoBehaviour
{
    public ScrapManager scrapManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            scrapManager.ModifyScrap(other.GetComponent<PickupableObject>().itemInformation.shopData.SellPrice);
            Destroy(other.gameObject);
        }
    }
}
