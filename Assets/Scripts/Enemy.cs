using System.Collections;
using UnityEngine;

public class RandomMovingEnemy : MonoBehaviour
{
    public float speed = 2f; // 이동 속도
    public float minMoveDistance = 1f; // 최소 이동 거리
    public float maxMoveDistance = 3f; // 최대 이동 거리
    public float minWaitTime = 0.5f; // 최소 대기 시간
    public float maxWaitTime = 2f;   // 최대 대기 시간
    public LayerMask groundLayer; // 바닥 감지용 레이어
    public LayerMask obstacleLayer; // 장애물 감지 레이어
    public LayerMask playerLayer; // 플레이어 감지 레이어
    public float groundCheckDistance = 0.5f; // 바닥 감지 거리
    public int ObjectNumber;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;
    private Vector3 targetPosition;
    private bool isWaiting = false;
    private int direction = 1; // 현재 이동 방향 (1: 오른쪽, -1: 왼쪽)

    private PlayerHealth PlayerHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
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
        // 목표 지점으로 이동
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 장애물과 충돌하거나 바닥이 없으면 방향을 바꿈
        if (Physics2D.Raycast(transform.position, Vector2.right * direction, 0.5f, obstacleLayer) || !IsGroundAhead())
        {
            ReverseDirection();
        }

        // 목표 지점에 도달하면 랜덤한 시간 동안 대기 후 새로운 위치 설정
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

        // 방향에 따라 flipX 변경 (오른쪽 = true, 왼쪽 = false)
        sr.flipX = direction > 0;
    }

    private void ReverseDirection()
    {
        direction *= -1; // 이동 방향 반전
        SetNewTargetPosition(); // 새로운 위치 설정
    }

    private bool IsGroundAhead()
    {
        // 바닥 감지를 위해 발밑에서 이동 방향으로 Raycast 발사
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition, Vector2.down, groundCheckDistance, groundLayer);

        return hit.collider != null; // 바닥이 감지되면 true, 아니면 false 반환
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌하면 방향 반전
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            ReverseDirection();
        }

        // 플레이어가 적을 위에서 밟았는지 체크
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.contacts[0].normal.y < -0.5f) // 위에서 충돌한 경우
            {
                SoundManager.Instance.PlaySFX("Hurt");
                FallDown();
            }
        }
    }

    private void FallDown()
    {
        col.enabled = false; // Collider 비활성화 (바닥을 통과하게 됨)
        sr.flipY = true;
        Destroy(gameObject, 2f); // 2초 후 삭제
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
        // 바닥 감지 Ray를 시각적으로 확인하기 위한 기즈모 표시
        Gizmos.color = Color.red;
        Vector2 groundCheckPosition = new Vector2(transform.position.x + (0.5f * direction), transform.position.y);
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector2.down * groundCheckDistance);
    }
}






