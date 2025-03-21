using UnityEngine;

public class EnemyRaycastMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;
    public float detectionDistance = 0.5f;

    // 감지할 레이어들 (ground, wall, obstacle, enemy 등)
    public LayerMask detectionLayers;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Raycast 감지
        if (IsObstacleAhead())
        {
            FlipDirection();
        }

        rb.velocity = moveDirection * moveSpeed;
    }

    bool IsObstacleAhead()
    {
        Vector2 origin = transform.position;
        Vector2 direction = moveDirection.normalized;

        // Ray를 쏴서 감지
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectionDistance, detectionLayers);

        return hit.collider != null;
    }

    void FlipDirection()
    {
        moveDirection = -moveDirection;

        // 스프라이트 반전
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // 디버그용 Ray 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(moveDirection.normalized * detectionDistance));
    }
}

