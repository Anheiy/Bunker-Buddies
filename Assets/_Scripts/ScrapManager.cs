using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using TMPro;

public class ScrapManager : NetworkBehaviour
{
    [SyncVar(WritePermissions = WritePermission.ClientUnsynchronized)] public int scrap = 100;
    public TextMeshProUGUI scrapText;

    private void Start()
    {
        scrapText.text = scrap.ToString();
    }
    [ServerRpc(RequireOwnership = false,RunLocally = true)]
    public void ModifyScrap(int modifiedScrap)
    {
        ModifyScrapOnClients(modifiedScrap);
    }
    [ObserversRpc]
    private void ModifyScrapOnClients(int modifiedScrap)
    {
        scrap += modifiedScrap;
        UpdateText();
    }
    private void UpdateText()
    {
        scrapText.text = scrap.ToString();
    }
    public int getScrap()
    {
        return scrap;
    }
}
