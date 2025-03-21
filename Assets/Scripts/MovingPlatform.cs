using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float sideRayLength = 0.1f;

    private Vector3 previousPosition;
    private BoxCollider2D col;

    void Start()
    {
        previousPosition = transform.position;
        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        // �̵�
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime);

        // ���� ������ ���� �������� ���
        Vector2 originLeft = new Vector2(col.bounds.min.x, col.bounds.center.y);
        Vector2 originRight = new Vector2(col.bounds.max.x, col.bounds.center.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(originLeft, Vector2.left, sideRayLength, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(originRight, Vector2.right, sideRayLength, groundLayer);

        if (hitLeft.collider != null && moveDirection.x < 0)
        {
            moveDirection = Vector2.right;
        }
        else if (hitRight.collider != null && moveDirection.x > 0)
        {
            moveDirection = Vector2.left;
        }

        // ���� �ִ� �÷��̾� ����
        Vector2 boxCenter = new Vector2(transform.position.x, col.bounds.max.y + 0.05f);
        Vector2 boxSize = new Vector2(col.bounds.size.x, 0.1f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, playerLayer);

        // �̵� �Ÿ� ���
        Vector3 deltaMove = transform.position - previousPosition;

        foreach (Collider2D c in hits)
        {
            // y�� �̵� ���� x�ุ �÷��̾ ���󰡰�
            c.transform.position += new Vector3(deltaMove.x, 0f, 0f);
        }

        previousPosition = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        if (GetComponent<Collider2D>() == null) return;

        BoxCollider2D col = GetComponent<BoxCollider2D>();

        // ���� �� ���� Ȯ��
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(col.bounds.min.x, col.bounds.center.y), new Vector2(col.bounds.min.x - sideRayLength, col.bounds.center.y));
        Gizmos.DrawLine(new Vector2(col.bounds.max.x, col.bounds.center.y), new Vector2(col.bounds.max.x + sideRayLength, col.bounds.center.y));

        // ���� �ڽ� üũ
        Gizmos.color = Color.green;
        Vector2 boxCenter = new Vector2(transform.position.x, col.bounds.max.y + 0.05f);
        Vector2 boxSize = new Vector2(col.bounds.size.x, 0.1f);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}



