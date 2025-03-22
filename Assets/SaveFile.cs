using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    //������ ����� ���
    string dirPath; //���� ���
    public string filePath; //���� ���

    private void Start()
    {
        //��θ� �������ֱ�
        dirPath = Application.persistentDataPath;
        Debug.Log(dirPath);

        //��ο� �����϶�
        SaveByPath();
    }
    void SaveByPath()
    {
        //���� �̸�/ ���� �̸� �����ض�

        //1. ��θ� �����Ѵ�.
        string savePath = Path.Combine(dirPath, filePath);

        //2. ������ ��ο� ������ ����� �ش�.

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            string dataToStore = PlayerPrefs.GetInt("BestScore").ToString();

            // ���� ���� ������ ���� ���ϴ� ���� ���� ���� ���
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


            Debug.Log($"�ҷ��� ������: {dataToLoad}");
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
