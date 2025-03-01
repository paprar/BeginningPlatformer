using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform pointA; // 이동 시작점
    public Transform pointB; // 이동 끝점
    public float speed = 2f; // 이동 속도
    public LayerMask playerLayer; // 플레이어 레이어 설정
    public float minWaitTime = 0.5f; // 최소 대기 시간
    public float maxWaitTime = 2f;   // 최대 대기 시간

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
        // 적이 이동하는 로직
        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // 목표 지점에 도달하면 랜덤 시간만큼 멈춤
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

        // 반대 방향으로 이동 시작
        nextPoint = (nextPoint == pointA.position) ? pointB.position : pointA.position;
        isWaiting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 적을 위에서 밟았는지 체크
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (collision.contacts[0].normal.y < -0.5f) // 위에서 충돌한 경우
            {
                FallDown();
            }
        }
    }

    private void FallDown()
    {
        col.enabled = false; // Collider 비활성화 (바닥을 통과하게 됨)
        Destroy(gameObject, 2f); // 2초 후 삭제
    }
}
