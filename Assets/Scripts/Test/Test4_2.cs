using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4_2 : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float baseJumpForce = 8f;
    public float maxMultiplier = 1.3f;
    public float accelerationTime = 2f;
    public float wallSlideSpeed = 1f;  // 벽에 붙었을 때 미끄러지는 속도
    public float wallJumpForce = 12f;  // 벽 점프 힘
    public float wallJumpPushForce = 7f; // 벽 점프 시 밀리는 힘
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private float speedMultiplier = 1f;
    private float jumpMultiplier = 1f;
    private float increaseTimer = 0f;
    private bool isGrounded;
    private bool isTouchingWall;
    private float lastDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // 땅 감지
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // 벽 감지 (좌우)
        bool touchingLeftWall = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallLayer);
        bool touchingRightWall = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallLayer);
        isTouchingWall = touchingLeftWall || touchingRightWall;

        // 이동 방향이 바뀌면 속도 & 점프력 초기화
        if (Mathf.Abs(moveX) > 0.01f)
        {
            if (Mathf.Sign(moveX) != Mathf.Sign(lastDirection) || lastDirection == 0f)
            {
                increaseTimer = 0f;
                speedMultiplier = 1f;
                jumpMultiplier = 1f;
            }
            else
            {
                increaseTimer += Time.deltaTime;
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

        // 벽 미끄러짐 처리
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }

        // 이동 적용 (벽에 붙어있지 않을 때만)
        if (!isTouchingWall || isGrounded)
        {
            rb.velocity = new Vector2(moveX * baseSpeed * speedMultiplier, rb.velocity.y);
        }

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, baseJumpForce * jumpMultiplier);
            }
            else if (isTouchingWall)
            {
                // 벽 점프 처리
                float jumpDirection = touchingLeftWall ? 1f : -1f; // 왼쪽 벽이면 오른쪽으로, 오른쪽 벽이면 왼쪽으로
                rb.velocity = new Vector2(jumpDirection * wallJumpPushForce, wallJumpForce);
            }
        }
    }
}

