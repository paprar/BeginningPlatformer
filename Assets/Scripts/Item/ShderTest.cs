using UnityEngine;

public class ChangeSpriteColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 스프라이트 컬러를 하얗게 변경
        //spriteRenderer.color = Color.white;

        // 또는 원하는 RGB 컬러로 변경 (예시: 빨간색)
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
    }
}

