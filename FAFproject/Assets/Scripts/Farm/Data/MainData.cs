using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainData 
{
    public int year;
    public int month;
    public int days;
    public string name;
    public int age;
    public BackpackItemSubData[,] itemListArr = new BackpackItemSubData[3,12];
    public Dictionary<SceneEnum.Scenes,Dictionary<Vector3Int, PlotData>> ScenePlotDic=new Dictionary<SceneEnum.Scenes, Dictionary<Vector3Int, PlotData>>();
    public Dictionary<SceneEnum.Scenes,Dictionary<int, BuildingData>> SceneBuildingDic=new Dictionary<SceneEnum.Scenes, Dictionary<int, BuildingData>>();
    public Dictionary<int, AnimalData> AnimalDataDic = new Dictionary<int, AnimalData>();
}
