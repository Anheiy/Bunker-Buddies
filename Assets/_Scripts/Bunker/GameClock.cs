using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;
using System;

public class GameClock : NetworkBehaviour
{
    [SyncObject] public readonly SyncTimer seconds = new SyncTimer();
    [SyncVar] public bool StartCount = false;
    public int hours;
    public int days;
    public int maxSeconds = 60;
    public int updateTick = 10;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI clockText;
    // Start is called before the first frame update
 
    private void Update()
    {
            if (StartCount == false)
            {
                seconds.StartTimer(maxSeconds);
                StartCount = true;
            }

        if (StartCount)
        {
            seconds.Update(Time.deltaTime);
            if (Mathf.Round(seconds.Elapsed * (60 / maxSeconds)) % updateTick == 0)
            clockText.text = string.Format("{0:00}:{1:00}", hours, seconds.Elapsed * (60/maxSeconds));
            daysText.text = days.ToString();
        }
        if (Mathf.Round(seconds.Elapsed) == maxSeconds)
        {
            seconds.StartTimer(maxSeconds);
            if (hours == 23)
            {
                hours = 0;
                days++;
            }
            else
            hours++;
        }
    }
    public void StartClock()
    {
        seconds.StartTimer(60);
    }

}
