using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Excel;
using ExcelDataReader;

public class ConfigReader : MonoBehaviour
{
    public void GetData()
    {

        string[] files = Directory.GetFiles("Assets/Config", "*.xlsx");
        List<string> codeList = new List<string>();
        Dictionary<string, List<ConfigData[]>> dataDict = new Dictionary<string, List<ConfigData[]>>();
        for (int i = 0; i < files.Length; ++i)
        {
            //打开excel
            string file = files[i];
            FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (!excelReader.IsValid)
            {
                Debug.Log("invalid excel " + file);
                Console.WriteLine("invalid excel " + file);
                continue;
            }
        }
    }
}
