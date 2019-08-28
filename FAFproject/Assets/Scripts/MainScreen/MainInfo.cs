using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        MainData.year = 191;
        MainData.month = 4;
        MainData.name = "Pochi";
        MainData.age = 8;
        transform.Find("Year").GetComponent<Text>().text = MainData.year.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
