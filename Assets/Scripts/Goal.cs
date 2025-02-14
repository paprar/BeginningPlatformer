using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject ClearInfoObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //스테이지를 클리어했습니다.
        Debug.Log("스테이지를 클리어했습니다.");
        //클리어 과정

        //플레이어와 충돌하면 Clear Info UI를 활성화한다.
        ClearInfoObject.SetActive(true);
    }
}
