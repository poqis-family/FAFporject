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
        MainData.dataManager.LoadAll();
        GameObject[] itemImgArr =FindChild.FindTheChildren(GameObject.Find("UI"), "ItemIMG");

        
        for (int i = 0; i < MainData.itemListArr.GetLength(1); i++)
        {
            if (MainData.itemListArr[BackpackData.nowBackpackPage, i, 0] != 0)
            {
                Image img=itemImgArr[i].GetComponent<Image>();
                img.sprite = 
                    Resources.Load("UI/ItemIMG/" + MainData.dataManager.GetPropsItemByID(MainData.itemListArr[BackpackData.nowBackpackPage,i,0]).img,typeof(Sprite))as Sprite;
            }
        }
    }
}
