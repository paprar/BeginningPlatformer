using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        // ���� ���� �� SavePoint1 �� ����
        if (PlayerPrefs.HasKey("SavePoint1"))
        {
            PlayerPrefs.DeleteKey("SavePoint1");
            Debug.Log("���� ����: SavePoint1 �� ������!");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if(PlayerPrefs.HasKey("SavePoint1"))
            {
                PlayerPrefs.DeleteKey("SavePoint1");
                Debug.Log("�÷��� ��� ����: SavePoint1 �� ������!");
            }
        }
    }
}
