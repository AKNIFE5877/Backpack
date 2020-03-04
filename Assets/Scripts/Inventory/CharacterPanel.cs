using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventory
{
    private static CharacterPanel _instance;
    public static CharacterPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharacterPanel>();
            }
            return _instance;
        }
    }

    private Text propertyText;
    private Player player;
    public override void Start()
    {
        base.Start();
        propertyText = transform.Find("PropertyPanel/Text").GetComponent<Text>();
        player = GameObject.FindObjectOfType<Player>();
        UpdatePropertyText();
        Hide();
    }

    public void UpdatePropertyText ()
    {
        int strength = 0;
        int intellect = 0;
        int agility = 0;
        int stamina = 0;
        int damage = 0;
        foreach (EquipmentSlot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                Item item = slot.transform.GetChild(0).GetComponent<ItemUI>().Item;
                if(item is Equipment)
                {
                    Equipment e=(Equipment)item;
                    strength += e.Strength;
                    intellect += e.Intellect;
                    agility += e.Agility;
                    stamina += e.Stamina;
                }
                else
                {
                    damage += ((Weapon)item).Damage;
                }
            }
        }
        strength += player.BasicStrength;
        intellect += player.BasicIntellect;
        agility += player.BasicAgility;
        stamina += player.BasicStamina;
        damage += player.BasicDamage;
        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4}\n", strength, intellect, agility, stamina, damage);
        propertyText.text = text;
    }

    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach (Slot slot in slotList)
        {
            EquipmentSlot equipmentSlot = (EquipmentSlot)slot;
            if (equipmentSlot.isRightItem(item))
            {
                if (equipmentSlot.transform.childCount > 0)
                {
                    ItemUI currentItemUI = equipmentSlot.transform.GetChild(0).GetComponent<ItemUI>();
                    exitItem = currentItemUI.Item;
                    currentItemUI.SetItem(item, 1);
                }
                else
                {
                    equipmentSlot.StoreItem(item);
                }
                break;
            }
        }
        if (exitItem != null)
        {
            Knapsack.Instance.StoreItem(exitItem);
        }
        UpdatePropertyText();
    }
    public void PutOff(Item item)
    {
        Knapsack.Instance.StoreItem(item);
        UpdatePropertyText();

    }
}
