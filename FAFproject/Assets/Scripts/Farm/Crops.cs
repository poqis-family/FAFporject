using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Crops
{

    public static object GetCropStage(int cropID,int growDays)
    {
        if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 1;
        }
        
        else if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 2;
        }
        else if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 3;
        }
        else if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 4;
        }
        else if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days
                 || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 5;
        }
        if (growDays < FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days
            || FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage1Days==-1)
        {
            return 6;
        }

        return null;
    }

    public static object CheckCropMature(int cropID,int cropStage)
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

                break;
            case 2:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage2Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 3:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage3Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 4:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage4Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 5:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage5Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 6:
                if (FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).Stage6Days == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
        }
        return null;
    }

    public static string GetCropTileName(int cropID,int growDays)
    {
       int cropStage =(int)GetCropStage(cropID, growDays);
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
