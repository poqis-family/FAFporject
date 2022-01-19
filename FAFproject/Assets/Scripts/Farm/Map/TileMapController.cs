using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using WJExcelDataClass;

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
    private GameObject BuildableCheckLayer;
    private Tilemap BuildableTM;
    public bool inBuildMode;

    private Sprite buildableSprite;
    private Sprite unbuildableSprite;
    
    bool noBuildBlock;
    
    public static TileMapController _Instance;

    // public TileBase ruleTile= tmpObj as TileBase;
   //public Object Obj;

   private void Update()
   {
       if (inBuildMode)
       {
           GameObject buildSprite = GameObject.Find("Map");
           Vector3 screenPos = Camera.main.WorldToScreenPoint(buildSprite.transform.position); //获取Screen坐标系Z的距离
           Vector3 mousePos = Input.mousePosition;
           mousePos.z = screenPos.z; //修改鼠标的屏幕坐标的Z的数值
           Vector3 mouseWorldPos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(mousePos)); //获取鼠标的世界坐标
           
           buildCheck(2,mouseWorldPos);
           if (Input.GetMouseButtonDown(0))
           {
               setBuilding(mouseWorldPos,2);
           }
       }
   }

   private void Awake()
   {
       buildableSprite = Resources.Load("Map/Buildings/Buildable", typeof(Sprite)) as Sprite; //刷新图片
       unbuildableSprite = Resources.Load("Map/Buildings/Unbuildable", typeof(Sprite)) as Sprite; //刷新图片      
       inBuildMode = true;
       
       _Instance = this;
       
       arableCheckLayer= FindChild.FindTheChild(GameObject.Find("Map"), "ArableCheckLayer");
       plowLayer= FindChild.FindTheChild(GameObject.Find("Map"), "PlowLayer");
       wateringLayer= FindChild.FindTheChild(GameObject.Find("Map"), "WateringLayer");
       cropsLayer= FindChild.FindTheChild(GameObject.Find("Map"), "CropsLayer");
       BuildableCheckLayer= FindChild.FindTheChild(GameObject.Find("Map"), "BuildableCheckLayer");

       arableTM = arableCheckLayer.GetComponent<Tilemap>();
       plowTM = plowLayer.GetComponent<Tilemap>();
       waterTM = wateringLayer.GetComponent<Tilemap>();
       cropsTM = cropsLayer.GetComponent<Tilemap>();
       BuildableTM = BuildableCheckLayer.GetComponent<Tilemap>();
   }
   
    public bool CheckArable(Vector3Int pos)
    {

        FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(pos, out PlotData plotData);
        if (plotData != null && plotData.isBlocked)
        {
            Debug.LogError("plot is blocked");
            return false;
        }
        
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
        FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(pos, out PlotData plotData);
        if (plotData != null && plotData.isBlocked)
        {
            Debug.LogError("plot is blocked");
            return false;
        }
        
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
        FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(pos, out PlotData plotData);
        if (plotData != null && plotData.isBlocked)
        {
            Debug.LogError("plot is blocked");
            return false;
        }
        
        TileBase Plowtile = plowTM.GetTile(pos);
        if (ReferenceEquals(Plowtile,null))//检查是不是没耕地
        {
            Debug.LogError("Tile didnt plowed");
            return false;
        }

        FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(pos, out PlotData temp);
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

    /// <summary>
    /// 刷新地图（后续这里要加房屋和动物和树们）
    /// </summary>
    public void RefreshTilemap()
    {


        if (FarmDataManager._Instance.mainData.ScenePlotDic.TryGetValue(FarmSceneManager._Instance.nowScene,
            out Dictionary<Vector3Int, PlotData> plotData) && plotData.Count != 0)
        {
            
            var plowTileBase = Resources.Load("Tiles/Test/grounds/PlowTile", typeof(TileBase));
            var waterTileBase = Resources.Load("Tiles/Test/grounds/WateringTile", typeof(TileBase));

            foreach (var plot in FarmDataManager._Instance.TryGetNowPlotDataDic())
            {
                if (plot.Value.IsPlowed)
                {
                    plowTM.SetTile(plot.Key, plowTileBase as TileBase);
                }
                else
                {
                    plowTM.SetTile(plot.Key, null);
                }

                if (plot.Value.IsWatered)
                {
                    waterTM.SetTile(plot.Key, waterTileBase as TileBase);
                }
                else
                {
                    waterTM.SetTile(plot.Key, null);
                }

                if (plot.Value.cropID != 0)
                {
                    GameObject nowCropObject =
                        EditorUtility.InstanceIDToObject(plot.Value.CropInstanceID) as GameObject;
                    if (nowCropObject != null) //如果已有实例
                    {
                        changeCropObjectSprite(nowCropObject, plot.Value.CropTileName);
                        setCropObjectCollider(nowCropObject, plot.Value.cropID);
                    }
                    else
                    {
                        GameObject cropObject = creatCropObject();
                        changeCropObjectSprite(cropObject, plot.Value.CropTileName);
                        changeCropObjectPosTo(cropObject, plot.Key);
                        setCropObjectCollider(cropObject, plot.Value.cropID);
                        plot.Value.CropInstanceID = cropObject.GetInstanceID();
                    }
                }
            }
        }

        
        if (FarmDataManager._Instance.mainData.SceneBuildingDic.TryGetValue(FarmSceneManager._Instance.nowScene,
            out Dictionary<int, BuildingData> buildingData) && buildingData.Count != 0)
        {
            foreach (var buildingDic in FarmDataManager._Instance.TryGetNowBuildingDataDic())
            {

                GameObject buildingObject =
                    EditorUtility.InstanceIDToObject(buildingDic.Value.buildingInstanceID) as GameObject;

                BuildingData buildingDataTemp = buildingDic.Value;
                if (buildingObject != null) //如果已有实例
                {
                    changeBuildingSprite(buildingObject, buildingDataTemp);
                    setBuildingCollider(buildingObject, buildingDataTemp);
                    changeBuildingPosTo(buildingObject, Vector3Int.FloorToInt(buildingDataTemp.pos), buildingDataTemp);
                }
                else
                {
                    buildingObject = creatBuildingObject();

                    GameObject cropObject = creatCropObject();
                    changeBuildingSprite(buildingObject, buildingDataTemp);
                    setBuildingCollider(buildingObject, buildingDataTemp);
                    changeBuildingPosTo(buildingObject, Vector3Int.FloorToInt(buildingDataTemp.pos), buildingDataTemp);
                    buildingDic.Value.buildingInstanceID = cropObject.GetInstanceID();
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

    public void changeCropObjectSprite(GameObject cropObject,string tileName)
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

    #region 建筑相关

    public void buildCheck(int buildingID,Vector3 mouseWorldPos)
    {
        //删除上一帧的建筑区域
        DelBuildCell();

        GameObject buildCell = (GameObject) Resources.Load("Prefabs/Objects/BuildCell");
        var buildingItem = FarmDataManager._Instance.dataManager.GetBuildingItemByID(buildingID);
        //绘制建筑区域大小
        noBuildBlock = true;
        for (int x = 0; x < buildingItem.size[0]; x++)
        {
            for (int y = 0; y < buildingItem.size[1]; y++)
            {
                buildCell = Instantiate(buildCell);
                buildCell.transform.parent = GameObject.Find("BuildingSub").transform;

                Vector3 cellpos;
                cellpos.x = mouseWorldPos.x + x + 0.5f;
                cellpos.y = mouseWorldPos.y - y + 0.5f;
                cellpos.z = mouseWorldPos.z;

                FarmDataManager._Instance.TryGetNowPlotDataDic().TryGetValue(Vector3Int.FloorToInt(cellpos), out PlotData plotData);
                
                if (BuildableTM.GetTile(Vector3Int.FloorToInt(cellpos)) == null ||
                    (plotData != null && plotData.cropID != 0) ||
                    (plotData != null && plotData.isBlocked)) //不可建造  或  plot上有数据且有植物  或  plot上存在阻挡
                {
                    buildCell.GetComponent<SpriteRenderer>().sprite = unbuildableSprite;
                    noBuildBlock = false;
                }
                else if (BuildableTM.GetTile(Vector3Int.FloorToInt(cellpos)).name == "buildable")
                {
                    buildCell.GetComponent<SpriteRenderer>().sprite = buildableSprite;
                }

                buildCell.transform.position = cellpos;
                buildCell.transform.name = "BuildCell";
            }
        }
    }

    public void setBuilding(Vector3 pos, int buildingID)
    {
        GameObject buildingObject = creatBuildingObject();
        
        pos.y -= FarmDataManager._Instance.dataManager.GetBuildingItemByID(buildingID).size[1] - 1;//建筑的pos需以左下角为标准点
        BuildingData buildingDataTemp= FarmDataManager._Instance.addBuildingData(Vector3Int.FloorToInt(pos),buildingID,buildingObject.GetInstanceID());
        
        changeBuildingSprite(buildingObject, buildingDataTemp); 
        changeBuildingPosTo(buildingObject, Vector3Int.FloorToInt(pos), buildingDataTemp);
        setBuildingCollider(buildingObject,buildingDataTemp);
        inBuildMode = false;
        DelBuildCell();
    }
    
    private GameObject creatBuildingObject()
    {
        GameObject cropObject = (GameObject)Resources.Load("Prefabs/Objects/BuildingObject");
        cropObject=Instantiate(cropObject);
        cropObject.transform.parent=GameObject.Find("BuildingSub").transform;
        return cropObject;
    }
    public void changeBuildingSprite(GameObject targetBuildingObject,BuildingData buildingDataTemp)
    {
        BuildingItem buildingItem = FarmDataManager._Instance.dataManager.GetBuildingItemByID((int)buildingDataTemp.buildingType);
        string SpriteName = buildingItem.SpriteName[buildingDataTemp.nowLevel];
        Sprite sprites = Resources.Load("Map/Buildings/"+SpriteName,typeof(Sprite))as Sprite;
        targetBuildingObject.GetComponent<SpriteRenderer>().sprite = sprites;
    }
    
    private void changeBuildingPosTo(GameObject buildingObject, Vector3Int pos,BuildingData buildingDataTemp)
    {
        Vector3 posVector3 = pos;
        buildingObject.transform.position = posVector3;
    }

    private void setBuildingCollider(GameObject buildingObject,BuildingData buildingDataTemp)
    {
        buildingObject.GetComponent<BoxCollider2D>().size=new Vector2(
            FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).size[0],
            FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).size[1]);
        buildingObject.GetComponent<BoxCollider2D>().offset=new Vector2(
            (float)FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).size[0] / 2,
            (float)FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).size[1] / 2);
        
         buildingObject.GetComponent<CircleCollider2D>().radius = 0.34f;
         buildingObject.GetComponent<CircleCollider2D>().offset=new Vector2(
             FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).entranceOffset[0],
             FarmDataManager._Instance.dataManager.GetBuildingItemByID((int) buildingDataTemp.buildingType).entranceOffset[1]);
    }
    
    private void DelBuildCell()
    {
        //删除上一帧的建筑区域
        if (FindChild.FindTheChildren(GameObject.Find("BuildingSub"), "BuildCell") != null)
        {
            foreach (var VARIABLE in FindChild.FindTheChildren(GameObject.Find("BuildingSub"), "BuildCell"))
            {
                Destroy(VARIABLE);
            }
        }
    }

    #endregion

}
