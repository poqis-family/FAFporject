using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;
using WJExcelDataClass;

public class Backpack : MonoBehaviour
{
    private int[] itemInfo = new int[2];
    DataManager dataManager = new DataManager();
    public int nowBackpackPage = 0;//当前玩家浏览的背包页
    // Start is called before the first frame update
    void Start()
    {
        //假模假样给点数据
        for (int i = 0; i < 4; i++)
        {   
            int[] Temp=new int[2];
            Temp[0] = 10001+i;  
            Temp[1] = 1;
            
            MainData.ItemListArr[0,i,0]=Temp[0];
            MainData.ItemListArr[0,i,1]=Temp[1];
        }
        
        RefreshItemUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 更新快捷背包界面的UI
    /// </summary>
    private void RefreshItemUI()
    {       
        dataManager.LoadAll();
        GameObject[] ItemIMGArr =FindChild.FindTheChildren(this.gameObject, "ItemIMG");

        
        for (int i = 0; i < MainData.ItemListArr.GetLength(1); i++)
        {
            if (MainData.ItemListArr[nowBackpackPage, i, 0] != 0)
            {
                Image img=ItemIMGArr[i].GetComponent<Image>();
                img.sprite = 
                    Resources.Load("UI/ItemIMG/" + dataManager.GetPropsItemByID(MainData.ItemListArr[nowBackpackPage,i,0]).img,typeof(Sprite))as Sprite;
            }
        }
        
    }
}
