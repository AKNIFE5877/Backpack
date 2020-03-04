using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    protected Slot[] slotList;

    private float targetAlpha = 1;
    public float smoothing = 5;
    private CanvasGroup canvasGroup;
    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, (smoothing * Time.deltaTime) / Mathf.Abs(targetAlpha - canvasGroup.alpha));
        }
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
            if (slot.transform.childCount >= 1 && slot.GetItemID() == item.ID && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    public void Show()
    {
        canvasGroup.blocksRaycasts = true;
        targetAlpha = 1;
    }
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        targetAlpha = 0;
    }
    public void DisplaySwitch()
    {
        if (targetAlpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    //save
    public void SaveInventory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                sb.Append(itemUI.Item.ID + "," + itemUI.Amount + "-");
            }
            else
            {
                sb.Append("0-");
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, sb.ToString());
    }
    public void LoadInventory()
    {
        if (!PlayerPrefs.HasKey(this.gameObject.name))
        {
            return;
        }
        string str = PlayerPrefs.GetString(this.gameObject.name);
        string[] itemArray = str.Split('-');
        for (int i = 0; i < itemArray.Length-1; i++)
        {
            string itemstr = itemArray[i];
            if (itemstr != "0")
            {
                string[] temp = itemstr.Split(',');
                int id = int.Parse(temp[0]);
                int amount = int.Parse(temp[1]);
                Item item = InventoryManager.Instance.GetItemById(id);
                for (int j = 0; j < amount; j++)
                {
                    slotList[i].StoreItem(item);
                }
            }
        }
    }
}
