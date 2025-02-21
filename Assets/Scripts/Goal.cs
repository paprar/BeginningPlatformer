using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject ClearInfoObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //스테이지를 클리어했습니다.
            Debug.Log("스테이지를 클리어했습니다.");
            //클리어 과정

            StartCoroutine(StageClear());
            //플레이어와 충돌하면 Clear Info UI를 활성화한다.
            

        }
        
        IEnumerator StageClear()
        {
            ClearInfoObject.SetActive(true);
            yield return new WaitForSeconds(3);
            ClearInfoObject.SetActive(false);
        }

    }
}
