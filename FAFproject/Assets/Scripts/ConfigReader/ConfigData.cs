using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using System.Data;
using ExcelDataReader;
using ICSharpCode;
using Excel;

class ConfigData
{
    public string Type;
    public string Name;
    public string Data;

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
                string[] types = null;
        string[] names = null;
        List<ConfigData[]> dataList = new List<ConfigData[]>();
        int index = 1;

        //开始读取
        while (excelReader.Read())
        {
            //这里读取的是每一行的数据
            string[] datas = new string[excelreader];
            for (int j = 0; j < excelReader.FieldCount; ++j)
            {
                datas[j] = excelReader.GetString(j);
            }

            //空行不处理
            if (datas.Length == 0 || string.IsNullOrEmpty(datas[0]))
            {
                ++index;
                continue;
            }

            //第三行表示类型
            if (index == 1) types = datas;
            //第四行表示变量名
            else if (index == 2) names = datas;
            //后面的表示数据
            else if (index > 2)
            {
                //把读取的数据和数据类型,名称保存起来,后面用来动态生成类
                List<ConfigData> configDataList = new List<ConfigData>();
                for (int j = 0; j < datas.Length; ++j)
                {
                    ConfigData data = new ConfigData();
                    data.Type = types[j];
                    data.Name = names[j];
                    data.Data = datas[j];
                    if (string.IsNullOrEmpty(data.Type) || string.IsNullOrEmpty(data.Data))
                        continue;
                    configDataList.Add(data);
                }
                dataList.Add(configDataList.ToArray());
            }
            ++index;
        }
    }
}
