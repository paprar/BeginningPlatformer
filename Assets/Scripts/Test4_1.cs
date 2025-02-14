using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4_1 : MonoBehaviour
{
    public float baseSpeed = 5f;  // 기본 속도
    public float baseJumpForce = 15f; // 기본 점프력
    public float maxMultiplier = 1.3f; // 최대 배율 (1.3배)
    public float accelerationTime = 2f; // 최대 증가 시간 (2초)
    public LayerMask groundLayer;
    public LayerMask wallLayer; // 벽 감지 레이어

    private Rigidbody2D rb;
    private float speedMultiplier = 1f;
    private float jumpMultiplier = 1f;
    private float increaseTimer = 0f;
    private bool isGrounded;
    private bool isWallSliding;
    private float lastDirection = 0f; // 마지막 이동 방향
    private float wallJumpCooldown = 0.2f; // 벽 점프 후 입력 잠금 시간
    private float wallJumpTimer;
    private int wallJumpDirection = 1; // 벽 점프 방향
    public float wallJumpMultiplier = 1.2f; // 벽 점프 추가 배율

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // 이동 방향이 바뀌면 속도 & 점프력 초기화
        if (Mathf.Abs(moveX) > 0.01f)
        {
            if (Mathf.Sign(moveX) != Mathf.Sign(lastDirection) || lastDirection == 0f)
            {
                increaseTimer = 0f; // 방향 바꾸면 초기화
                speedMultiplier = 1f;
                jumpMultiplier = 1f;
            }
            else
            {
                increaseTimer += Time.deltaTime; // 계속 이동하면 증가
                float targetMultiplier = Mathf.Lerp(1f, maxMultiplier, increaseTimer / accelerationTime);
                speedMultiplier = Mathf.Min(targetMultiplier, maxMultiplier);
                jumpMultiplier = Mathf.Min(targetMultiplier, maxMultiplier);
            }
            lastDirection = moveX;
        }
        else
        {
            increaseTimer = 0f;
            speedMultiplier = 1f;
            jumpMultiplier = 1f;
        }

        // 최종 속도 적용 (벽 점프 직후 잠깐 입력 막기)
        if (Time.time > wallJumpTimer)
        {
            rb.velocity = new Vector2(moveX * baseSpeed * speedMultiplier, rb.velocity.y);
        }

        // 땅에 닿아 있는지 확인
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // 벽에 닿아 있는지 확인
        bool isTouchingWallLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallLayer);
        bool isTouchingWallRight = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallLayer);

        isWallSliding = (isTouchingWallLeft || isTouchingWallRight) && !isGrounded && rb.velocity.y < 0;

        // 벽 슬라이딩 상태일 때 속도 줄이기
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
            wallJumpDirection = isTouchingWallLeft ? 1 : -1; // 벽 방향 반대쪽으로 점프
        }

        // 점프 처리 (일반 점프 및 벽 점프)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) // 일반 점프
            {
                rb.velocity = new Vector2(rb.velocity.x, baseJumpForce * jumpMultiplier);
            }
            else if (isWallSliding) // 벽 점프
            {
                rb.velocity = new Vector2(baseSpeed * wallJumpDirection, baseJumpForce * wallJumpMultiplier);
                wallJumpTimer = Time.time + wallJumpCooldown; // 벽 점프 후 잠깐 입력 막기
            }
        }
    }
}
