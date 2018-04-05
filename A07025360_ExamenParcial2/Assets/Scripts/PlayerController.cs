using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float maxVel = 5f;
    private float yJumpForce = 500f;


    private Rigidbody2D rb;
    private LivesKeeper livesKeeper;
    private Animator anim;
    private Vector2 jumpForce;
    private bool isJumping = false;
    private bool isDucking = false;
    private bool movingRight = true;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        livesKeeper = GameObject.Find("Lives").GetComponent<LivesKeeper>();

        jumpForce = new Vector2(0, 0);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //	We update horizontal speed
        float v = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0, rb.velocity.y);

        v *= maxVel;

        vel.x = v;

        rb.velocity = vel;

        //	We change animations if needed
        if (v != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //	If the player jumps
        if (Input.GetAxis("Jump") > 0.01f)
        {
            if (!isJumping)
            {
                anim.SetBool("isJumping", true);
                isJumping = true;
                if (rb.velocity.y == 0)
                {
                    
                    jumpForce.x = 0f;
                    jumpForce.y = yJumpForce;
                    rb.AddForce(jumpForce);
                }
            }
        }
        else
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
        //if the player ducks
        if (Input.GetAxis("Vertical") < 0.00f)
        {
            if (!isDucking)
            {
                anim.SetBool("isDucking", true);
                isDucking = true;
               
            }
        }
        else
        {
            anim.SetBool("isDucking", false);
            isDucking = false;
        }

        if (movingRight && v < 0)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && v > 0)
        {
            movingRight = true;
            Flip();
        }
    }

    private void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //	We try to identify the object that collided with us as a projectile (laser beam).
        EnemyC enemy = collider.gameObject.GetComponent<EnemyC>();

        //	If our ship collided with a laser beam, we decrease our ship's health in the amount
        //	of damage set by the projectile.  If the ship's health is zero or less, then we destroy
        //	our ship.
        if (enemy)
        {
            livesKeeper.Hurt();
        }


    }

}