using System.Collections;
using UnityEngine;

public class RandomMovingEnemy : MonoBehaviour
{
    public float speed = 2f; // �̵� �ӵ�
    public float minMoveDistance = 1f; // �ּ� �̵� �Ÿ�
    public float maxMoveDistance = 3f; // �ִ� �̵� �Ÿ�
    public float minWaitTime = 0.5f; // �ּ� ��� �ð�
    public float maxWaitTime = 2f;   // �ִ� ��� �ð�
    public LayerMask groundLayer; // �ٴ� ������ ���̾�
    public LayerMask obstacleLayer; // ��ֹ� ���� ���̾�
    public LayerMask playerLayer; // �÷��̾� ���� ���̾�
    public float groundCheckDistance = 0.5f; // �ٴ� ���� �Ÿ�
    public int ObjectNumber;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Vector3 targetPosition;
    private bool isWaiting = false;
    private int direction = 1; // ���� �̵� ���� (1: ������, -1: ����)

    private PlayerHealth PlayerHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
        SetNewTargetPosition();

        sr.flipY = false;
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
        // ��ǥ �������� �̵�
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ֹ��� �浹�ϰų� �ٴ��� ������ ������ �ٲ�
        if (Physics2D.Raycast(transform.position, Vector2.right * direction, 0.5f, obstacleLayer) || !IsGroundAhead())
        {
            ReverseDirection();
        }

        // ��ǥ ������ �����ϸ� ������ �ð� ���� ��� �� ���ο� ��ġ ����
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartCoroutine(WaitAndSetNewTarget());
        }
    }

    private IEnumerator WaitAndSetNewTarget()
    {
        isWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        SetNewTargetPosition();
        isWaiting = false;
    }

    private void SetNewTargetPosition()
    {
        float moveDistance = Random.Range(minMoveDistance, maxMoveDistance);
        targetPosition = new Vector3(transform.position.x + (moveDistance * direction), transform.position.y, transform.position.z);

        // ���⿡ ���� flipX ���� (������ = true, ���� = false)
        sr.flipX = direction > 0;
    }

    private void ReverseDirection()
    {
        direction *= -1; // �̵� ���� ����
        SetNewTargetPosition(); // ���ο� ��ġ ����
    }

    private bool IsGroundAhead()
    {
        // �ٴ� ������ ���� �߹ؿ��� �̵� �������� Raycast �߻�
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null; // �ٴ��� �����Ǹ� true, �ƴϸ� false ��ȯ
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� �浹�ϸ� ���� ����
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            ReverseDirection();
        }

        // �÷��̾ ���� ������ ��Ҵ��� üũ
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.contacts[0].normal.y < -0.5f) // ������ �浹�� ���
            {
                SoundManager.Instance.PlaySFX("Hurt");
                FallDown();
            }
        }
    }

    private void FallDown()
    {
        col.enabled = false; // Collider ��Ȱ��ȭ (�ٴ��� ����ϰ� ��)
        sr.flipY = true;
        Destroy(gameObject, 2f); // 2�� �� ����
        //Respawn();
    }

    //private void Respawn()
    //{
        //if (PlayerHealth.isDead == true)
        //{

        //}
    //}

    private void OnDrawGizmos()
    {
        // �ٴ� ���� Ray�� �ð������� Ȯ���ϱ� ���� ����� ǥ��
        Gizmos.color = Color.red;
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * groundCheckDistance);
    }
}






