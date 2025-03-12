using UnityEngine;

public class ChangeSpriteColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ��������Ʈ �÷��� �Ͼ�� ����
        //spriteRenderer.color = Color.white;

        // �Ǵ� ���ϴ� RGB �÷��� ���� (����: ������)
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }
}

