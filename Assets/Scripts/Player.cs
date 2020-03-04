using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength { get => basicStrength; set => basicStrength = value; }
    public int BasicIntellect { get => basicIntellect; set => basicIntellect = value; }
    public int BasicAgility { get => basicAgility; set => basicAgility = value; }
    public int BasicStamina { get => basicStamina; set => basicStamina = value; }
    public int BasicDamage { get => basicDamage; set => basicDamage = value; }

    private int coinAmount = int.MaxValue;
    public int CoinAmount
    {
        get
        {
            return coinAmount;
        }
        set
        {
            coinAmount = value;
            coinText.text = coinAmount.ToString();
        }
    }
    private Text coinText;
    private void Start()
    {
        coinText = GameObject.Find("CoinsPanel/CoinsText").GetComponent<Text>();
        coinText.text = coinAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 17);
            Knapsack.Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Knapsack.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Chest.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CharacterPanel.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Vendor.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Forge.Instance.DisplaySwitch();
        }
    }

    public bool ConsumeCoin(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            coinText.text = coinAmount.ToString();
            return true;
        }
        return false;
    }

    public void EarnCoin(int amount)
    {
        this.coinAmount += amount;
        coinText.text = coinAmount.ToString();
    }
}
