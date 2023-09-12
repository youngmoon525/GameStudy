using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 4;
    // Start is called before the first frame update
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    //normalized

    void Update()
    {


        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if(rigid.velocity.normalized.x == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h , ForceMode2D.Impulse);
        
        if (rigid.velocity.x > maxSpeed)//Right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); 
        else if (rigid.velocity.x < maxSpeed * (-1))//Left
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
    }
}
