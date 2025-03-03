using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LifeController : MonoBehaviour
{
    public TMP_Text lifeText; // TextMeshPro UI �ؽ�Ʈ
    private int lifeCount = 2; // �ʱ� ��� ���� (X2���� ����)
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
            lifeCount--; // ��� ����
        }
        else
        {
            lifeText.text = "X-1";
            Debug.LogError("���� ����! �� �̻� �÷��̾ ��Ȱ�� �� �����ϴ�.");
            go.LoadScene();
        }
    }
}

