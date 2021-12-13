using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainData 
{

    //每增加一条需保存的值都要在MainDataDeliver中添加，并在MainData.LoadData中添加相应加载
    public  int year;
    public  int month;
    public  int days;
    public  string name;
    public  int age;
    public  int[,,] itemListArr = new int[3,12,2];
    public  Dictionary<Vector3Int, PlotData> plotDataDic = new Dictionary<Vector3Int, PlotData>();
    

}
