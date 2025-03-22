using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    //파일이 저장될 경로
    string dirPath; //폴더 경로
    public string filePath; //파일 경로

    private void Start()
    {
        //경로를 지정해주기
        dirPath = Application.persistentDataPath;
        Debug.Log(dirPath);

        //경로에 저장하라
        SaveByPath();
    }
    void SaveByPath()
    {
        //폴더 이름/ 파일 이름 실행해라

        //1. 경로를 지정한다.
        string savePath = Path.Combine(dirPath, filePath);

        //2. 지정된 경로에 폴더를 만들어 준다.

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            string dataToStore = PlayerPrefs.GetInt("BestScore").ToString();

            // 현재 파일 실행중 변경 못하는 에러 막기 위해 사용
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error : {e}");
        }

        
    }

    void LoadByPath()
    {
        string loadPath = Path.Combine(dirPath, filePath);

        if(File.Exists(loadPath))
        {
            string dataToLoad;

            using (FileStream stream = new FileStream(loadPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }


            Debug.Log($"불러온 데이터: {dataToLoad}");
        }
    }

    void Delete()
    {
        string deletePath = Path.Combine(dirPath, filePath);

        if(File.Exists(deletePath))
        {
            File.Delete(deletePath);
        }
    }
}
