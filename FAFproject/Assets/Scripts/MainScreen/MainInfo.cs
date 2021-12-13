using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WJExcelDataManager;

using WJExcelDataClass;

public class MainInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //     FarmDataManager temp2 = new FarmDataManager();
        FarmDataManager._Instance.mainData.year = 111;
        FarmDataManager._Instance.mainData.month = 111;
        FarmDataManager._Instance.mainData.name = "111";
        FarmDataManager._Instance.mainData.age = 111;
        FarmDataManager._Instance.LoadData();
        //DataManager dataManager = new DataManager();
      // dataManager.LoadAll();
      // Sheet1Item sheet1Item= dataManager.GetSheet1ItemByID(10006);
     //  MainData.name =sheet1Item.name;
        transform.Find("year").GetComponent<Text>().text = FarmDataManager._Instance.mainData.year.ToString();
        transform.Find("month").GetComponent<Text>().text = FarmDataManager._Instance.mainData.month.ToString();
        transform.Find("name").GetComponent<Text>().text = FarmDataManager._Instance.mainData.name.ToString();
        transform.Find("age").GetComponent<Text>().text = FarmDataManager._Instance.mainData.age.ToString();
        FarmDataManager._Instance.mainData.year = 122211;
        FarmDataManager._Instance.mainData.month = 1222211;
        FarmDataManager._Instance.mainData.name = "112221";
        FarmDataManager._Instance.mainData.age = 2;
        FarmDataManager._Instance.SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
