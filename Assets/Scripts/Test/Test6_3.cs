using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest6_3 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform playerStartPosition;
    private Rigidbody2D rigid;
    ParticleSystem dust;
    private bool isGrounded = false;
    public LayerMask groundLayer;

    float isRight = 1;

    public Transform wallCheck;
    public float wallCheckDistance;
    //public LayerMask wall;
    bool isWall;
    public float slidingSpeed;
    public float wallJumpForce;
    public bool isWallJump;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        dust = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        transform.position = playerStartPosition.position;
    }

    void Update()
    {
        if (!isWallJump)
        {
            float moveInputX = Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(moveInputX * moveSpeed, rigid.velocity.y);
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        isWall = Physics2D.Raycast(wallCheck.position,Vector2.right*isRight,wallCheckDistance,groundLayer);

        // 스페이스바를 눌렀을 때, 땅에 있을 때만 점프 실행
        if (Input.GetAxisRaw("Jump")!=0)
        {
            if (isGrounded)
            {
                CreateDust();
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            }
        }


    }

    private void FixedUpdate()
    {
        if (isWall)
        {
            isWallJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isWallJump = true;
                Invoke("FreezeX",0.3f);
                rigid.velocity = new Vector2(-isRight*wallJumpForce*rigid.velocity.x, 0.9f*wallJumpForce*rigid.velocity.y);
                
            }

        }

    }
    void FreezeX()
    {
        isWallJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(wallCheck.position, Vector2.right*isRight*wallCheckDistance);
    }
    void CreateDust()
    {
        dust.Play();
    }
}
