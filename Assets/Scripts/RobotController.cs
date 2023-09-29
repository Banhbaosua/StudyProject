using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rgbd2D;
    public GameObject player;
    CapsuleCollider2D coll;
    [SerializeField] LayerMask groundMask;
    float speed,animatorSpeed;
    float jumpSpeed = 12f, rollSpeed = 25f,rollDistance;
    float maxSpeed=10f;
    bool isRunning=false,doubleJump,isJumping,isCrouching=false,isRolling=false;
    public bool facingRight=true;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        rgbd2D = player.GetComponent<Rigidbody2D>();
        coll  = player.GetComponent<CapsuleCollider2D>();
        animatorSpeed = animator.speed;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        addForce(speed);
        updateStatus();
    }

    public bool getFacingStatus()    {
        return facingRight;
    }

    public void leftButton_Down()
    {
        facingRight=false;
        speed = -maxSpeed;
        if(isCrouching)
        {
            speed /= 2;
            animator.speed = animatorSpeed;
        }
        isRunning = true;
        Vector3 scale = player.transform.localScale;
        if(scale.x >0)
            scale.x *=(-1);
        player.transform.localScale = scale;
    }

    public void leftButton_Up()
    {
        speed = 0;
        isRunning=false;
        if(isCrouching)
            animator.speed=0;
        else
            animator.speed = animatorSpeed;
    }

    public void rightButton_Down()
    {
        facingRight=true;
        speed = maxSpeed;
        if(isCrouching)
        {
            speed /= 2;
            animator.speed = animatorSpeed;
        }
        isRunning = true;
        Vector3 scale = player.transform.localScale;
        if(scale.x <0)
            scale.x *=(-1);
        player.transform.localScale = scale;
    }

    public void rightButton_Up()
    {
        speed = 0;
        isRunning=false;
        if(isCrouching)
            animator.speed=0;
        else
            animator.speed = animatorSpeed;
    }

    public void interactButton_Down()
    {
        if(groundCheck())
        {
            isRolling = true;
            if(facingRight)
            {
                speed = rollSpeed;
            }
            if(!facingRight)
            {
                speed = -rollSpeed;
            }
        }
    }

    public void interactButton_Up()
    {
        
    }

    public void jumpButton_Down()
    {
        isCrouching=false;
        if(doubleJump)
        {
            rgbd2D.velocity = new Vector3(rgbd2D.velocity.x,jumpSpeed,0);
            doubleJump=false;
        }
        if(groundCheck())
        {
            rgbd2D.velocity = new Vector3(rgbd2D.velocity.x,jumpSpeed,0);
            doubleJump = true;
        }
    }

    public void crouchButton_Down()
    {
        if(isCrouching)
        {
            isCrouching=false;
            animator.speed = animatorSpeed;
        }
        else
        {
            isCrouching = true;
            animator.speed = 0;
        }
    }

    public void crouchButton_Up()
    {

    }

    public void eventCheck()
    {
        Debug.Log("Jump:" + rgbd2D.velocity );
    }

    public void jumpButton_Up()
    {
        
    }

    public void rollSpeedHandle()
    {
        if(speed != 0)
        {
            if(isRolling)
            {
                if(facingRight)
                    speed -=0.2f;
                if(!facingRight)
                    speed+=0.2f;
            }
        }
        if(Mathf.Abs(speed) < 5f)
        {
            isRolling = false;
            speed = 0;
        }
    }

    public void addForce(float speed)
    {
        rgbd2D.velocity = new Vector3(speed,rgbd2D.velocity.y,0);
    }

    private bool groundCheck()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,0f, Vector2.down,.1f,groundMask);
    }

    void updateStatus()
    {
        //grounded
        if(groundCheck())
        {
            animator.SetBool("isJumping", false);

            //crouching
            if(isCrouching)
            {
                animator.SetBool("isCrouching",true);
            }

            //not crouching
            if(!isCrouching)
            {
                animator.speed = animatorSpeed;
                animator.SetBool("isCrouching",false);
                //not running
                if(!isRunning)
                    animator.SetBool("isRunning",false);

                //running
                if(isRunning)
                    animator.SetBool("isRunning",true);
                
                if(isRolling)
                {
                    animator.SetBool("isRolling", true);
                    rollSpeedHandle();
                }

                if(!isRolling)
                {
                    animator.SetBool("isRolling", false);
                }
            }
        }

        //jump
        if(!groundCheck())
        {
            animator.SetFloat("yVelocity",rgbd2D.velocity.y);
            animator.SetBool("isJumping",true);
            animator.SetBool("isCrouching",false);
            animator.SetBool("isRolling", false);
            animator.SetBool("isRunning",false);
           // animator.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
            takeDamage(5);
    }

    private void takeDamage(int damage) {
        currentHealth-=damage;
        healthBar.setHealth(currentHealth);
    }
}

