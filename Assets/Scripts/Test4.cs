using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4 : MonoBehaviour
{
    public float baseSpeed = 5f;  // 기본 속도
    public float baseJumpForce = 15f; // 기본 점프력
    public float maxMultiplier = 1.3f; // 최대 배율 (1.3배)
    public float accelerationTime = 2f; // 최대 증가 시간 (2초)
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float speedMultiplier = 1f;
    private float jumpMultiplier = 1f;
    private float increaseTimer = 0f;
    private bool isGrounded;
    private float lastDirection = 0f; // 마지막 이동 방향

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

        // 최종 속도 적용
        rb.velocity = new Vector2(moveX * baseSpeed * speedMultiplier, rb.velocity.y);

        // 땅에 닿아 있는지 확인
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // 점프 처리 (이동 중 증가한 점프력 적용)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, baseJumpForce * jumpMultiplier);
        }
    }
}

















