using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WJExcelDataClass;

public class Backpack : MonoBehaviour
{
    private int[] itemInfo = new int[2];
    private List<int[]> ItemList = new List<int[]>();
    // Start is called before the first frame update
    void Start()
    {
        DataManager dataManager = new DataManager();
        dataManager.LoadAll();
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
