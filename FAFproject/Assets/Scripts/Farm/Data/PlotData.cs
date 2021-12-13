using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotData
{
    public  int sceneEnum; //地块所在的场景
    public bool isPlowed;//是否锄过地
    public bool isWatered;//是否浇过水
    public int cropID;//所种植的作物ID
    public int cropDays;//作物生长的天数
    
    public object GetCropStage()
    {
        if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 1;
        }
        
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days==-1)
        {
            return 2;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days==-1)
        {
            return 3;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days==-1)
        {
            return 4;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days==-1)
        {
            return 5;
        }
        if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days==-1)
        {
            return 6;
        }

        return null;
    }
    
    public static object GetCropStage(int cropID,int cropDays)
    {
        if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 1;
        }
        
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days==-1)
        {
            return 2;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days==-1)
        {
            return 3;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days==-1)
        {
            return 4;
        }
        else if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days==-1)
        {
            return 5;
        }
        if (cropDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days==-1)
        {
            return 6;
        }

        return null;
    }
    public object CheckCropMature(int cropStage)
    {
        switch (cropStage)
        {
            case 1:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 4:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 5:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 6:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return null;
    }
    public string GetCropTileName()
    {
       int cropStage =(int)GetCropStage();
       List<string> tileList = new List<string>();
       switch (cropStage)
       {
           case 1:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Tile;
               break;
           case 2:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Tile;
               break;
           case 3:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Tile;
               break;
           case 4:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Tile;
               break;
           case 5:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Tile;
               break;
           case 6:
               tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Tile;
               break;
       }
       int randomIndex =Random.Range(0,tileList.Count);
       return tileList[randomIndex];
    }
    public static string GetCropTileName(int cropID,int growDays)
    {
        int cropStage =(int)GetCropStage(cropID,growDays);
        List<string> tileList = new List<string>();
        switch (cropStage)
        {
            case 1:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Tile;
                break;
            case 2:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Tile;
                break;
            case 3:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Tile;
                break;
            case 4:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Tile;
                break;
            case 5:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Tile;
                break;
            case 6:
                tileList= FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Tile;
                break;
        }
        int randomIndex =Random.Range(0,tileList.Count);
        return tileList[randomIndex];
    }

}
