using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
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
        isLadder = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 0.3f, LadderLayer);
        // 벽 감지
        isWall = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? 1 : -1), 1.1f, wallLayer);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = true;
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        animator.SetTrigger("jump right");
            //    }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = false;
            // if (Input.GetKeyDown(KeyCode.Space))
            //  {
            //      animator.SetTrigger("jump left");
        }


        // 벽 슬라이딩 기능
        if (isWall && !isGrounded && rigid.velocity.y < 0)
        {
            //천천히 내려가기

            isWallSliding = true;
            rigid.velocity = new Vector2(0, -wallSlideSpeed);

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

                if ((sprite.flipX && isWall) || (!sprite.flipX && isWall))
                {
                    isWallSliding = false;
                }

                // 벽 점프 방향 설정 (왼쪽 벽이면 오른쪽 위로, 오른쪽 벽이면 왼쪽 위로)
                float jumpDirection = sprite.flipX ? 1 : -1;

                // 벽에서 떨어지는 속도를 반대 방향으로 강하게 적용
                rigid.velocity = new Vector2(jumpDirection * wallJumpForce, jumpForce);

                // 방향 반전 (즉시 방향을 바꿔 플레이어가 벽에 다시 붙지 않게 함)
                sprite.flipX = !sprite.flipX;
            }

            else if (isLadder) // 사다리에서 점프 가능
            {
                SoundManager.Instance.PlaySFX("Jump");
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 2);
            }
            else
            {

            }
        }

        // **사다리 로직 수정 (위/아래 이동 가능)**
        if (isLadder)
        {
            rigid.gravityScale = 0; // 사다리에 있을 때 중력 제거

            if (Input.GetKey(KeyCode.Space)) // 위로 올라감
            {
                rigid.velocity = new Vector2(rigid.velocity.x, climbSpeed);
            }
            else if (Input.GetKey(KeyCode.DownArrow)) // 아래로 내려감
            {
                rigid.velocity = new Vector2(rigid.velocity.x, -climbSpeed);
            }
            else // 아무 키도 누르지 않으면 그대로 멈춤
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
            }
        }
        else
        {
            rigid.gravityScale = 1; // 사다리를 벗어나면 다시 중력 활성화
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}
