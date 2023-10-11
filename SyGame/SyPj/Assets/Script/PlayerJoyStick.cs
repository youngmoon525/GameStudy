using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerJoyStick : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    public float maxSpeed;
    public float jumpPower;
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

    PlayerActionsExample playerInput;
    AudioSource audioSrc;




    private void Awake()
    {
        playerInput = new PlayerActionsExample();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider2 = GetComponent<CapsuleCollider2D>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if(playerInput == null)
            playerInput = new PlayerActionsExample();

        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        //Vector3 move = new Vector3(movement.x, 0, movement.y);
      
        if (movement.x != 0)
        {
            //Debug.Log("NOT ZERO"+maxSpeed  + rigid.velocity.x);

            rigid.velocity = new Vector2(rigid.velocity.x * Mathf.Abs(movement.x), rigid.velocity.y);
            //rigid.AddForce(Vector2.right * rigid.velocity.x, ForceMode2D.Impulse);
            spriteRenderer.flipX = movement.x < 0;

            
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }



        //  bool jumpPress = playerInput.Player.Jump.IsPressed();
        bool jumpPress = playerInput.Player.Jump.triggered || playerInput.Player.Jump.IsPressed();
        if (jumpPress && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            playSound(audioJump);
        }

    }

    void FixedUpdate()
    {
        //이동 속도
        //float h = maxSpeed; //Input.GetAxisRaw("Horizontal");
        //Debug.Log("www" + h);
        //rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
       // Debug.Log(Vector2.right + " A "  +rigid.velocity.x + " B " + Vector2.right * rigid.velocity.x);
        rigid.AddForce(Vector2.right *playerInput.Player.Move.ReadValue<Vector2>().x, ForceMode2D.Impulse);
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




    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if (isBronze)
                gameManager.stagePoint += 50;
            else if (isSilver)
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
        if (collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
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

        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);


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
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

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
