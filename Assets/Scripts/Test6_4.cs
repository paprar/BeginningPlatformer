using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_4 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float climbSpeed = 2f;
    public float wallSlideSpeed = 0.5f; // 벽에 붙었을 때 떨어지는 속도
    public float wallJumpForce = 10f; // 벽 점프 힘
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    Animator animator;
    SpriteRenderer sprite;

    private bool isGrounded = false;
    private bool isLadder = false;
    private bool isWall = false;
    private bool isWallSliding = false;
    public LayerMask groundLayer;
    public LayerMask LadderLayer;
    public LayerMask wallLayer;

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

        // 땅 감지
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        // 사다리 감지
        isLadder = Physics2D.Raycast(transform.position, Vector2.right, 1.1f, LadderLayer);
        // 벽 감지
        isWall = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 1.1f, wallLayer);

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

        // 벽 슬라이딩 기능
        if (isWall && !isGrounded && rigid.velocity.y < 0)
        {
            isWallSliding = true;
            rigid.velocity = new Vector2(rigid.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // 점프 로직
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                SoundManager.Instance.PlaySFX("Jump");
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            else if (isWallSliding) // 벽 점프
            {
                SoundManager.Instance.PlaySFX("Jump");
                float jumpDirection = sprite.flipX ? -1 : 1; // 벽에서 반대 방향으로 점프
                rigid.velocity = new Vector2(jumpDirection * wallJumpForce, jumpForce);
                sprite.flipX = !sprite.flipX; // 방향 반전
            }
        }

        //클라임 로직
        if (Input.GetKey(KeyCode.Space))
        {
            if (isLadder)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, climbSpeed);
            }
        }

    }

    void CreateDust()
    {
        dust.Play();
    }
}
