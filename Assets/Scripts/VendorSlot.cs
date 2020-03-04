using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !InventoryManager.Instance.IsPickedItem)
        {
            if (transform.childCount > 0)
            {
                Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
                Vendor.Instance.BuyItem(item);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.IsPickedItem)
        {
            Vendor.Instance.SellItem();
        }
    }
}
