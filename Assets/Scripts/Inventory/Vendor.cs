using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : Inventory
{

    private static Vendor _instance;
    public static Vendor Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Vendor>();
            }
            return _instance;
        }
    }
    public int[] itemIdArray;

    private Player player;

    public override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.FindObjectOfType<Player>();
        Hide();
    }
    public void InitShop()
    {
        foreach (int itemid in itemIdArray)
        {
            StoreItem(itemid);
        }
    }

    public void BuyItem(Item item)
    {
        bool isSuccess = player.ConsumeCoin(item.BuyPrice);
        if (isSuccess)
        {
            Knapsack.Instance.StoreItem(item);
        }
    }
    public void SellItem()
    {
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            sellAmount = 1;
        }
        else
        {
            sellAmount = InventoryManager.Instance.PickedItem.Amount;
        }
        int coinAmount = InventoryManager.Instance.PickedItem.Item.SellPrice * sellAmount;
        InventoryManager.Instance.RemoveItem(sellAmount);
        player.EarnCoin(coinAmount);
    }
}
