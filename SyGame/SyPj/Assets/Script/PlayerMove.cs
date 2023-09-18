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
    public GameManager gameManager;
    CapsuleCollider2D capsuleCollider2;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDameged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    AudioSource audioSrc;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2 = GetComponent<CapsuleCollider2D>();
        audioSrc = GetComponent<AudioSource>(); 
    }

    //normalized

    void Update()
    {

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            playSound(audioJump);

        }



        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
        {
            Debug.Log(Input.GetAxisRaw("Horizontal"));
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        /*        if (Mathf.Abs(rigid.velocity.x) < 0.3)
                    animator.SetBool("isWalking", false);
                else
                    animator.SetBool("isWalking", true);*/

        Debug.Log(rigid.velocity.normalized.x);
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
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
        //이동 속도
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h , ForceMode2D.Impulse);
        
        //최대 속도
        if (rigid.velocity.x > maxSpeed)//Right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); 
        else if (rigid.velocity.x < maxSpeed * (-1))//Left
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);



        //Landing Platform
        //Debug.DrawRay(rigid.position, Vector3.down , new Color(0,1,0));
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("isJumping", false);
                }
              //  Debug.Log(rayHit.collider.name);
            }
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if(isBronze)
                gameManager.stagePoint += 50;
            else if(isSilver)
                gameManager.stagePoint += 100;
            else if (isGold)
                gameManager.stagePoint += 300;
            playSound(audioItem);
            collision.gameObject.SetActive(false);
        } 
        else if (collision.gameObject.tag == "Finish")
        {
            playSound(audioFinish); 
            gameManager.NextStage();

            //NextStage
        }
    }

    //충돌 이벤트
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {   
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                onAttack(collision.transform);
                playSound(audioAttack);
            }
            else
            {
                OnDameged(collision);
                playSound(audioDameged);
            }
           // Debug.Log("충돌");
           
        }
    }

    void onAttack(Transform enemy)
    {

        gameManager.stagePoint += 100;

        rigid.AddForce(Vector2.up *10 , ForceMode2D.Impulse);


        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDameged();
    }

    void OnDameged(Collision2D collision)
    {
        gameManager.HealthDown();

        gameObject.layer = 11;//PlayerDameged로 Change Layer;
        
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //
        int dirc = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) *7, ForceMode2D.Impulse);

        //Animation
        animator.SetTrigger("doDameged");
        Invoke("OffDameged", 2);



    }

    void OffDameged()
    {
        gameObject.layer = 10;//PlayerDameged로 Change Layer;
        spriteRenderer.color = new Color(1, 1, 1, 1f);

    }

    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        capsuleCollider2.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        playSound(audioDie);

    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    public void playSound(AudioClip src)
    {
        audioSrc.clip = src;
        audioSrc.Play();
    }

}
