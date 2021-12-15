using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeEnum : MonoBehaviour
{
    public enum MainItemType
    {
        tools=1, //工具
        seeds=2, //种子
        foods=3,//食物
        muck=4,//肥料
        equipment=5,//装备
        material=6,//素材
        machine=7,//机器
        furniture=8,//家具
    }
    public enum  ToolsType
    {
        hoe=1,//锄头
        pick=2,//镐
        axe=3,//斧
        wateringCan=4,//浇喷壶
        fishingRod=5,//鱼竿
    }
}
