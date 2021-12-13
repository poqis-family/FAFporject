using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEditor.SceneManagement;
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

    // public TileBase ruleTile= tmpObj as TileBase;
   //public Object Obj;

   private void Awake()
   {
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
        FarmDataManager._Instance.AddPlowPlotData(pos);
        
        plowTM.SetTile(pos,basetemp as TileBase);
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
        FarmDataManager._Instance.AddWaterPlotData(pos);
        
        
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
    
    public  void SowingSeed(Vector3Int pos,int cropID)
    {
        string tileName = Crops.GetCropTileName(cropID, 0);
        var basetemp = Resources.Load("Tiles/StardewValley/Crops/" + tileName, typeof(TileBase));

        FarmDataManager._Instance.AddSowingPlotData(pos, cropID);
        cropsTM.SetTile(pos,basetemp as TileBase);
        return;
    }

    public void RefreshTilemap()
    {        
        var plowTileBase = Resources.Load("Tiles/Test/grounds/PlowTile", typeof(TileBase));
        var waterTileBase = Resources.Load("Tiles/Test/grounds/WateringTile", typeof(TileBase));

        foreach (var plot in FarmDataManager._Instance.mainData.plotDataDic)
        {
            if (plot.Value.isPlowed)
            {
                plowTM.SetTile(plot.Key, plowTileBase as TileBase);
            }
            else
            {
                plowTM.SetTile(plot.Key, null);
            }

            if (plot.Value.isWatered)
            {
                plowTM.SetTile(plot.Key, waterTileBase as TileBase);
            }
            else
            {
                plowTM.SetTile(plot.Key, null);
            }

            if (plot.Value.cropID != 0 && plot.Value.cropID != null)
            {
                string tileName = Crops.GetCropTileName(plot.Value.cropID, plot.Value.cropDays);
                var cropTileBase = Resources.Load("Tiles/StardewValley/Crops/" + tileName, typeof(TileBase));
                cropsTM.SetTile(plot.Key,cropTileBase as TileBase);
            }

        }
    }
}
