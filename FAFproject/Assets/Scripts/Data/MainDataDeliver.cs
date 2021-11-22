﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MainDataDeliver
{
    public  int year;
    public  int month;
    public  string name;
    public  int age;
    public  List<int[]> ItemList = new List<int[]>();
    public MainDataDeliver()
    {
        year = MainData.year;
        month = MainData.month;
        name = MainData.name;
        age = MainData.age;
        ItemList = MainData.ItemList;
        
    }
}