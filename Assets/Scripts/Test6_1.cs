using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_1 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float climbSpeed;
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    Animator animator;
    SpriteRenderer sprite;

    private bool isGrounded = false;
    private bool isLadder = false;
    public LayerMask groundLayer;
    public LayerMask LadderLayer;

    public bool FlipX = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        transform.position = playerStartPosition.position;
    }

    void Update()
    {
        float moveInputX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        isLadder = Physics2D.Raycast(transform.position, Vector2.right, 1.1f, LadderLayer);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump right");
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("jump left");
            }
        }


        // 스페이스바를 눌렀을 때, 땅에 있을 때만 점프 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                SoundManager.Instance.PlaySFX("Jump");
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
            if (isLadder)
            {
                SoundManager.Instance.PlaySFX("Jump");
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce*3);
            }

        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}
