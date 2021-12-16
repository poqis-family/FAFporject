using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using DG.Tweening.Core.Easing;
using UnityEditor;
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

        FarmDataManager._Instance.mainData.plotDataDic.TryGetValue(pos, out PlotData temp);
        bool hadCrop;
        if (temp.cropID == 0) hadCrop = false;
        else hadCrop = true;
        if (hadCrop)
        {
            Debug.LogError("Tile had Crops");
            return false;
        }

        if ((!ReferenceEquals(Plowtile,null))&&!hadCrop)//既有耕地且无作物
        {
            return true;
        }
        return false;
    }
    
    public  void SowingSeed(Vector3Int pos,int cropID)
    {

        string tileName = PlotData.GetCropTileName(cropID, 0);
        GameObject cropObject= creatCropObject();
        changeCropObjectSprite(cropObject,tileName);
        changeCropObjectPosTo(cropObject, pos);
        setCropObjectCollider(cropObject,cropID);
        FarmDataManager._Instance.AddSowingPlotData(pos,cropID,cropObject.GetInstanceID());//加数据
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
                waterTM.SetTile(plot.Key, waterTileBase as TileBase);
            }
            else
            {
                waterTM.SetTile(plot.Key, null);
            }

            if (plot.Value.cropID != 0)
            {
                GameObject nowCropObject = EditorUtility.InstanceIDToObject(plot.Value.CropInstanceID) as GameObject;
                if (nowCropObject!=null)//如果已有实例
                {
                    changeCropObjectSprite(nowCropObject,plot.Value.cropTileName);
                    setCropObjectCollider(nowCropObject,plot.Value.cropID);
                }
                else
                {
                    GameObject cropObject= creatCropObject();
                    changeCropObjectSprite(cropObject,plot.Value.cropTileName);
                    changeCropObjectPosTo(cropObject,plot.Key);
                    setCropObjectCollider(cropObject,plot.Value.cropID);
                }
            }
        }
    }

    private GameObject creatCropObject()
    {
        GameObject cropObject = (GameObject)Resources.Load("Prefabs/Objects/CropObject");
        cropObject=Instantiate(cropObject);
        cropObject.transform.parent=GameObject.Find("CropsLayer").transform;
        return cropObject;
    }

    private void changeCropObjectSprite(GameObject cropObject,string tileName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("StardewValley/TileSheets21/Crops./");
        Sprite sprite=null;
        foreach (var VARIABLE in sprites)
        {
            if (VARIABLE.name==tileName)
            {
                sprite = VARIABLE;
            }
        }
        cropObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void changeCropObjectPosTo(GameObject cropObject, Vector3Int pos)
    {
        Vector3 posVector3 = pos;
        posVector3.x += 0.5f;
        posVector3.y += 0.5f;
        cropObject.transform.position = posVector3;
    }
    
    private void setCropObjectCollider(GameObject cropObject,int cropID)
    {
        int hasCollider = PlotData.GetCropHasCollider(cropID);
        if (hasCollider==0)
        {
            cropObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            cropObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
