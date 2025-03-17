using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth; // 플레이어 체력
    public float knockbackForce = 5f; // 튕겨나는 힘
    public float invincibleTime = 2f; // 무적 시간
    public float blinkInterval = 0.2f; // 깜빡임 간격
    public LayerMask obstacleLayers; // 여러 개의 장애물 레이어 설정

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    public bool isInvincible = false;
    public bool isDead = false; // 사망 여부 확인
    private Animator animator;

    private float respawnTime = 5f; // 부활까지 걸리는 시간

    public GameObject Border;

    private HealthHeartController HC;
    private LifeController L;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
        col = GetComponent<Collider2D>(); // Collider 가져오기
        sr = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 가져오기
        animator = GetComponent<Animator>();
        HC = FindObjectOfType<HealthHeartController>();
        L = FindObjectOfType<LifeController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayers) != 0 && !isInvincible && !isDead)
        {
            TakeDamage(1);
            Knockback(collision.transform);
            HC.ProcessHealthChange();
            Debug.Log("Hurt");
        }
    }
    public void RestoreHealth()
    {
        health = health+1;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("플레이어 피격! 남은 체력: " + health);
        SoundManager.Instance.PlaySFX("Hurt");

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
        Vector2 knockbackDir = (transform.position - obstacle.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        sr.enabled = true;
        isInvincible = false;
    }

    public void Die()
    {
        SoundManager.Instance.PlaySFX("Died");
        Debug.Log("플레이어 사망!");
        isDead = true;
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(0, -5f);
        sr.flipY = true;

        StartCoroutine(Respawn());
        StartCoroutine(ShowDeathInfo());

    }

    private IEnumerator Respawn()
    {
        col.enabled = false; // 충돌 비활성화

        yield return new WaitForSeconds(respawnTime); // 5초 대기

        // 부활 처리
        health = maxHealth; // 체력 초기화
        isDead = false;
        isInvincible = true;

        sr.enabled = true; // 다시 보이게 설정
        rb.simulated = true; // 물리 작동 다시 활성화
        col.enabled = true; // 충돌 다시 활성화
        sr.flipY = false;
        //animator.SetTrigger("respawn"); // 부활 애니메이션 실행 (필요시)

        transform.position = new Vector2(-15, 1); // 지정된 위치로 이동

        Debug.Log("플레이어 부활!");
        StartCoroutine(Invincibility()); // 부활 후 무적 상태 적용


        // 체력 감소가 정상적으로 작동하도록 초기화
        if (HC != null)
        {
            HC.RestoreAllHealth();
            HC.ResetHealthIndex();

        }

        // 목숨 UI 업데이트
        if (L != null)
        {
            L.ReduceLife();
        }
    }

    private IEnumerator ShowDeathInfo()
    {
        Border.SetActive(true);
        yield return new WaitForSeconds(3);
        Border.SetActive(false);
    }
}




