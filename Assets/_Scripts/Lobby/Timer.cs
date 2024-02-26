using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : NetworkBehaviour
{
    public int maxTime = 60;
    public Image clockImage;
    [SyncVar] public bool StartCount = false;
    [SyncObject] public readonly SyncTimer time = new SyncTimer();


    // Update is called once per frame
    void Update()
    {
        if (base.IsServer)
            if (StartCount == false)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    time.StartTimer(maxTime);
                    StartCount = true;
                }
            }
            
        if (StartCount)
        {
            UpdateTimeImage();
        }

    }


    public void UpdateTimeImage()
    {
        time.Update(Time.deltaTime);
        clockImage.fillAmount = time.Remaining / maxTime;
    }
}
