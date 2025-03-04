using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LifeController : MonoBehaviour
{
    public TMP_Text lifeText; // TextMeshPro UI 텍스트
    private int lifeCount = 2; // 초기 목숨 개수 (X2부터 시작)
    private SceneLoader_GameOver go;

    public void Start()
    {
        go = FindAnyObjectByType<SceneLoader_GameOver>();
    }
    public void ReduceLife()
    {
        if (lifeCount >= 0)
        {
            lifeText.text = "X" + lifeCount;
            lifeCount--; // 목숨 감소
        }
        else
        {
            lifeText.text = "X-1";
            Debug.LogError("게임 오버! 더 이상 플레이어를 부활할 수 없습니다.");
            go.LoadScene();
        }
    }
}

