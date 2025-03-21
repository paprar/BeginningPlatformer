using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameObject SaveInfoObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //스테이지를 클리어했습니다.
            //클리어 과정

            //값 저장
            PlayerPrefs.SetInt("SavePoint0", 1);
            PlayerPrefs.Save();
            Debug.Log("SavePoint0 저장됨!");

            //플레이어와 충돌하면 Clear Info UI를 활성화하고 3초 후 비활성화
            StartCoroutine(ShowSaveInfo());

        }
        

    }

    private IEnumerator ShowSaveInfo()
    { 
            SaveInfoObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            SaveInfoObject.SetActive(false);
    } 
}

