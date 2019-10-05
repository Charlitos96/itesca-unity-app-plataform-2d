using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Gamemanager gamemanager;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float maxVel;
    [SerializeField]
    float rayDistance;
    [SerializeField]
    LayerMask groundLayer;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 axis;
    int coins = 0;

    bool btnJump;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        //Move
        rigidbody2D.AddForce(Vector2.right * axis.x * moveSpeed, ForceMode2D.Impulse);
        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x, -maxVel, maxVel), rigidbody2D.velocity.y);

        //grounding
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            -transform.up,
            rayDistance,
            groundLayer
        );

        if(hit.collider)
        {
            //jumping
            if(btnJump)
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void Update()
    {
        axis = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        btnJump = Input.GetButtonDown("Jump");
    }

    void LateUpdate(){
        spriteRenderer.flipX = axis.x < 0 ? true : axis.x > 0 ? false : spriteRenderer.flipX;
        animator.SetFloat("axisX", Mathf.Abs(axis.x));
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * rayDistance);
    }

    public void grabCoin(int score)
    {
        coins += score;
        gamemanager.setScore();
    }

    public int Coins
    {
        get => coins;
    }
}
