using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_2 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;          // �Ϲ� ���� ��
    public float wallJumpForceX;     // �� ���� �� ���� ��
    public float wallJumpForceY;     // �� ���� �� ���� ��
    public Transform playerStartPosition;

    private Rigidbody2D rigid;
    ParticleSystem dust;

    private bool isGrounded = false;
    public LayerMask groundLayer;

    // �� ���� �� �����̵� ���� ����
    public float wallDistance = 0.5f;    // �¿�� �� ������ ���� �Ÿ�
    public float wallSlideGravity = 0.5f;  // �� �����̵� �� ������ �߷� (���� �Ϲ� �߷º��� ����)
    public float normalGravity = 1f;       // �⺻ �߷� ������
    public float maxSlideSpeed = 2f;       // �� �����̵� �� �ִ� ���� �ӵ�

    private bool isWallSliding = false;

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
        // �¿� �̵�
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);

        // �ٴ� ����: �÷��̾� �߾ӿ��� �Ʒ������� Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // �¿� �� ����: �÷��̾� �߾ӿ��� ��/��� Raycast
        bool isTouchingWallLeft = Physics2D.Raycast(transform.position, Vector2.left, wallDistance, groundLayer);
        bool isTouchingWallRight = Physics2D.Raycast(transform.position, Vector2.right, wallDistance, groundLayer);

        // �� �����̵� ���� �Ǵ�: ���� ���� �ʰ� �¿� �� ���� ���� ��� ������
        if (!isGrounded && (isTouchingWallLeft || isTouchingWallRight))
        {
            isWallSliding = true;
            // �����̵� �߿��� �߷� �������� ���� ���� ���ϸ� ����
            rigid.gravityScale = wallSlideGravity;
            // ���� �ӵ��� �ʹ� ������ ����
            if (rigid.velocity.y < -maxSlideSpeed)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, -maxSlideSpeed);
            }
        }
        else
        {
            isWallSliding = false;
            rigid.gravityScale = normalGravity;
        }

        // ���� �Է� ó��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // �Ϲ� ����
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            else if (isWallSliding)
            {
                // �� ����: ���� ���� ������ �ݴ������� ���� ���� ���ϰ� ���� ����
                CreateDust();
                float wallDir = isTouchingWallLeft ? 1f : -1f;
                rigid.velocity = new Vector2(wallDir * wallJumpForceX, wallJumpForceY);
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}

