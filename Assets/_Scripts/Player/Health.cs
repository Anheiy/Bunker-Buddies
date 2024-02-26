using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class Health : NetworkBehaviour
{
    
    [SyncVar] public float hp = 100;
    public float maxhp = 100;
    public Image HealthBar;
    public TextMeshProUGUI HealthBarText;

    // Update is called once per frame
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.IsOwner)
        {
            HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
            HealthBarText = GameObject.Find("HealthValue").GetComponent<TextMeshProUGUI>();
        }
    }
    private void Update()
    {
        if (!base.IsOwner)
            return;
        if (HealthBar && this.tag == "Player")
            HealtBarUpdate();
        if(hp > maxhp)
        {
            hp = maxhp;
        }
        if (hp < 0)
        {
            hp = 0;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void modifyHealth(float heal, float timeToConsume, float amountOfTicks, float timePerTick)
    {
        if (hp <= maxhp && hp >= 0)
        {
            this.StartCoroutine(Heal(heal, timeToConsume, amountOfTicks, timePerTick));
        }
    }
    private void HealtBarUpdate()
    {
        HealthBar.fillAmount = (hp / maxhp);
        if (HealthBarText)
        {
            HealthBarText.text = Mathf.Round(hp).ToString();
        }
    }
    IEnumerator Heal(float heal, float timeToConsume, float amountOfTicks, float timePerTick)
    {
        yield return new WaitForSeconds(timeToConsume);
        for (int i = 0; i < amountOfTicks; i++)
        {
            hp += heal/(amountOfTicks);
            yield return new WaitForSeconds(timePerTick);
        }

    }
}
