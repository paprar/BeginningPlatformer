using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    private int currentScore = 0;

    //public float killEnemyPoint = 10.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainScore(int Score)
    {
        currentScore += Score;
        scoreText.text = "" + currentScore.ToString();
    }

    //public float GetScoreByParam(float value)
    //{
    //    return value * killEnemyPoint;
    //}
}
