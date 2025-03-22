using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    //��� ����
    TextMeshProUGUI bestScoreUI;

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
        bestScoreUI.text = $"Best Score : {PlayerPrefs.GetInt("BestScore", 0)}"; 
    }
}
