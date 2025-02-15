using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject SaveInfoObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���������� Ŭ�����߽��ϴ�.
        Debug.Log("���������� �����߽��ϴ�.");
        //Ŭ���� ����


        //�÷��̾�� �浹�ϸ� Clear Info UI�� Ȱ��ȭ�ϰ� 3�� �� ��Ȱ��ȭ
        StartCoroutine(ShowSaveInfo());

    }

    private IEnumerator ShowSaveInfo()
    { 
            SaveInfoObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            SaveInfoObject.SetActive(false);
    } 
}

