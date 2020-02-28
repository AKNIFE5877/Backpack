using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject itemPrefab;

    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab, transform) as GameObject;
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }
    public int GetItemID()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }
    public ItemType GetItemType()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ItemType;
    }
    public bool IsFilled()
    {
        ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
        return itemUI.Amount >= itemUI.Item.Capacity;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ////自身是空 1、IsPickedItem==true 放在这个位置
        //            //按下ctrl   放下一个
        //            //没有按下ctrl  全部放下
        //         //2、IsPickedItem==false 不做任何处理
        ////自身不是空
        //         //1、IsPickedItem==true
        //            //自身的id==pickeditem.id
        //                    //按下ctrl   放置当前鼠标上的一个
        //                    //没有按下ctrl  放置当前鼠标所有
        //            //自身的id!=pickeditem.id  pickeditem跟当前物品交换 
        //        //2、IsPickedItem==false
        //            //ctrl按下 取得当前物品的一半
        //            //ctrl没有按下  把当前物品槽里面的物品放到鼠标上
        if (transform.childCount > 0)
        {
            ItemUI currentItem = transform.GetChild(0).GetComponent<ItemUI>();
            if (InventoryManager.Instance.IsPickedItem == false)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int amountPicked = (currentItem.Amount + 1) / 2;
                    InventoryManager.Instance.PickupItem(currentItem.Item, amountPicked);
                    int amountRemained = currentItem.Amount - amountPicked;
                    if (amountRemained <= 0)
                    {
                        Destroy(currentItem.gameObject);
                    }
                    else
                    {
                        currentItem.SetAmount(amountRemained);
                    }
                }
                else
                {
                    InventoryManager.Instance.PickupItem(currentItem.Item, currentItem.Amount);
                    Destroy(currentItem.gameObject);
                }
            }
            else
            {
                if (currentItem.Item.ID == InventoryManager.Instance.PickedItem.Item.ID)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (currentItem.Item.Capacity > currentItem.Amount)
                        {
                            currentItem.AddAmount();
                            InventoryManager.Instance.RemoveItem(1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (currentItem.Item.Capacity > currentItem.Amount)
                        {
                            int amountRemain = currentItem.Item.Capacity - currentItem.Amount;
                            if (amountRemain >= InventoryManager.Instance.PickedItem.Amount)
                            {
                                currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                            }
                            else
                            {
                                currentItem.SetAmount(currentItem.Amount + amountRemain);
                                InventoryManager.Instance.RemoveItem(amountRemain);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    Item item = currentItem.Item;
                    int amount = currentItem.Amount;
                    currentItem.SetItem(InventoryManager.Instance.PickedItem.Item, InventoryManager.Instance.PickedItem.Amount);
                    InventoryManager.Instance.PickedItem.SetItem(item, amount);
                }
            }
        }
        else //空格子
        {
            if(InventoryManager.Instance.IsPickedItem == true)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.RemoveItem(1);
                }
                else
                {
                    for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);

                }
            }
            else
            {
                return;
            }
        }

    }
}
