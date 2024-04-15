using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Transporting;
using UnityEngine.UI;
using FishNet.Object;

public class Power : NetworkBehaviour
{
    [SyncVar] private float power = 100;
    [SerializeField] private float powerWeight = 1;
    private Image powerImage;
    public void Start()
    {
        powerImage = GameObject.Find("PowerImage").GetComponent<Image>();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsHost)
        InvokeRepeating("PowerDegen", 0, 1);
    }
    private void Update()
    {
        powerImage.fillAmount = power / 100;
        if(power > 100)
        {
            power = 100;
        }
        if (power < 0)
        {
            power = 0;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void AddPower(float addedPower)
    {
        power = power + addedPower;
    }
    [ServerRpc(RequireOwnership = false)] 
    public void SubtractPower(float subtractedPower)
    {
        power = power - subtractedPower;
    }
    [ServerRpc(RequireOwnership = false)]
    private void PowerDegen()
    {
        SubtractPower(powerWeight);
    }
    public float getPower()
    {
        return power;
    }

}
