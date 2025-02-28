using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3; // �÷��̾� ü��
    public float knockbackForce = 5f; // ƨ�ܳ��� ��
    public float invincibleTime = 2f; // ���� �ð�
    public float blinkInterval = 0.2f; // ������ ����
    public string obstacleLayerName = "Obstacle"; // ��ֹ� ���̾� �̸�

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private int obstacleLayer; // ��ֹ� ���̾� ����
    private bool isDead = false; // ��� ���� Ȯ��

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ��������
        col = GetComponent<Collider2D>(); // Collider ��������
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ��������
        obstacleLayer = LayerMask.NameToLayer(obstacleLayerName); // ��ֹ� ���̾� �� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer && !isInvincible && !isDead) // ��ֹ� ���� & ���� ���� �ƴ�
        {
            TakeDamage(1);
            Knockback(collision.transform);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("�÷��̾� �ǰ�! ���� ü��: " + health);

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
        Vector2 knockbackDir = (transform.position - obstacle.position).normalized; // �浹 ���� ���
        rb.velocity = Vector2.zero; // ���� �ӵ� �ʱ�ȭ
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse); // ƨ�ܳ�
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // ������
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true; // ���� ���� �� �ٽ� ���̵��� ����
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("�÷��̾� ���!");
        isDead = true; // ��� ���� Ȱ��ȭ
        col.enabled = false; // �浹 ��Ȱ��ȭ (�ٴ��� ���)
        rb.gravityScale = 5f; // �߷� ���� (������ ����)
        rb.velocity = new Vector2(0, -10f); // �Ʒ��� �������� �� �߰�
    }
}

