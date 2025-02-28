using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3; // 플레이어 체력
    public float knockbackForce = 5f; // 튕겨나는 힘
    public float invincibleTime = 2f; // 무적 시간
    public float blinkInterval = 0.2f; // 깜빡임 간격
    public string obstacleLayerName = "Obstacle"; // 장애물 레이어 이름

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private int obstacleLayer; // 장애물 레이어 저장
    private bool isDead = false; // 사망 여부 확인

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
        col = GetComponent<Collider2D>(); // Collider 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기
        obstacleLayer = LayerMask.NameToLayer(obstacleLayerName); // 장애물 레이어 값 저장
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer && !isInvincible && !isDead) // 장애물 감지 & 무적 상태 아님
        {
            TakeDamage(1);
            Knockback(collision.transform);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("플레이어 피격! 남은 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    void Knockback(Transform obstacle)
    {
        Vector2 knockbackDir = (transform.position - obstacle.position).normalized; // 충돌 방향 계산
        rb.velocity = Vector2.zero; // 기존 속도 초기화
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse); // 튕겨남
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // 깜빡임
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true; // 무적 종료 시 다시 보이도록 설정
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("플레이어 사망!");
        isDead = true; // 사망 상태 활성화
        col.enabled = false; // 충돌 비활성화 (바닥을 통과)
        rb.gravityScale = 5f; // 중력 증가 (빠르게 낙하)
        rb.velocity = new Vector2(0, -10f); // 아래로 떨어지는 힘 추가
    }
}

