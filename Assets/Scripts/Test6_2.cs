using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_2 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;          // 일반 점프 힘
    public float wallJumpForceX;     // 벽 점프 시 수평 힘
    public float wallJumpForceY;     // 벽 점프 시 수직 힘
    public Transform playerStartPosition;

    private Rigidbody2D rigid;
    ParticleSystem dust;

    private bool isGrounded = false;
    public LayerMask groundLayer;

    // 벽 감지 및 슬라이딩 관련 변수
    public float wallDistance = 0.5f;    // 좌우로 벽 감지를 위한 거리
    public float wallSlideGravity = 0.5f;  // 벽 슬라이딩 시 적용할 중력 (보통 일반 중력보다 낮게)
    public float normalGravity = 1f;       // 기본 중력 스케일
    public float maxSlideSpeed = 2f;       // 벽 슬라이딩 시 최대 낙하 속도

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
        // 좌우 이동
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);

        // 바닥 감지: 플레이어 중앙에서 아래쪽으로 Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // 좌우 벽 감지: 플레이어 중앙에서 좌/우로 Raycast
        bool isTouchingWallLeft = Physics2D.Raycast(transform.position, Vector2.left, wallDistance, groundLayer);
        bool isTouchingWallRight = Physics2D.Raycast(transform.position, Vector2.right, wallDistance, groundLayer);

        // 벽 슬라이딩 상태 판단: 땅에 있지 않고 좌우 중 한쪽 벽에 닿아 있으면
        if (!isGrounded && (isTouchingWallLeft || isTouchingWallRight))
        {
            isWallSliding = true;
            // 슬라이딩 중에는 중력 스케일을 낮춰 빠른 낙하를 방지
            rigid.gravityScale = wallSlideGravity;
            // 낙하 속도가 너무 빠르면 제한
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

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // 일반 점프
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            else if (isWallSliding)
            {
                // 벽 점프: 벽에 닿은 방향의 반대쪽으로 수평 힘을 가하고 위로 점프
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

