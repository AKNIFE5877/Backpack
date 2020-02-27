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
    private Canvas canvas;

    private Vector2 toolTipPositionOffset = new Vector2(10, -10);

    private void Start()
    {
        ParseItemJson();
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
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
                    int strength= (int)itemjd["strength"];
                    int intellect = (int)itemjd["intellect"];
                    int agility = (int)itemjd["agility"];
                    int stamina = (int)itemjd["stamina"];
                    EquipmentType equipmentType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), itemjd["equipmentType"].ToString());
                    item = new Equipment(id, name, type, quality, description, capacity, buyPrice, sellPrice, spritePath, strength, intellect, agility, stamina, equipmentType);
                    break;
                case ItemType.Weapon:
                    break;
                case ItemType.Material:
                    break;
                default:
                    break;
            }
            itemList.Add(item);
            Debug.Log(item.ID.ToString() + "- " + item.Name.ToString() + "- " + item.Description.ToString() + "- " + item.ItemType.ToString() + "- " + item.Quality.ToString() + "- " + item.Sprite.ToString() + "- " + item.BuyPrice.ToString() + "- ");
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
        isToolTipShow = true;
        toolTip.Show(content);
    }
    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.Hide();
    }
    private void Update()
    {
        if (isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null,out position);
            toolTip.SetLocalPosition(position+ toolTipPositionOffset);
        } 
    }
}
