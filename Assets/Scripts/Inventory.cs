using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slotList;

    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("要存储的物品的id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("背包已满");
                return false;
            }
            else
            {
                slot.StoreItem(item);
            }
        }
        else
        {
            Slot slot = FindSameIDSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot == null)
                {
                    Debug.Log("背包已满");
                    return false;
                }
                else
                {
                    emptySlot.StoreItem(item);
                }
            }
        }
        return true;
    }


    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    private Slot FindSameIDSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemID() == item.ID &&slot.IsFilled()==false)
            {
                return slot;
            }
        }
        return null;
    }
}
