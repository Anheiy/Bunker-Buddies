using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Object;
using FishNet.Connection;

public class Thirst : NetworkBehaviour
{
    public float thirst = 100;
    public float maxThirst = 100;
    public float TickSpeed = 1;
    public Image ThirstBar;
    public TextMeshProUGUI ThirstBarText;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            ThirstBar = GameObject.Find("ThirstBar").GetComponent<Image>();
        }
        else
        {
            enabled = false;
        }
    }
    private void Start()
    {
        InvokeRepeating("RepeatedModifyThirst", 0, TickSpeed);
    }
    private void Update()
    {
        if (ThirstBar)
            UpdateThirst();
        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
        if (thirst < 0)
        {
            thirst = 0;
        }
    }
    public void ModifyThirst(float ThirstValue, float timeToConsume)
    {
        StartCoroutine(Thirsting(ThirstValue, timeToConsume));
    }
    public void UpdateThirst()
    {
        ThirstBar.fillAmount = (thirst/maxThirst);
        if (ThirstBarText)
        {
            ThirstBarText.text = Mathf.Round(thirst).ToString();
        }
    }
    public void RepeatedModifyThirst()
    {
        ModifyThirst(-1, 0);
    }
    IEnumerator Thirsting(float thirstValue, float timeToConsume)
    {
        yield return new WaitForSeconds(timeToConsume);
        thirst += thirstValue;
    }
}
