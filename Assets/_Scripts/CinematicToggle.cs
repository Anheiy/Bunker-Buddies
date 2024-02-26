using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicToggle : MonoBehaviour
{
    [Header("UI to Hide")]
    public GameObject InteractableUI;
    public GameObject Crosshair;
    public GameObject HotkeyUI;

    [Header("Logic to Disable")]
    private WeaponLogic weaponLogic;
    private ConsumableLogic consumableLogic;
    private ToolLogic toolLogic;
    private void Start()
    {
        weaponLogic = this.GetComponent<WeaponLogic>();
        consumableLogic = this.GetComponent<ConsumableLogic>();
        toolLogic = this.GetComponent<ToolLogic>();
    }
    public void ToggleOn()
    {
        EnableLogic();
        EnableUI();
    }
    public void ToggleOff()
    {
        DisableLogic();
        DisableUI();
    }

    private void EnableLogic()
    {

        weaponLogic.enabled = true;
        consumableLogic.enabled = true;
        toolLogic.enabled = true;
    }
    private void DisableLogic()
    {
        weaponLogic.enabled = false;
        consumableLogic.enabled = false;
        toolLogic.enabled = false;
    }

    private void EnableUI()
    {
        InteractableUI.SetActive(true);
        Crosshair.SetActive(true);
        HotkeyUI.SetActive(true);
    }
    private void DisableUI()
    {
        InteractableUI.SetActive(false);
        Crosshair.SetActive(false);
        HotkeyUI.SetActive(false);
    }
}
