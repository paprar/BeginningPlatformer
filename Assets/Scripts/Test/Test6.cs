using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    private bool isGrounded = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        transform.position = playerStartPosition.position;
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);

        // 스페이스바를 눌렀을 때, 땅에 있을 때만 점프 실행
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            CreateDust();
            rigid.velocity = new Vector2(rigid.velocity.x, 10f);
        }
    }

    // 땅과 충돌했을 때 isGrounded를 true로 설정 (땅 오브젝트는 "Ground" 태그가 필요)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 땅과의 충돌이 끝나면 isGrounded를 false로 설정
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}


