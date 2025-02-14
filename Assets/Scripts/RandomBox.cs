using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public GameObject CoinPrefab; //코인 프리팹을 사용하겠다.

    public bool isActivate = true; //RandomBox가 활성화 되어있는 상태

    SpriteRenderer _sr;

    public Sprite unActivateBoxSpriteRenderer;

    public ParticleSystem broken;

    private void Start()
    {
        _sr = GetComponentInParent<SpriteRenderer>();
        broken = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivate) //isActivate변수가 true일때만 아래코드를 실행함.
        {
            //스테이지를 클리어했습니다.
            isActivate = false;
            create();  
            Debug.Log("랜덤박스와 충돌했습니다.");

            Instantiate(CoinPrefab, transform.position, Quaternion.identity);

            // 부모의(오브젝트 이름이 randombox 안의) spriterenderer요소의 sprite 값을 tilemap_29로 변경하라.
            _sr.sprite = unActivateBoxSpriteRenderer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("랜덤박스와 충돌이 끝났습니다.");
    }

    //가만히 있으면 계속 실행됨.
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("랜덤박스와 충돌 진행중입니다.");
    }

    void create()
    {
        broken.Play();
    }
}
