using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formula
{
    public int Item1ID { get; set; }
    public int Item1Amount { get; set; }
    public int Item2ID { get; set; }
    public int Item2Amount { get; set; }

    public int ResID { get; set; }
    private List<int> needIdlist = new List<int>();
    public List<int> NeedIdList
    {
        get
        {
            return needIdlist;
        }
    }

    public Formula(int item1ID, int item1Amount, int item2ID, int item2Amount, int resID)
    {
        this.Item1ID = item1ID;
        this.Item1Amount = item1Amount;
        this.Item2ID = item2ID;
        this.Item2Amount = item2Amount;
        this.ResID = resID;

        for (int i = 0; i < Item1Amount; i++)
        {
            needIdlist.Add(Item1ID);
        }
        for (int i = 0; i < Item2Amount; i++)
        {
            needIdlist.Add(Item2ID);
        }

    }
    public bool Match(List<int> idlist)
    {
        List<int> tempIDlist = new List<int>(idlist);
        foreach (int id in needIdlist)
        {
            bool isSuccess = tempIDlist.Remove(id);
            if (isSuccess == false)
            {
                return false;
            }
        }
        return true;
    }
}
