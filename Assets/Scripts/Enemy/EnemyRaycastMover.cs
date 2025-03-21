using UnityEngine;

public class EnemyRaycastMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;
    public float detectionDistance = 0.5f;

    // ������ ���̾�� (ground, wall, obstacle, enemy ��)
    public LayerMask detectionLayers;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Raycast ����
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

        // Ray�� ���� ����
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectionDistance, detectionLayers);

        return hit.collider != null;
    }

    void FlipDirection()
    {
        moveDirection = -moveDirection;

        // ��������Ʈ ����
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ����׿� Ray �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(moveDirection.normalized * detectionDistance));
    }
}

