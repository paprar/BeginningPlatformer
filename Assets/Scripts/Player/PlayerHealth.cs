using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth; // �÷��̾� ü��
    public float knockbackForce = 5f; // ƨ�ܳ��� ��
    public float invincibleTime = 2f; // ���� �ð�
    public float blinkInterval = 0.2f; // ������ ����
    public LayerMask obstacleLayers; // ���� ���� ��ֹ� ���̾� ����

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    public bool isInvincible = false;
    public bool isDead = false; // ��� ���� Ȯ��
    private Animator animator;

    private float respawnTime = 5f; // ��Ȱ���� �ɸ��� �ð�

    public GameObject Border;

    private HealthHeartController HC;
    private LifeController L;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
        col = GetComponent<Collider2D>(); // Collider ��������
        sr = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ��������
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
        Debug.Log("�÷��̾� �ǰ�! ���� ü��: " + health);
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
        Debug.Log("�÷��̾� ���!");
        isDead = true;
        rb.gravityScale = 1f;
        rb.velocity = new Vector2(0, -5f);
        sr.flipY = true;

        StartCoroutine(Respawn());
        StartCoroutine(ShowDeathInfo());

    }

    private IEnumerator Respawn()
    {
        col.enabled = false; // �浹 ��Ȱ��ȭ

        yield return new WaitForSeconds(respawnTime); // 5�� ���

        // ��Ȱ ó��
        health = maxHealth; // ü�� �ʱ�ȭ
        isDead = false;
        isInvincible = true;

        sr.enabled = true; // �ٽ� ���̰� ����
        rb.simulated = true; // ���� �۵� �ٽ� Ȱ��ȭ
        col.enabled = true; // �浹 �ٽ� Ȱ��ȭ
        sr.flipY = false;
        //animator.SetTrigger("respawn"); // ��Ȱ �ִϸ��̼� ���� (�ʿ��)

        transform.position = new Vector2(-15, 1); // ������ ��ġ�� �̵�

        Debug.Log("�÷��̾� ��Ȱ!");
        StartCoroutine(Invincibility()); // ��Ȱ �� ���� ���� ����


        // ü�� ���Ұ� ���������� �۵��ϵ��� �ʱ�ȭ
        if (HC != null)
        {
            HC.RestoreAllHealth();
            HC.ResetHealthIndex();

        }

        // ��� UI ������Ʈ
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




