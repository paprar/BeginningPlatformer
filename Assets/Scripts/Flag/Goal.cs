using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject ClearInfoObject;
    public SceneLoader_Intro SceneLoader_Intro;
    public int SceneIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //���������� Ŭ�����߽��ϴ�.
            Debug.Log("���������� Ŭ�����߽��ϴ�.");
            //Ŭ���� ����

            StartCoroutine(StageClear());
            //�÷��̾�� �浹�ϸ� Clear Info UI�� Ȱ��ȭ�Ѵ�.
            

        }
        
        IEnumerator StageClear()
        {
            ClearInfoObject.SetActive(true);
            yield return new WaitForSeconds(3);
            ClearInfoObject.SetActive(false);
            LoadScene();
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
