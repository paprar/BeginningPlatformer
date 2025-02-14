using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    int speed = 5;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 입력 값 받기
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D 키 또는 화살표 좌우
        float moveVertical = Input.GetAxis("Vertical");     // W/S 키 또는 화살표 상하

        // 속도 설정
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Rigidbody2D의 속도 설정
        rb.velocity = movement * speed;
    }
}
