using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainData 
{
    
    public  int year;
    public  int month;
    public  int days;
    public  string name;
    public  int age;
    public  BackpackItemSubData[,] itemListArr = new BackpackItemSubData[3,12];
    public  Dictionary<Vector3Int, PlotData> plotDataDic = new Dictionary<Vector3Int, PlotData>();//已经变成临时存储的介质了，主要信息都存在下面那个ScenePlotDic了
    public  Dictionary<SceneEnum.Scenes,Dictionary<Vector3Int, PlotData>> ScenePlotDic=new Dictionary<SceneEnum.Scenes, Dictionary<Vector3Int, PlotData>>();
}
