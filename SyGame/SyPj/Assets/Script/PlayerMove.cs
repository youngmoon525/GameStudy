using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed ;
    public float jumpPower ;
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

        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);

        }



        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if(rigid.velocity.normalized.x < 0.3)
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


        //Landing Platform
        Debug.DrawRay(rigid.position, Vector3.down , new Color(0,1,0));
        if (rigid.velocity.y < 0)
        {


            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("isJumping", false);
                }
                Debug.Log(rayHit.collider.name);
            }
        }
    }
}
