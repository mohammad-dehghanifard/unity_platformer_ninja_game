using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

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

    // health variables
    private int currentHealth;
    public int maxHealth;
    public HealthBarController healthBarController;
    public float imortalTime;
    private float imortalCounter;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       Init();
    }

    void Update()
    {
        PlayerHorizontalMove();
        Playerjump();
        ChangeDirection();
        ImortalDecrease();
    }

    // init components
    private void Init()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBarController.SetMaxHealth(currentHealth);
    }

    #region player movement functions
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
    #endregion

    // decrease palyer dmage
    public void DecreaseDamage(int damage)
    {
        if(imortalCounter <= 0)
        {
            currentHealth -= damage;
            healthBarController.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                imortalCounter = imortalTime;
                sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0.5f);
            }
        }
    }

    private void ImortalDecrease()
    {
        if(imortalCounter > 0)
        {
            imortalCounter -= Time.deltaTime;

            if(imortalCounter <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,1f);
            }
        }
    }
}
