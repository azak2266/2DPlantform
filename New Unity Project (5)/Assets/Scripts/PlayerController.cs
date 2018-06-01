using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rb;
    public float speedBoost;
    public Boolean filp ;

    public Transform feet;
    public float boxWidth = 1f;
    public float boxHeight = 1f;

    public bool isGrounded = false;
    public LayerMask WhatisGround;

    SpriteRenderer sr;
    Animator anim;
    private bool isjumping = false;

    public float jumpSpeed;

    private bool canDoubleJump;
    
    private const int stateIdle = 0;
    private const int stateRunning = 1;
    private const int stateJump = 2;
    void Start () {
        //filp = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.OverlapBox(feet.position, new Vector2(boxWidth, boxHeight),360.0f, WhatisGround);
        float moveSpees = Input.GetAxisRaw("Horizontal");
        //flip(moveSpees);
        if (moveSpees!=0)
        {
            MoveHor(moveSpees);
        }
        else
        {
            StopMoving();
        }
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        /*if(rb.velocity.y<0)如果有掉下的動畫的話。
        {
            showFallling();
        }*/
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0f));
    }

    /*private void showFallling()
    {
        anim.SetInteger("State", 4);
    }*/

    private void Jump()
    {
        if(isGrounded)
        {
            isjumping = true;
            rb.AddForce(new Vector2(0, jumpSpeed));
            anim.SetInteger("State", stateJump);

            canDoubleJump = true;
            
        }
        else
        {
            if(canDoubleJump)
            {
                canDoubleJump = false;
                rb.velocity=new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0, jumpSpeed));
                anim.SetInteger("State", stateJump);
                
            }
        }
    }

    private void MoveHor(float speed)
    {
        Debug.Log("Move");
        if (speed > 0)
            sr.flipX = false;
        else if (speed < 0)
            sr.flipX = true;
        rb.velocity = new Vector2(speed * speedBoost, rb.velocity.y);
        if(!isjumping)
            anim.SetInteger("State", stateRunning);
    }

    private void StopMoving()
    {
        
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (isjumping == false)
            anim.SetInteger("State", stateIdle);
        
    }
    private void  OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isjumping = false;
        }
    }
    /*private void flip(float moveSpees)
    {
        if (moveSpees > 0&&!filp||moveSpees<0&&filp)
        {

            filp = !filp;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }*/
}
