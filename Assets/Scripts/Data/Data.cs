using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    void SaveData(int score)
    {
        PlayerPrefs.SetInt("StageNumber", score);
        Debug.Log("저장되었습니다.");
    }

}
