using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : Inventory
{

    private static Forge _instance;
    public static Forge Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Forge>();
            }
            return _instance;
        }
    }

    private List<Formula> formulaList;

    private void Awake()
    {
        ParseFormulasJson();
    }
    void ParseFormulasJson()
    {
        formulaList = new List<Formula>();
        TextAsset formulasText = Resources.Load<TextAsset>("Formulas");
        string formulasjson = formulasText.text;

        JsonData jd = JsonMapper.ToObject(formulasjson);
        foreach (JsonData itemjd in jd)
        {
            int id1 = (int)itemjd["Item1ID"];
            int amount1 = (int)itemjd["Item1Amount"];
            int id2 = (int)itemjd["Item2ID"];
            int amount2 = (int)itemjd["Item2Amount"];
            int resID = (int)itemjd["ResID"];
            Formula formula = new Formula(id1, amount1, id2, amount2, resID);
            formulaList.Add(formula);
        }
    }
    public void ForgeItem()
    {
        List<int> haveMaterrialIDList = new List<int>();
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                ItemUI currentItemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                for (int i = 0; i < currentItemUI.Amount; i++)
                {
                    haveMaterrialIDList.Add(currentItemUI.Item.ID);
                }
            }
        }
        Formula matchedFormula = null;
        foreach (Formula formula in formulaList)
        {
            bool isMatch = formula.Match(haveMaterrialIDList);
            if (isMatch)
            {
                matchedFormula = formula; break;
            }
        }
        if (matchedFormula != null)
        {
            Knapsack.Instance.StoreItem(matchedFormula.ResID);
            //消耗材料
            foreach (int id in matchedFormula.NeedIdList)
            {
                foreach (Slot slot in slotList)
                {
                    if (slot.transform.childCount > 0)
                    {
                        ItemUI itemUI = slot.transform.GetChild(0).GetComponent<ItemUI>();
                        if (itemUI.Item.ID == id)
                        {
                            itemUI.ReduceAmount();
                            if (itemUI.Amount <= 0)
                            {
                                DestroyImmediate(itemUI.gameObject);
                            }
                            break;

                        }
                    }
                }
            }
        }
    }

}
