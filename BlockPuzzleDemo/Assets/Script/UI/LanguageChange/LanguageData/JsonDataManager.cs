//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEngine.UI;

//public class JsonDataManager : MonoBehaviour
//{
//    public Text showText;

//    //文件名，在Assets/StreamingAssets目录下，如：myData.json。
//    public string fileName;

//    private LanguageData loadedData;

//    private void Start()
//    {
//        LoadJsonDateText();
//        showAllJsonData();
//    }

//    public void LoadJsonDateText()
//    {
//        //获取文件路径。
//        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

//        if (File.Exists(filePath))                          //如果该文件存在。
//        {
//            string dataAsJson = File.ReadAllText(filePath); //读取所有数据送到json格式的字符串里面。

//            //直接赋值。FromJson
//            loadedData = JsonUtility.FromJson<LanguageData>(dataAsJson);

//            //使用已有对象，添加值。FromJsonOverwrite
//            //loadedData = new MyData();
//            //JsonUtility.FromJsonOverwrite(dataAsJson, loadedData);

//            Debug.Log("Data loaded, dictionary contains: " + loadedData.datas.Length + " entries");
//        }
//        else
//            Debug.LogError("Cannot find file! Make sure file in \"Assets/StreamingAssets\" path. And file suffix should be \".json \"");
//    }

//    public void showAllJsonData()
//    {
//        showText.text = "";
//        for (int i = 0; i < loadedData.datas.Length; ++i)
//        {
//            for (int j = 0; j < loadedData.datas[i].ky.Length; j++)
//            {
//                showText.text += loadedData.datas[i].ky[j].key + "  :  " + loadedData.datas[i].ky[j].value + "\n";
//            }
//        }
//    }
//}
