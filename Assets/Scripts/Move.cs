using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float climbSpeed = 2f;
    public float wallSlideSpeed = 0.5f; // ���� �پ��� �� �������� �ӵ� // �� ����
    public float wallJumpForce = 10f; // �� ���� �� // �� ����
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    Animator animator;
    SpriteRenderer sprite;

    private bool isGrounded = false;
    private bool isLadder = false;
    private bool isWall = false; // �� ����
    private bool isWallSliding = false; // �� ����
    public LayerMask groundLayer;
    public LayerMask LadderLayer;
    public LayerMask wallLayer; // �� ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        transform.position = playerStartPosition.position;
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);

        // �� ����
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        // ��ٸ� ����
        isLadder = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 0.3f, LadderLayer);
        // �� ����
        isWall = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 1.1f, wallLayer); // �� ����

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump right");
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump left");
            }
        }

        // �� �����̵� ���
        if (isWall && !isGrounded && rigid.velocity.y < 0)
        {
            isWallSliding = true;
            rigid.velocity = new Vector2(0, -wallSlideSpeed); // ������ õõ�� ��������
        }
        else
        {
            isWallSliding = false;
        }

        // ���� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                SoundManager.Instance.PlaySFX("Jump");
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            else if (isWallSliding) // �� ����
            {
                // �ݴ� ������ ȭ��ǥ Ű�� �Բ� ������ ���� ���� ����
                if ((!sprite.flipX && Input.GetKey(KeyCode.LeftArrow)) || (sprite.flipX && Input.GetKey(KeyCode.RightArrow)))
                {
                    SoundManager.Instance.PlaySFX("Jump");

                    // ������ �ݴ� �������� ���� (������ ���̸� ����, ���� ���̸� ������)
                    float jumpDirection = sprite.flipX ? -1 : 1;
                    rigid.velocity = new Vector2(jumpDirection * wallJumpForce, jumpForce);
                    sprite.flipX = !sprite.flipX; // ���� ����
                }
            }
            else if (isLadder) // ��ٸ����� ���� ����, else�� ���� �ȵ�.
            {
                SoundManager.Instance.PlaySFX("Jump");
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 2);
            }
        }

        // **��ٸ� ���� ���� (��/�Ʒ� �̵� ����)**
        if (isLadder)
        {
            rigid.gravityScale = 0; // ��ٸ��� ���� �� �߷� ����

            if (Input.GetKey(KeyCode.Space)) // ���� �ö�
            {
                rigid.velocity = new Vector2(rigid.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow)) // �Ʒ��� ������
            {
                rigid.velocity = new Vector2(rigid.velocity.x, -climbSpeed);
            }
            else // �ƹ� Ű�� ������ ������ �״�� ����
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }
        }
        else
        {
            rigid.gravityScale = 1; // ��ٸ��� ����� �ٽ� �߷� Ȱ��ȭ
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

}

