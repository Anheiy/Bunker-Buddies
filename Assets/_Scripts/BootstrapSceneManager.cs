using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapSceneManager : MonoBehaviour
{
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
    }
    public void LoadScene(string sceneName)
    {
        if (!InstanceFinder.IsServer)
            return;
        
        SceneLoadData sld = new SceneLoadData(sceneName);
        sld.ReplaceScenes = ReplaceOption.All;
        InstanceFinder.SceneManager.LoadGlobalScenes(sld);
    }
    public void UnloadScene(string sceneName)
    {
        if (!InstanceFinder.IsServer)
            return;
        SceneUnloadData sld = new SceneUnloadData(sceneName);
        InstanceFinder.SceneManager.UnloadGlobalScenes(sld);
    }
}

