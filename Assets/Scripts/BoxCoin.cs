using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCoin : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float popPower = 2.5f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //컴포넌트를 찾아서 타입이 Rigidbody2D인 녀석을 rigid변수에 할당한다.
        rigid.AddForce(Vector2.up *popPower, ForceMode2D.Impulse); //new Vector3() 이건 왜 하는지, Vector2.up 방향 힘을 가한다.

        //Invoke(nameof((DisableObject), 1.5f)); //1.5f 뒤에 DisableObject 함수 실행
        Destroy(gameObject,2);                 //gameObject 파괴한다 2f 뒤에
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
