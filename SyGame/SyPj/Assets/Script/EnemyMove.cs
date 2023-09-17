using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
    }

    void think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("think", 5);
    }

}
