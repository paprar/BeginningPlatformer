using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    //��� ����
    TextmeshProGUI bestScoreUI;

    void Start()
    {
        bestScoreUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateScore();
    }
    //��� �Լ�

    //������ ȭ�鿡 �������� (UpdateScore)

    void UpdateScore()
    {
        bestScoreUI.text = $"Best Score : {PlyerPrefs.GetInt("BestScore", 0)}"; 
    }
}
