using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 저장 3가지 기능
 * Save, Load, Delete
 * 
 */

public class SaveManager : MonoBehaviour
{
    //멤버 변수
    public int currentScore;
    int bestScore;

    void Start()
    {
        LoadData();
    }
    //멤버 함수
    void UpdateBestScore(int currentScore)
    {
        if (currentScore > bestScore)
        {
            SaveData(currentScore);
        }
    }

    private void Update()
    { // S 점수가 저장, L 저장된 점수를 불러오기, D 저장된 데이터를 지운다.

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
        Debug.Log("저장되었습니다.");
    }

    //Default의 의미는 무엇일까? 데이터가 없을때 가져올 데이터
    void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("BestScore",0); //100
        Debug.Log($"최고 점수: {bestScore}");
    }

    void DeleteData()
    {
        PlayerPrefs.DeleteKey("BestScore");
        Debug.Log("초기화 되었습니다.");
    }
}
