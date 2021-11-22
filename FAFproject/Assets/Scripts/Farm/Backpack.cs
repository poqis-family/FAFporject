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

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] ItemIMGArr =FindChild.FindTheChildren(gameObject, "ItemIMG");
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();

        for (int i = 0; i < 4; i++)
        {   
            int[] Temp=new int[2];
            Temp[0] = 10001+i;  
            Temp[1] = 1;
            MainData.ItemList.Add(Temp);
        }

        int[] ItemTemp;
        for (int i = 0; i < MainData.ItemList.Count; i++)
        {
            ItemTemp = MainData.ItemList[i];
                Image img=ItemIMGArr[i].GetComponent<Image>();
                img.sprite = Resources.Load("UI/ItemIMG/" + dataManager.GetPropsItemByID(ItemTemp[0]).img,typeof(Sprite))as Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
