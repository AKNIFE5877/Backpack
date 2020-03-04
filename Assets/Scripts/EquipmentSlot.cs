using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public EquipmentType equipmentType;
    public WeaponType weaponType;

    public override void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                Item currentItem = currentItemUI.Item;
                DestroyImmediate(currentItemUI.gameObject);
                transform.parent.SendMessage("PutOff", currentItem);
                InventoryManager.Instance.HideToolTip();
            }
        }
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        ////手上有东西
        //        //当前装备槽有装备
        //        //无装备
        ////手上没东西
        //        //当前装备槽有装备
        //        //无装备 不做处理
        bool isUpdateProperty = false;

        if (InventoryManager.Instance.IsPickedItem)
        {
            ItemUI pickedItem = InventoryManager.Instance.PickedItem;

            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                if (isRightItem(pickedItem.Item))
                {
                    InventoryManager.Instance.PickedItem.Exchange(currentItemUI);
                    isUpdateProperty = true;
                }
            }
            else
            {
                if (isRightItem(pickedItem.Item))
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.RemoveItem(1);
                    isUpdateProperty = true;
                }
            }
        }
        else
        {
            if (transform.childCount > 0)
            {
                ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                InventoryManager.Instance.PickupItem(currentItemUI.Item, currentItemUI.Amount);
                Destroy(currentItemUI.gameObject);
                isUpdateProperty = true;
            }
        }
        if (isUpdateProperty)
        {
            transform.parent.SendMessage("UpdatePropertyText");
        }
    }

    public bool isRightItem(Item item)
    {
        if ((item is Equipment && ((Equipment)(item)).EquipType == this.equipmentType) || (item is Weapon && ((Weapon)(item)).WeaponType == this.weaponType))
        {
            return true;
        }
        return false;
    }
}
