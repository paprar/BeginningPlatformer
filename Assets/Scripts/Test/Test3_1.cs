using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3_1 : MonoBehaviour
{
    public float moveSpeed = 20f;  // 기본 이동 속도
    public float maxSpeed = 12f;   // 최대 속도
    public float acceleration = 5f; // 가속도
    private Rigidbody2D rigid;
    public float jumpForce = 8f;
    private bool isGrounded = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.drag = 0.5f; // 공기 저항 조절 (값을 줄이면 더 빠른 이동 가능)
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(moveX, 0);

        // 속도를 부드럽게 증가시키면서 AddForce 적용
        float smoothedSpeed = Mathf.Lerp(0, moveSpeed, Time.fixedDeltaTime * acceleration);
        rigid.AddForce(moveDirection * smoothedSpeed, ForceMode2D.Force);

        // 최대 속도 제한
        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // 공중 상태로 변경
        }
    }

    // 바닥에 닿았을 때 점프 가능하도록 변경
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ground 태그가 있는 오브젝트와 충돌 시
        {
            isGrounded = true;
        }
    }
}
