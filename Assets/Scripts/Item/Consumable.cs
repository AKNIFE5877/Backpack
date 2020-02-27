using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int HP { get; set; }
    public int MP { get; set; }
    public Consumable(int id, string name, ItemType itemType, Quality quality, string description, int capacity, int buyPrice, int sellPrice, string sprite, int hp,int mp) 
        : base(id, name, itemType, quality, description, capacity, buyPrice, sellPrice, sprite)
    {
        this.HP = hp;
        this.MP = mp;
    }
}
