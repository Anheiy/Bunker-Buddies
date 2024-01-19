using FishNet;
using FishNet.Object;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Managing;
using FishNet.Transporting;
using System;
using FishNet.Connection;
using UnityEngine.SceneManagement;
using FishNet.Managing.Scened;

public class SpawnPlayers : NetworkBehaviour
{


    [SerializeField]
    private NetworkObject _playerPrefab;
    public Transform Spawnpoint;
    private NetworkManager _networkManager;
    public List<NetworkObject> playersObjects;
    public int currentIndex = 0;

    private void Start()
    {
        InitializeOnce();
        
    }

    private void Update()
    {
        GameObject[] clients = GameObject.FindGameObjectsWithTag("Client");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("clients: " + clients.Length + " players: " + players.Length);
        if (clients.Length > players.Length)
        {
           Spawn();
        }
    }

    private void Disconnected(NetworkConnection connection, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            Debug.Log("connected");
        }
        if (args.ConnectionState == RemoteConnectionState.Stopped)
        {
            currentIndex--;
            Debug.Log("disconnected");
        }
    }

    private void InitializeOnce()
    {
        _networkManager = InstanceFinder.NetworkManager;
        if (_networkManager == null)
        {
            Debug.LogWarning($"PlayerSpawner on {gameObject.name} cannot work as NetworkManager wasn't found on this object or within parent objects.");
            return;
        }
        _networkManager.ServerManager.OnRemoteConnectionState += Disconnected;
    }

    private void Spawn()
    {
        
        GameObject[] clients = GameObject.FindGameObjectsWithTag("Client");
        NetworkObject clientnob = clients[currentIndex].GetComponent<NetworkObject>();
        NetworkObject nob = _networkManager.GetPooledInstantiated(_playerPrefab, Spawnpoint.position, Spawnpoint.rotation, true);
        playersObjects.Add(nob);
        _networkManager.ServerManager.Spawn(nob, clientnob.Owner);
        currentIndex++;

    }


}


    


       
        