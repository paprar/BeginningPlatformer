using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    //멤버 변수
    TextmeshProGUI bestScoreUI;

    void Start()
    {
        bestScoreUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateScore();
    }
    //멤버 함수

    //점수를 화면에 갱신해줘 (UpdateScore)

    void UpdateScore()
    {
        bestScoreUI.text = $"Best Score : {PlyerPrefs.GetInt("BestScore", 0)}"; 
    }
}
