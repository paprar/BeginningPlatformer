using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //플레이어가 시작할때 내가 지정한 위치로 이동하라.
        //transform.position = new Vector2(-8f,2f);

        transform.position = playerStartPosition.position;

        //transform.Rotate(new Vector3());

        //transform.localScale = 
    }

    // Update is called once per frame
    void Update()
    {
        //입력을 받아와야 한다.
        //if(Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position = transform.position + Vector3.right* moveSpeed * Time.deltaTime;
        //    //위치 = 위치+ 방향*속도*시간
        //}

        //if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position = transform.position + Vector3.left* moveSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position = transform.position + Vector3.down * moveSpeed * Time.deltaTime;
        //}


        float moveInputX = Input.GetAxisRaw("Horizontal"); //가로방향의 입력값을 받는다.

        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateDust();
            rigid.velocity = new Vector2(rigid.velocity.x, 10);
        }

        
        //입력에 맞는 방향을 설정하는 법

        //캐릭터 방향

        //

        // 캐릭터가 움직인다.

        // 입력

        //(x,y) 좌표 이동한다.
    }

    void CreateDust()
    {
        dust.Play();
    }
}
