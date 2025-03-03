using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform pointA; // �̵� ������
    public Transform pointB; // �̵� ����
    public float speed = 2f; // �̵� �ӵ�
    public LayerMask playerLayer; // �÷��̾� ���̾� ����
    public float minWaitTime = 0.5f; // �ּ� ��� �ð�
    public float maxWaitTime = 2f;   // �ִ� ��� �ð�

    private Rigidbody2D rb;
    private Collider2D col;
    private Vector3 nextPoint;
    private bool isWaiting = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        nextPoint = pointB.position;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            Move();
        }
    }

    private void Move()
    {
        // ���� �̵��ϴ� ����
        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // ��ǥ ������ �����ϸ� ���� �ð���ŭ ����
        if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
        {
            StartCoroutine(WaitAndSwitchDirection());
        }
    }

    private IEnumerator WaitAndSwitchDirection()
    {
        isWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        // �ݴ� �������� �̵� ����
        nextPoint = (nextPoint == pointA.position) ? pointB.position : pointA.position;
        isWaiting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾ ���� ������ ��Ҵ��� üũ
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.contacts[0].normal.y < -0.5f) // ������ �浹�� ���
            {
                FallDown();
            }
        }
    }

    private void FallDown()
    {
        col.enabled = false; // Collider ��Ȱ��ȭ (�ٴ��� ����ϰ� ��)
        Destroy(gameObject, 2f); // 2�� �� ����
    }
}
