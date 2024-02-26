using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Object;
using FishNet.Connection;

public class Stamina : NetworkBehaviour
{
    public float stamina = 0;
    public float maxStamina = 100;
    public bool isAllowedToRegen = true;
    public float RegenSpeed = 1;
    public Image StaminaBar;
    public TextMeshProUGUI StaminaBarText;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            StaminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();
        }
        else
        {
            enabled = false;
        }
    }
    private void Update()
    {
        if (StaminaBar)
            UpdateStaminaBar();
            RegenStamina();
        //control values
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        if (stamina < 0)
        {
            stamina = 0;
        }
        
    }
    public void ModifyStamina(float addedstamina,float timeToConsume)
    {
        StartCoroutine(StaminaManage(addedstamina, timeToConsume));
    }
    public void UpdateStaminaBar()
    {
        StaminaBar.fillAmount = (stamina / maxStamina);
        if (StaminaBarText)
        {
            StaminaBarText.text = Mathf.Round(stamina).ToString();
        }
    }
    public void RegenStamina()
    {
        if (isAllowedToRegen == true && stamina < maxStamina)
        {
            if (stamina < maxStamina)
            {
                StartCoroutine(staminaRegen());
            }
        }
    }

    IEnumerator staminaRegen()
    {
        isAllowedToRegen = false;
        while (stamina != maxStamina)
        {
            stamina++;
            yield return new WaitForSeconds(RegenSpeed);
        }
        isAllowedToRegen = true;
        
    }
    IEnumerator StaminaManage(float staminaValue, float timeToConsume)
    {
        yield return new WaitForSeconds(timeToConsume);
        stamina += staminaValue;
    }
}
