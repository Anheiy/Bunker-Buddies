using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Connection;
using FishNet.Object;

public class Hunger : NetworkBehaviour
{
    public float hunger = 100;
    public float maxHunger = 100;
    public float TickSpeed = 1;
    public Image HungerBar;
    public TextMeshProUGUI HungerBarText;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
          HungerBar = GameObject.Find("HungerBar").GetComponent<Image>();
        }
        else
        {
            enabled = false;
        }
    }
    private void Start()
    {
        InvokeRepeating("RepeatedModifyHunger", 0, TickSpeed);
    }
    private void Update()
    {
        if(HungerBar)
        UpdateHunger();
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
        if (hunger < 0)
        {
            hunger = 0;
        }
    }
    public void ModifyHunger(float HungerValue, float timeToConsume)
    {
        StartCoroutine(StaminaManage(HungerValue, timeToConsume));
    }
    public void UpdateHunger()
    {
        HungerBar.fillAmount = (hunger / maxHunger);
        if (HungerBarText)
        {
            HungerBarText.text = Mathf.Round(hunger).ToString();
        }
    }
    public void RepeatedModifyHunger()
    {
        ModifyHunger(-1, 0);
    }
    IEnumerator StaminaManage(float hungerValue, float timeToConsume)
    {
        yield return new WaitForSeconds(timeToConsume);
        hunger += hungerValue;
    }
}
