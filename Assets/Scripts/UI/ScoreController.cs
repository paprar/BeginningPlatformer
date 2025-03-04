using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    private int totalScore;

    public void GainScore(int amount)
    {
        scoreText.text = "" + totalScore; // ���� ����
        totalScore += amount; // ���� �߰�
    }
}
