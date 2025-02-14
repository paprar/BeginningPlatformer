using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public float moveSpeed = 15f; //이동 속도
    private Rigidbody2D rigid; //Rigidbody 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //Rigidbody 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); //좌우이동

        Vector2 moveDirectionX = new Vector2(moveX, 0);
        Vector2 moveDirectionY = new Vector2(0, 1);
        rigid.AddForce(moveDirectionX * moveSpeed, ForceMode2D.Impulse); 
        //velocity는 vector 생성자 사용해서 방향 즉시 수정 가능
        //addforce는 vector 생성자를 인자로 받기만 해서 방향 즉시 수정 불가능, 다른 방향은 다른 변수 추가해야함.

        if(Input.GetKey(KeyCode.Space))
        {
            rigid.AddForce(moveDirectionY,ForceMode2D.Impulse);
        }



    }
}
