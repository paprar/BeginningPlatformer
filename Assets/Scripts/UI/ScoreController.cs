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
        scoreText.text = "" + totalScore; // 점수 갱신
        totalScore += amount; // 점수 추가
    }
}
