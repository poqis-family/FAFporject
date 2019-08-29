using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MainData.year = 111;
        MainData.month = 111;
        MainData.name = "111";
        MainData.age = 111;
        MainData.LoadData();
        transform.Find("year").GetComponent<Text>().text = MainData.year.ToString();
        transform.Find("month").GetComponent<Text>().text = MainData.month.ToString();
        transform.Find("name").GetComponent<Text>().text = MainData.name.ToString();
        transform.Find("age").GetComponent<Text>().text = MainData.age.ToString();
        //  MainData.SaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
