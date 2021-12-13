using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    void Start()
    {
        RefreshItemUI();
    }
    
    /// <summary>
    /// 更新快捷背包界面的UI
    /// </summary>
    public void RefreshItemUI()
    {
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();
        GameObject[] itemImgArr =FindChild.FindTheChildren(GameObject.Find("UI"), "ItemIMG");

        
        for (int i = 0; i < FarmDataManager._Instance.mainData.itemListArr.GetLength(1); i++)
        {
            if (FarmDataManager._Instance.mainData.itemListArr[BackpackData.nowBackpackPage, i, 0] != 0)
            {
                Image img=itemImgArr[i].GetComponent<Image>();
                img.sprite = 
                    Resources.Load("UI/ItemIMG/" + dataManager.GetPropsItemByID(FarmDataManager._Instance.mainData.itemListArr[BackpackData.nowBackpackPage,i,0]).img,typeof(Sprite))as Sprite;

                FindChild.FindTheChild(itemImgArr[i], ("Text")).GetComponent<Text>().text =
                    FarmDataManager._Instance.mainData.itemListArr[BackpackData.nowBackpackPage, i, 1].ToString();
            }
        }
    }
}
