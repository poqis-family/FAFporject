using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;
using WJExcelDataClass;
using Random = UnityEngine.Random;

public class TileMapController : MonoBehaviour
{
    private GameObject arableCheckLayer;
    private Tilemap arableTM;
    private GameObject plowLayer;
    private Tilemap plowTM;
    private GameObject wateringLayer;
    private Tilemap waterTM;
    private GameObject cropsLayer;
    private Tilemap cropsTM;
    public static TileMapController _Instance;
    public TileBase basetemp;

    private DataManager dataManager;
   // public TileBase ruleTile= tmpObj as TileBase;
   //public Object Obj;

   private void Awake()
   {
       dataManager = new DataManager();
       dataManager.LoadAll();
       _Instance = this;
       
       
       arableCheckLayer= FindChild.FindTheChild(GameObject.Find("Map"), "ArableCheckLayer");
       plowLayer= FindChild.FindTheChild(GameObject.Find("Map"), "PlowLayer");
       wateringLayer= FindChild.FindTheChild(GameObject.Find("Map"), "WateringLayer");
       cropsLayer= FindChild.FindTheChild(GameObject.Find("Map"), "CropsLayer");
       arableTM = arableCheckLayer.GetComponent<Tilemap>();
       plowTM = plowLayer.GetComponent<Tilemap>();
       waterTM = wateringLayer.GetComponent<Tilemap>();
       cropsTM = cropsLayer.GetComponent<Tilemap>();
   }
   
    public bool CheckArable(Vector3Int pos)
    {
        TileBase arableTile = arableTM.GetTile(pos);
        TileBase plowTile = plowTM.GetTile(pos);
        if (ReferenceEquals(arableTile,null))
        {
            Debug.LogError("arableMapTile is null");
            return false;
        }
        if (!ReferenceEquals(plowTile,null))
        {
            Debug.Log("Tilemap is plowed");
            return false;
        }
        if (arableTile.name=="arable")
        {
            Debug.Log("Tilemap is arable");
            return true;
        }
        if (arableTile.name=="Unarable")
        {
            Debug.LogError("Tilemap is Unarable");
            return false;
        }

        
        return false;
    }

    public void PlowLand(Vector3Int pos)
    {
        var basetemp = Resources.Load("Tiles/Test/grounds/PlowTile", typeof(TileBase));
        MainData.plowedData.Add(pos);//向MainData中添加了锄地的地块的信息
        plowTM.SetTile(pos,basetemp as TileBase);
    }

    public void test(Vector3Int pos)
    {
        var basetemp = Resources.Load("Objects/3", typeof(GameObject));
        cropsTM.SetTile(pos, basetemp as TileBase);
    }

    public bool CheckWaterable(Vector3Int pos)
    {
        
        TileBase plowTile = plowTM.GetTile(pos);
        if (ReferenceEquals(plowTile,null))
        {
            Debug.LogError("Tile didnt plowed");
            return false;
        }
        if (!ReferenceEquals(plowTile,null))
        {
            return true;
        }
        return false;
    }

    public void WateringLand(Vector3Int pos)
    {
        var basetemp = Resources.Load("Tiles/Test/grounds/WateringTile", typeof(TileBase));
        MainData.wateredData.Add(pos);//向MainData中添加了浇水的地块的信息
        waterTM.SetTile(pos,basetemp as TileBase);
    }

    public bool CheckSownable(Vector3Int pos)
    {
        TileBase Plowtile = plowTM.GetTile(pos);
        if (ReferenceEquals(Plowtile,null))//检查是不是没耕地
        {
            Debug.LogError("Tile didnt plowed");
            return false;
        }
        

        TileBase cropsTile = cropsTM.GetTile(pos);
        if (!ReferenceEquals(cropsTile,null)) //检查是不是有作物
        {
                Debug.LogError("Tile had Crops");
                return false;
        }
        
        if ((!ReferenceEquals(Plowtile,null))&&ReferenceEquals(cropsTile,null))//既有耕地且无作物
        {
            return true;
        }
        return false;
    }
    
    public void SowingSeed(Vector3Int pos)
    {
        int cropID = 10001;
        int randomIndex =Random.Range(0, dataManager.GetCropsItemByID(10001).Stage1Tile.Count);
        var basetemp = Resources.Load("Tiles/StardewValley/Crops/"+dataManager.GetCropsItemByID(cropID).Stage1Tile[randomIndex], typeof(TileBase));

        int[] temp = {cropID, 0};
        MainData.cropsData.Add(pos,temp);//向MainData中添加了当前坐标与种子与种子天数的信息
        
        cropsTM.SetTile(pos,basetemp as TileBase);
        return;
    }
}
