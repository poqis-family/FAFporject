using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using WJExcelDataClass;

public static class BackpackData 
{
    private static int _nowBackpackPage = 0;//当前玩家浏览的背包页
    public static int nowBackpackPage//当前玩家浏览的背包页
    {
        get
        {
            return _nowBackpackPage;
        }
        set
        {
            _nowBackpackPage = value;
            RefreshItemID();
        }
    }
    private static int _nowBackpackIndex = 0;//当前玩家选中的Item格子
    public static int nowBackpackIndex//当前玩家选中的Item格子
    {
        get
        {
            return _nowBackpackIndex;
        }
        set
        {
            _nowBackpackIndex = value;
            RefreshItemID();
        }
    }
    public static int nowItemID = 0;//当前玩家选中的Item的ID

    public static void RefreshItemID()//刷新当前玩家选中的Item的ID
    {
        nowItemID = FarmDataManager._Instance.mainData.itemListArr[nowBackpackPage, nowBackpackIndex].ID;
    }
}
