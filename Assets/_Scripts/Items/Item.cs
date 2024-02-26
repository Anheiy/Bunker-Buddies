using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    //item info
    enum itemType { weapon, consumable, tool };
    public enum toolType { light, talkie };
    //all objects have these
    [Header("Base Information")]
    [SerializeField] new string name;
    [SerializeField] string desription;
    [SerializeField] itemType ItemType;
    [SerializeField] Sprite icon;
    [SerializeField] NetworkObject Prefab;


    //weapons
    [System.Serializable]
    public class Weapon
    {
        public float damage;
        public float attackDistance;
        public float attackLayer;
        public float cooldown;
        public AudioClip weaponsoundEffect;
    }
    //consumable
    [System.Serializable]
    public class Consumable
    {
        public float healAmount;
        public float staminaAmount;
        public float hungerAmount;
        public float thirstAmount;
        public float speedAmount;
        public float lengthOfConsume;
        public float tickAmount;
        public float tickTime;
    }


    //tool
    [System.Serializable]
    public class Tool
    {
        //Core of Tool
        public bool isActive;
        //Possible Features
        public bool isLightable;
        public toolType toolType;
        public float toolChargeTick = 1;
    }

    public Weapon weaponData;
    public Consumable consumableData;
    public Tool toolData;
    #region Senders
    
    public string SendItemType()
    {
        return ItemType.ToString();
    }
    public string SendName()
    {
        return name;
    }
    public string SendDescription()
    {
        return desription;
    }
    public Sprite SendIcon()
    {
        return icon;
    }
    public NetworkObject SendObject()
    {
        return Prefab;
    }
    public float SendweaponDamage()
    {
        return weaponData.damage;
    }
    public float SendweaponCooldown()
    {
        return weaponData.cooldown;
    }
    public float SendweaponRange()
    {
        return weaponData.attackDistance;
    }
    public AudioClip SendweaponAudio()
    {
        return weaponData.weaponsoundEffect;
    }
    public float SendweaponLayer()
    {
        return weaponData.attackLayer;
    }
    

    public float SendconsumableHeal()
    {
        return consumableData.healAmount;
    }
    public float SendconsumableStamina()
    {
        return consumableData.staminaAmount;
    }
    public float SendconsumableThirst()
    {
        return consumableData.thirstAmount;
    }
    public float SendconsumableHunger()
    {
        return consumableData.hungerAmount;
    }
    public float SendconsumableSpeed()
    {
        return consumableData.speedAmount;
    }
    public float SendlengthofConsume()
    {
        return consumableData.lengthOfConsume;
    }
    public float SendtickAmount()
    {
        return consumableData.tickAmount;
    }
    public float SendtickTime()
    {
        return consumableData.tickTime;
    }


    public bool SendtoolLightable()
    {
        return toolData.isLightable;
    }
    public float SendtoolChargeTick()
    {
        return toolData.toolChargeTick;
    }
    public toolType SendtoolType()
    {
        return toolData.toolType;
    }
    #endregion
}








