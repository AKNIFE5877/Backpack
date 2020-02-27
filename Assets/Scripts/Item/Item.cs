using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType ItemType { get; set; }
    public Quality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    public Item(int id,string name,ItemType itemType,Quality quality,string description,int capacity,int buyPrice,int sellPrice,string sprite)
    {
        this.ID = id;
        this.Name = name;
        this.ItemType = itemType;
        this.Quality = quality;
        this.Description = description;
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }
    public Item()
    {
        this.ID = -1;
    }

    public virtual string GetToolTipText()
    {
        return Name;
    }

}
public enum ItemType
{
    Consumable,
    Equipment,
    Weapon,
    Material
}
//品质
public enum Quality
{
    Common,
    Unmmon,
    Rare,
    Epic,
    Legendary,
    Artifact
}
