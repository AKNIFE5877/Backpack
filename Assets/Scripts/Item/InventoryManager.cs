using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryManager>();
            }
            return _instance;
        }

    }

    private List<Item> itemList;

    private ToolTip toolTip;
    private bool isToolTipShow = false;
    private Vector2 toolTipPositionOffset = new Vector2(10, -10);
    private Canvas canvas;


    private ItemUI pickedItem;
    public ItemUI PickedItem
    {
        get
        {
            return pickedItem;
        }
    }

    private bool isPickedItem = false;
    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
    }
    private void Awake()
    {
        ParseItemJson();
        
    }
    private void Start()
    {
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        pickedItem = GameObject.Find("PickedItem").GetComponent<ItemUI>();
        pickedItem.Hide();
    }


    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemjson = itemText.text;

        JsonData jd = JsonMapper.ToObject(itemjson);
        foreach (JsonData itemjd in jd)
        {
            Item item = null;
            int id = (int)itemjd["id"];
            string name = itemjd["name"].ToString();
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), itemjd["type"].ToString());
            Quality quality = (Quality)System.Enum.Parse(typeof(Quality), itemjd["quality"].ToString());
            string description = itemjd["description"].ToString();
            int capacity = (int)itemjd["capacity"];
            int buyPrice = (int)itemjd["buyPrice"];
            int sellPrice = (int)itemjd["sellPrice"];
            string spritePath = itemjd["sprite"].ToString();
            switch (type)
            {
                case ItemType.Consumable:
                    int hp = (int)itemjd["hp"];
                    int mp = (int)itemjd["mp"];
                    item = new Consumable(id, name, type, quality, description, capacity, buyPrice, sellPrice, spritePath, hp, mp);
                    break;
                case ItemType.Equipment:
                    int strength = (int)itemjd["strength"];
                    int intellect = (int)itemjd["intellect"];
                    int agility = (int)itemjd["agility"];
                    int stamina = (int)itemjd["stamina"];
                    EquipmentType equipmentType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), itemjd["equipmentType"].ToString());
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, spritePath, strength, intellect, agility, stamina, equipmentType);
                    break;
                case ItemType.Weapon:
                    int damage = (int)itemjd["damage"];
                    WeaponType weaponType = (WeaponType)System.Enum.Parse(typeof(WeaponType), itemjd["weaponType"].ToString());
                    item = new Weapon(id, name, type, quality, description, capacity, buyPrice, sellPrice, spritePath, damage, weaponType);
                    break;
                case ItemType.Material:
                    item = new Material(id, name, type, quality, description, capacity, buyPrice, sellPrice, spritePath);
                    break;
            }
            itemList.Add(item);
            //Debug.Log(item.ID.ToString() + "- " + item.Name.ToString() + "- " + item.Description.ToString() + "- " + item.ItemType.ToString() + "- " + item.Quality.ToString() + "- " + item.Sprite.ToString() + "- " + item.BuyPrice.ToString() + "- ");
        }
    }

    public Item GetItemById(int id)
    {
        foreach (Item item in itemList)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowToolTip(string content)
    {
        if (isPickedItem) return;
        isToolTipShow = true;
        toolTip.Show(content);
    }
    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();
    }


    public void PickupItem(Item item, int amount)
    {
        pickedItem.SetItem(item, amount);
        isPickedItem = true;
        PickedItem.Show();
        toolTip.Hide();
    }
 
    public void RemoveItem(int amount)
    {
        PickedItem.ReduceAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    private void Update()
    {
        if (isPickedItem)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            pickedItem.SetLocalPosition(position);
        }
        else
        if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            toolTip.SetLocalPosition(position + toolTipPositionOffset);
        }

        if (isPickedItem && Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)==false)
        {
            isPickedItem = false;
            pickedItem.Hide();
        }
    }

    public void SaveInventory()
    {
        Knapsack.Instance.SaveInventory();
        Chest.Instance.SaveInventory();
        CharacterPanel.Instance.SaveInventory();
        Forge.Instance.SaveInventory();
    }

    public void LoadInventory()
    {
        Knapsack.Instance.LoadInventory();
        Chest.Instance.LoadInventory();
        CharacterPanel.Instance.LoadInventory();
        Forge.Instance.LoadInventory(); 
    }
}
