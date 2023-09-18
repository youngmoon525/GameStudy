using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2; 
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        Invoke("think", 5);
        //think();
    }

    void Start()
    {
       
    }
    void Update()
    {
       
    }

     void FixedUpdate()
    {
     
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        if (rigid.velocity.y < 0)
        {


            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1f, LayerMask.GetMask("Platform"));
            if (rayHit.collider == null)
            {
                turn();
            }
        }

    }

    void think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
    
        anim.SetInteger("walkSpeed", nextMove);
        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
        Invoke("think", nextThinkTime);


    }

    void turn()
    {
        nextMove = nextMove * -1;
        CancelInvoke();
        Invoke("think", 5);
        spriteRenderer.flipX = nextMove == 1;
    }
    public void OnDameged()
    {
       // gameObject.layer = 11;//PlayerDameged�� Change Layer;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        boxCollider2.enabled = false;
        rigid.AddForce(Vector2.up * 5 , ForceMode2D.Impulse);
        Invoke("DeActive", 5);

    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
