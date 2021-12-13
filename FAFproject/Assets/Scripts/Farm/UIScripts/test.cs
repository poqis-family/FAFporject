using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public void onclick()
   {
       Debug.Log("BTN Down");
        FarmDataManager._Instance.DayPass();
    }
}
