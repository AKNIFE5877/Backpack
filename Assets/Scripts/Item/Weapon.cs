using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public int Damage { get; set; }
    public WeaponType WeaponType { get; set; }


    public Weapon(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite, int damage,WeaponType weaponType)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        this.Damage = damage;
        this.WeaponType = weaponType;
    }
}
public enum WeaponType
{
    None,
    MainHand,
    OffHand
}
