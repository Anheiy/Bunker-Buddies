using FishNet;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnItems : MonoBehaviour
{
    public List<GameObject> spawnLocations = new List<GameObject>();
    public GameObject[] items;
    public float[] itemPercentages;
    public List<bool> itemMustSpawn = new List<bool>();
    public Timer time;
    private bool spawnedItems = false;
    private bool spawnMustSpawns = true;
    public int minspawnsPP;
    public int maxspawnsPP;
    public GameObject ToDoList;
    private void OnEnable()
    {
        time.time.OnChange += _ItemGeneration;
    }
    private void OnDisable()
    {
        time.time.OnChange -= _ItemGeneration;
    }

    private void _ItemGeneration(SyncTimerOperation op, float prev, float next, bool asServer)
    {
        ToDoList.SetActive(true);
        if (op == SyncTimerOperation.Start)
        {
            if (spawnedItems == false)
            {
                GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
                int spawns = 0;
                foreach (GameObject player in Players)
                {
                    int randomVal = Random.Range(minspawnsPP, maxspawnsPP + 1);
                    spawns = spawns += randomVal;
                    Debug.Log("Spawning Items... Amount: " + randomVal);
                }
                //foreach spawn
                for (int i = 0; i < spawns; i++)
                {

                    //if theres a item that must spawn it is spawned
                    if (spawnMustSpawns == true)
                        for (int j = 0; j < itemMustSpawn.Count; j++)
                        {
                            if (itemMustSpawn[j] == true)
                            {
                                int spawnedHere = Random.Range(0, spawnLocations.Count);
                                NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(items[j], spawnLocations[spawnedHere].transform.position, spawnLocations[spawnedHere].transform.rotation, true);
                                InstanceFinder.NetworkManager.ServerManager.Spawn(nob);
                                spawnLocations.RemoveAt(spawnedHere);
                                i++;
                                Debug.Log("must spawn has spawned");
                                spawnMustSpawns = false;
                            }
                        }

                    //randomly spawns items at random locations
                    if (spawnLocations.Count > 0)
                    {
                        int spawnedHere = Random.Range(0, spawnLocations.Count);
                        NetworkObject nob = InstanceFinder.NetworkManager.GetPooledInstantiated(items[GetRandomSpawn()], spawnLocations[spawnedHere].transform.position, spawnLocations[spawnedHere].transform.rotation, true);
                        InstanceFinder.NetworkManager.ServerManager.Spawn(nob);
                        spawnLocations.RemoveAt(spawnedHere);
                    }
                    else
                    {
                        break;
                    }
                }
                spawnedItems = true;
            }
        }
    }
    private int GetRandomSpawn()
    {
        float random = Random.Range(0f,1f);
        float numForAdding = 0;
        float total = 0;
        for (int i = 0; i < itemPercentages.Length; i++)
        {
            total += itemPercentages[i];
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (itemPercentages[i]/total + numForAdding >= random)
            {
                return i;
            }
            else
            {
                numForAdding += itemPercentages[i]/total;
            }
        }
        return 0;
    }
}
