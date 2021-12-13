using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Year").GetComponent<Text>().text = FarmDataManager._Instance.mainData.year.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
