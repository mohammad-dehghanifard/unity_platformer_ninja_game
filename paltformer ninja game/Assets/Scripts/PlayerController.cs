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

    // respwan variable
    private Vector2 checkPoint;

    // pause menu
    public GameObject pauseMenu;
    public bool isPause = false;

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
        ActivePauseMenu();
        
    }

    // init components
    private void Init()
    {
        rd = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        SetPlayerHealth();
        checkPoint = transform.position;
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
            AudioController.instance.PlayAudio(1);
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
            AudioController.instance.PlayAudio(2);
            healthBarController.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                PlayerDie();
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

    private void SetPlayerHealth()
    {
        currentHealth = maxHealth;
        healthBarController.SetMaxHealth(currentHealth);
    }

    private void PlayerDie()
    {
        SetPlayerHealth();  
        transform.position = checkPoint;
    }

    public void UpdateCheckPoint(Vector2 newCheckPoint) => checkPoint = newCheckPoint;

    private void ActivePauseMenu()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        if(isPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

}
