using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    public static BackpackController _Instance;
    public List<GameObject> itemImgArr=new List<GameObject>();
    private void Awake()
    {
        _Instance = this;
        getItemUIObjects();
    }

    void Start()
    {
        RefreshItemUI();
    }

    /// <summary>
    /// 更新快捷背包界面的UI
    /// </summary>
    public void RefreshItemUI()
    {
        for (int i = 0; i < FarmDataManager._Instance.mainData.itemListArr.GetLength(1); i++)
        {
            if (i < itemImgArr.Count)
            {
                Image img = itemImgArr[i].GetComponent<Image>();
                if (FarmDataManager._Instance.mainData.itemListArr[BackpackData.nowBackpackPage, i].ID != 0)
                {
                    img.sprite =
                        Resources.Load("UI/ItemIMG/" +
                                       FarmDataManager._Instance.dataManager.GetPropsItemByID(
                                               FarmDataManager._Instance.mainData.itemListArr[
                                                   BackpackData.nowBackpackPage,
                                                   i].ID)
                                           .img, typeof(Sprite)) as Sprite; //刷新图片

                    FindChild.FindTheChild(itemImgArr[i], ("Text")).GetComponent<Text>().text =
                        FarmDataManager._Instance.mainData.itemListArr[BackpackData.nowBackpackPage, i].Num
                            .ToString(); //刷新数量
                }
                else if (i < itemImgArr.Count)
                {
                    img.sprite = null;
                    FindChild.FindTheChild(itemImgArr[i], ("Text")).GetComponent<Text>().text = "0";
                }

                if (i == BackpackData.nowBackpackIndex) //刷新选中框
                {
                    FindChild.FindTheChild(itemImgArr[i], "Choose").SetActive(true);
                }
                else if (i < itemImgArr.Count)
                {
                    FindChild.FindTheChild(itemImgArr[i], "Choose").SetActive(false);
                }
            }
        }
    }

    private void getItemUIObjects()
    {
        for (int i = 0; i < FarmDataManager._Instance.mainData.itemListArr.GetLength(1); i++)
        {
            GameObject temp = FindChild.FindTheChild(GameObject.Find("UI"), i.ToString());
            if (temp != null)
            {
                itemImgArr.Add(temp);
            }
        }
    }
}
