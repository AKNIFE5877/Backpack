﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 3 );
            Knapsack.Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Knapsack.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Chest.Instance.DisplaySwitch();
        }
    }
}
