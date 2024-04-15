using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using TMPro;

public class ScrapManager : NetworkBehaviour
{
    [SyncVar] private int scrap = 100;
    public TextMeshProUGUI scrapText;
    [ServerRpc(RequireOwnership = false)]
    public override void OnStartClient()
    {
        base.OnStartClient();
        scrapText.text = scrap.ToString();
    }
    public void ModifyScrap(int modifiedScrap)
    {
        scrap += modifiedScrap;
        scrapText.text = scrap.ToString();
    }
    public int getScrap()
    {
        return scrap;
    }
}
