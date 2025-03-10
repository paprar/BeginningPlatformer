using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LifeController : MonoBehaviour
{
    public TMP_Text lifeText; // TextMeshPro UI �ؽ�Ʈ
    public int lifeCount = 2; // �ʱ� ��� ���� (X2���� ����)
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
            lifeCount--; // ��� ����
            lifeText.text = "X" + lifeCount;
        }
        else
        {
            lifeText.text = "X-1";
            Debug.LogError("���� ����! �� �̻� �÷��̾ ��Ȱ�� �� �����ϴ�.");
            go.LoadScene();
        }
    }

    public void GainLife()
    {
        lifeCount++;
        lifeText.text = "X" + lifeCount;     
    }
}

