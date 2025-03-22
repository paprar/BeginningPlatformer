using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� 3���� ���
 * Save, Load, Delete
 * 
 */

public class SaveManager : MonoBehaviour
{
    //��� ����
    public int currentScore;
    int bestScore;

    void Start()
    {
        LoadData();
    }
    //��� �Լ�
    void UpdateBestScore(int currentScore)
    {
        if (currentScore > bestScore)
        {
            SaveData(currentScore);
        }
    }

    private void Update()
    { // S ������ ����, L ����� ������ �ҷ�����, D ����� �����͸� �����.

        if(Input.GetKeyDown(KeyCode.S))
        {
            UpdateBestScore(currentScore);
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            DeleteData();
        }
    }

    void SaveData(int score)
    {
        PlayerPrefs.SetInt("BestScore",score);
        Debug.Log("����Ǿ����ϴ�.");
    }

    //Default�� �ǹ̴� �����ϱ�? �����Ͱ� ������ ������ ������
    void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("BestScore",0); //100
        Debug.Log($"�ְ� ����: {bestScore}");
    }

    void DeleteData()
    {
        PlayerPrefs.DeleteKey("BestScore");
        Debug.Log("�ʱ�ȭ �Ǿ����ϴ�.");
    }
}
