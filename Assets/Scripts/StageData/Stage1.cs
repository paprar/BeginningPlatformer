
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    private void Awake()
    {
        // ������ ����� �� ����� Ű �� �ʱ�ȭ
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save(); // ��������� ��� ����
        SaveData(3);

    }

    void SaveData(int score)
    {
        PlayerPrefs.SetInt("StageNumber", score);
        Debug.Log("����Ǿ����ϴ�.");
    }
}
