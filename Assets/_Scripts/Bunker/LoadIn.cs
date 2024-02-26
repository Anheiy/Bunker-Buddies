using FishNet;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Scened;
using FishNet.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIn : NetworkBehaviour
{
    private Scene lastScene;
    private NetworkManager networkManager;
    private ItemCollector itemCollector;
    public Vector3 spawnLocation;
    private void Start()
    {
        lastScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("House");
        networkManager = InstanceFinder.NetworkManager;
        itemCollector = GameObject.Find("ItemsCollected").GetComponent<ItemCollector>();
    }
    private void Update()
    {
        SpawnCollectedItems();
    }
    private void SpawnCollectedItems()
    {
        if (!networkManager.SceneManager.SceneConnections.ContainsKey(lastScene))
        {
            Debug.Log("IN!");
            SceneUnloadData sld = new SceneUnloadData("House");
            networkManager.SceneManager.UnloadGlobalScenes(sld);
            foreach (Item item in itemCollector.itemsCollected)
            {
                NetworkObject spawned = InstanceFinder.NetworkManager.GetPooledInstantiated(item.SendObject(), spawnLocation, Quaternion.identity, true);
                networkManager.ServerManager.Spawn(spawned);
                Debug.Log(spawned);
            }
            Destroy(itemCollector.gameObject);
            Destroy(this.gameObject);
        }

    }



}
