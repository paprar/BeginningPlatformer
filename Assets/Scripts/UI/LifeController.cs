using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LifeController : MonoBehaviour
{
    public TMP_Text lifeText; // TextMeshPro UI 텍스트
    public int lifeCount = 2; // 초기 목숨 개수 (X2부터 시작)
    private SceneLoader_GameOver go;

    public void Start()
    {
        go = FindAnyObjectByType<SceneLoader_GameOver>();
        lifeText.text = "X" + lifeCount;
    }
    public void ReduceLife()
    {
        if (lifeCount > 0)
        {
            lifeCount--; // 목숨 감소
            lifeText.text = "X" + lifeCount;
        }
        else
        {
            lifeText.text = "X-1";
            Debug.LogError("게임 오버! 더 이상 플레이어를 부활할 수 없습니다.");
            go.LoadScene();
        }
    }

    public void GainLife()
    {
        lifeCount++;
        lifeText.text = "X" + lifeCount;     
    }
}

