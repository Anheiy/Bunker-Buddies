using FishNet;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class LobbyExplosion : MonoBehaviour
{
    //public
    public Timer gameTimer;
    public NetworkObject explosionPrefab;
    public Transform explosionSpawn;
    public float timeBeforeScaled;
    public float scaleAmount;
    //private
    private NetworkObject explosion = null;
    public bool isScaleable = true;
    public bool timeComplete = false;
    private NetworkManager networkManager;
    private void Start()
    {
        networkManager = InstanceFinder.NetworkManager;
        gameTimer.time.OnChange += TimeChange;
    }
    private void OnDestroy()
    {
        gameTimer.time.OnChange -= TimeChange; 
    }

    private void TimeChange(SyncTimerOperation op, float prev, float next, bool asServer)
    {
        if(op == SyncTimerOperation.Finished)
        {
            timeComplete = true;
        }
    }

    void Update()
    {
        if (timeComplete && isScaleable == true)
        {
            Debug.Log("in");
            if (!explosion)
            explosion = networkManager.GetPooledInstantiated(explosionPrefab, explosionSpawn.position, explosionSpawn.rotation, true);
            networkManager.ServerManager.Spawn(explosion);
            StartCoroutine(updateScale());
        }
    }

    IEnumerator updateScale()
    {
        isScaleable = false;
        yield return new WaitForSeconds(timeBeforeScaled);
        explosion.transform.localScale = new Vector3(explosion.transform.localScale.x * scaleAmount, explosion.transform.localScale.y, explosion.transform.localScale.z * scaleAmount);
        isScaleable = true;
        
    }
     
}
