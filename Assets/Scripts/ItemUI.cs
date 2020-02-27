using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; set; }
    public int Amount { get; set; }

    private Image itemImage;
    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text amountText;
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    public void SetItem(Item item, int amount = 1)
    {
        this.Item = item;
        this.Amount = amount;
        //ui
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }

    }

}
