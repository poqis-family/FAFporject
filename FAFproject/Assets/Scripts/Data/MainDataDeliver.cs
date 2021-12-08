using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MainDataDeliver
{
    public  int year;
    public  int month;
    public  string name;
    public  int age;
    public  int[,,] ItemListArr = new int[3,12,2];
    public  List<Vector3Int> plowedData; //在某格子是否耕过田 的List，List内都是耕过的田的坐标
    public  List<Vector3Int> wateredData;   //在某格子是否浇过水 的List，List内都是浇过的水的坐标
    public  Dictionary<Vector3Int, int[]> cropsData;  //在某格子是否种过地，种的什么，长了几天  的List，List内都是作物们的坐标与状态
    public MainDataDeliver()
    {
        year = MainData.year;
        month = MainData.month;
        name = MainData.name;
        age = MainData.age; 
        ItemListArr = MainData.itemListArr;
        plowedData = MainData.plowedData; //在某格子是否耕过田 的List，List内都是耕过的田的坐标
        wateredData = MainData.wateredData;   //在某格子是否浇过水 的List，List内都是浇过的水的坐标
        cropsData = MainData.cropsData; //在某格子是否种过地，种的什么，长了几天  的List，List内都是作物们的坐标与状态

    }
}