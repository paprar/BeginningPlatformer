using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float climbSpeed = 2f;
    public float wallSlideSpeed = 0.5f; // ���� �پ��� �� �������� �ӵ�
    public float wallJumpForce = 10f; // �� ���� ��
    public Transform playerStartPosition;

    private Rigidbody2D rigid;
    private Collider2D col; // Collider2D �߰�
    private ParticleSystem dust;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerHealth playerHealth; // PlayerHealth ��ũ��Ʈ ����
    private Death death;

    public bool isGrounded = false;
    private bool isLadder = false;
    private bool isWall = false;
    private bool isWallSliding = false;
    public LayerMask groundLayer;
    public LayerMask LadderLayer;
    public LayerMask wallLayer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>(); // Collider2D ��������
        dust = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>(); // PlayerHealth ������Ʈ ��������
        death = FindAnyObjectByType<Death>();
    }

    void Start()
    {
        transform.position = playerStartPosition.position;
    }

    void Update()
    {
        // �÷��̾ ������ �̵�, ���� �Ұ��� + Collider ��Ȱ��ȭ
        if (playerHealth.isDead)
        {
            rigid.velocity = new Vector2(0,-15f);
            return;
        }
        else if (death.isDead)
        {
            rigid.velocity = new Vector2(0,0);
            return;
        }

        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);
        

        // �� ����
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        // ��ٸ� ����
        isLadder = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 0.3f, LadderLayer);
        // �� ����
        isWall = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 1.1f, wallLayer);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = true;
            animator.SetTrigger("walk");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = false;
            animator.SetTrigger("walk");
        }

        // �� �����̵� ���
        if (isWall && !isGrounded && rigid.velocity.y < 0)
        {
            isWallSliding = true;
            rigid.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // ���� ���� (�÷��̾ ���� �ʾ��� ��츸 ����)
        if (Input.GetKeyDown(KeyCode.Space) && !playerHealth.isDead)
        {

            if (isGrounded)
            {     
                SoundManager.Instance.PlaySFX("Jump");
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            else if (isWallSliding) // �� ����
            {
                SoundManager.Instance.PlaySFX("Jump");

                float jumpDirection = sprite.flipX ? 1 : -1;
                rigid.velocity = new Vector2(jumpDirection * wallJumpForce, jumpForce);
                sprite.flipX = !sprite.flipX;
            }
            else if (isLadder) // ��ٸ����� ���� ����
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


