using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerOsUi : MonoBehaviour
{
    public GameObject CameraPanel;
    public GameObject CreaturePanel;
    public GameObject EmailPanel;
    public GameObject ShopPanel;
    private LookingAt LookingAt;
    private ServerPlayer sp;
    private void Update()
    {
        FindDependencies();
    }

    public void TurnOnCameraPanel()
    {
            TurnOffAll();
            if (CameraPanel.activeInHierarchy)
                CameraPanel.SetActive(false);
            else
            {
                CameraPanel.SetActive(true);
            }
    }
    public void TurnOnCreaturePanel()
    {
            TurnOffAll();
            if (CreaturePanel.activeInHierarchy)
                CreaturePanel.SetActive(false);
            else
            {
                CreaturePanel.SetActive(true);
            }
    }
    public void TurnOnShopPanel()
    {
            TurnOffAll();
            if (ShopPanel.activeInHierarchy)
                ShopPanel.SetActive(false);
            else
                ShopPanel.SetActive(true);
    }

    public void TurnOnEmailPanel()
    {
            TurnOffAll();
            if (EmailPanel.activeInHierarchy)
                EmailPanel.SetActive(false);
            else
                EmailPanel.SetActive(true);
    }
    private void TurnOffAll()
    {
        CameraPanel.SetActive(false);
        CreaturePanel.SetActive(false);
        EmailPanel.SetActive(false);
        ShopPanel.SetActive(false);
    }
    private void FindDependencies()
    {
        if (!sp)
        {
            sp = GameObject.Find("GameManager").GetComponent<ServerPlayer>();
        }
        if (!LookingAt)
        {
            LookingAt = sp.player.GetComponent<LookingAt>();
        }
    }
}