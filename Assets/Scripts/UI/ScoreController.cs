using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TMP_Text Scoretext;

    public float killEnemyPoint = 10.5f;

    // Start is called before the first frame update
    void Start()
    {
        GainScore(GetScoreByParam(5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainScore(float Score)
    {
        Scoretext.text += Score;
    }

    public float GetScoreByParam(float value)
    {
        return value * killEnemyPoint;
    }
}
