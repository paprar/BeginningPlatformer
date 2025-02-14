using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_1 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    private bool isGrounded = false;
    public LayerMask groundLayer;
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
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // �����̽��ٸ� ������ ��, ���� ���� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}
