using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{

    public int Strength { get; set; }//力量
    public int Intellect { get; set; }//智力
    public int Agility { get; set; }//敏捷
    public int Stamina { get; set; }//体力
    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite, int strength, int intellect, int agility, int stamina, EquipmentType equipmentType)
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice,sprite)
    {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equipmentType;
    }

}
public enum EquipmentType
{
    Head,
    Neck,
    Chest,
    Ring,
    Leg,
    Bracer,
    Boots,
    Trinket,
    Shoulder,
    Belt

}
