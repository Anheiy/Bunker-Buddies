using FishNet;
using FishNet.Demo.AdditiveScenes;
using FishNet.Managing;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public List<Item> itemsCollected = new List<Item>();
    private NetworkManager networkManager;
    private void Start()
    {
        networkManager = InstanceFinder.NetworkManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemsCollected.Add(other.gameObject.GetComponent<PickupableObject>().itemInformation);
            networkManager.ServerManager.Despawn(other.gameObject);
        }
    } 
}
