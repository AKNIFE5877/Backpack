using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }
    public int Amount { get; private set; }

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

    private float targetScale = 1;
    public float smoothing = 5;
    public Vector3 animationScale = new Vector3(1.2f, 1.2f, 1.2f);
    private void Update()
    {
        if (transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, (smoothing * Time.deltaTime) / Mathf.Abs(targetScale - transform.localScale.x));
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void SetItem(Item item, int amount = 1)
    {
        transform.localScale = animationScale;
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
        transform.localScale = animationScale;

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
    public void ReduceAmount(int amount = 1)
    {
        transform.localScale = animationScale;
        this.Amount -= amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }
    public void SetAmount(int amount)
    {
        transform.localScale = animationScale;

        this.Amount = amount;
        if (Item.Capacity > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    public void Exchange(ItemUI itemUI)
    {
        Item itemtemp = itemUI.Item;
        int amounttemp = itemUI.Amount;
        itemUI.SetItem(this.Item, this.Amount);
        this.SetItem(itemtemp, amounttemp);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
