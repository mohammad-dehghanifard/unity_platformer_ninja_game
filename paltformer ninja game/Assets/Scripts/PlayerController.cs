using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement variables
    [Range(5f,30f)]
    public float moveSpeed;
    [Range(8f, 10f)]
    public float jumpForce;
    private float direction;
    private Rigidbody2D rd;
    private SpriteRenderer sr;

    // jump variables
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayers;
    private bool isGround;

    // amimation variables
    private Animator anim;

    void Start()
    {
       Init();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHorizontalMove();
        Playerjump();
        ChangeDirection();
    }

    private void Init()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void PlayerHorizontalMove()
    {
        direction = Input.GetAxis("Horizontal");
        rd.velocity = new Vector2(direction * moveSpeed, rd.velocity.y);
        anim.SetFloat("MoveSpeed",Mathf.Abs(rd.velocity.x));
    }

    private void Playerjump()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rd.velocity = new Vector2(rd.velocity.x, jumpForce);
        }
        anim.SetBool("isGrounded",isGround);
    }

    private void ChangeDirection()
    {
        if(rd.velocity.x < 0)
        {
            sr.flipX = true;
        } 
        else if(rd.velocity.x > 0)
        {
            sr.flipX = false;
        }
    }
}
