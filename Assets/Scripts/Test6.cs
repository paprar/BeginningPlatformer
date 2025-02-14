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

        // �����̽��ٸ� ������ ��, ���� ���� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            CreateDust();
            rigid.velocity = new Vector2(rigid.velocity.x, 10f);
        }
    }

    // ���� �浹���� �� isGrounded�� true�� ���� (�� ������Ʈ�� "Ground" �±װ� �ʿ�)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // ������ �浹�� ������ isGrounded�� false�� ����
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


