using System.Collections;
using UnityEngine;

public class RandomMovingEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float minMoveDistance = 2f; // �̵� �ּ� �Ÿ� ����
    public float maxMoveDistance = 5f; // �̵� �ִ� �Ÿ� ����
    public float minWaitTime = 0.5f;
    public float maxWaitTime = 2f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public float groundCheckDistance = 0.5f;
    public int ObjectNumber;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Vector3 targetPosition;
    private bool isWaiting = false;
    private int direction = 1;
    private bool hasReachedTarget = false; // ��ǥ ���� ����

    private PlayerHealth PlayerHealth;
    private ScoreController score;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        SetNewTargetPosition();
        score = FindObjectOfType<ScoreController>();

        sr.flipY = false;
    }

    private void Update()
    {
        if (!isWaiting && !hasReachedTarget)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ���� ���� üũ
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            hasReachedTarget = true;
            StartCoroutine(WaitAndSetNewTarget());
            return;
        }

        // ��ֹ�, �ٸ� �� ���� �� �ڱ� �ڽ� ����
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.3f, obstacleLayer);
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.3f, enemyLayer);

        if ((hitObstacle.collider != null) || (hitEnemy.collider != null && hitEnemy.collider.gameObject != gameObject) || !IsGroundAhead())
        {
            ReverseDirection();
        }
    }


    private IEnumerator WaitAndSetNewTarget()
    {
        isWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);

        SetNewTargetPosition();
        isWaiting = false;
        hasReachedTarget = false;
    }

    private void SetNewTargetPosition()
    {
        float moveDistance;
        do
        {
            moveDistance = Random.Range(minMoveDistance, maxMoveDistance);
        } while (moveDistance < minMoveDistance); // �ּ� �̵� �Ÿ� ����

        targetPosition = new Vector3(transform.position.x + (moveDistance * direction), transform.position.y, transform.position.z);

        sr.flipX = direction > 0;

        Debug.Log($"[Enemy] New target set: {targetPosition}, Direction: {direction}");
    }

    private void ReverseDirection()
    {
        if (hasReachedTarget) return; // ��ǥ ���� �Ŀ��� ���� �������� ����

        direction *= -1;
        SetNewTargetPosition();
        Debug.Log("[Enemy] Direction reversed!");
    }

    private bool IsGroundAhead()
    {
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� �浹�ϸ� ���� ����
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            ReverseDirection();
        }

        // ���� �浹 �� ���� ��ȯ (�ݺ����� ���� ����)
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            if (collision.relativeVelocity.magnitude > 0.1f)
            {
                ReverseDirection();
                Debug.Log("[Enemy] Collided with another enemy. Reversing direction.");
            }
        }

        // �÷��̾ ���� ������ ��Ҵ��� üũ
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                SoundManager.Instance.PlaySFX("Hurt");
                FallDown();
                if (score != null)
                {
                    Debug.Log("Score added");
                    score.GainScore(100);
                }
            }
        }
    }

    private void FallDown()
    {
        col.enabled = false;
        sr.flipY = true;
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * groundCheckDistance);
    }
}









