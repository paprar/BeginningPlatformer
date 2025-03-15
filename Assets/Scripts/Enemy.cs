using System.Collections;
using UnityEngine;

public class RandomMovingEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float minMoveDistance = 2f; // 이동 최소 거리 증가
    public float maxMoveDistance = 5f; // 이동 최대 거리 증가
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
    private bool hasReachedTarget = false; // 목표 도착 여부

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

        // 목표 지점 도착 체크
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            hasReachedTarget = true;
            StartCoroutine(WaitAndSetNewTarget());
            return;
        }

        // 장애물, 다른 적 감지 및 자기 자신 제외
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
        } while (moveDistance < minMoveDistance); // 최소 이동 거리 보장

        targetPosition = new Vector3(transform.position.x + (moveDistance * direction), transform.position.y, transform.position.z);

        sr.flipX = direction > 0;

        Debug.Log($"[Enemy] New target set: {targetPosition}, Direction: {direction}");
    }

    private void ReverseDirection()
    {
        if (hasReachedTarget) return; // 목표 도착 후에는 방향 변경하지 않음

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
        // 장애물과 충돌하면 방향 반전
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            ReverseDirection();
        }

        // 적과 충돌 시 방향 전환 (반복적인 변경 방지)
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            if (collision.relativeVelocity.magnitude > 0.1f)
            {
                ReverseDirection();
                Debug.Log("[Enemy] Collided with another enemy. Reversing direction.");
            }
        }

        // 플레이어가 적을 위에서 밟았는지 체크
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









