using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    public static int speed =5;
    float h;
    float v;
    Rigidbody2D rigid;
    bool isHorizonMove;


    PlayerActionsExample playerInput;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = new PlayerActionsExample();
    }
    private void Update()
    {


        if (playerInput == null)
            playerInput = new PlayerActionsExample();
        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Debug.Log("x" + movement.x + " y" + movement.y);
       
         //   isHorizonMove = true;
            h = movement.x;
         //   isHorizonMove = false;
            v = movement.y;
  



        /*     h = Input.GetAxisRaw("Horizontal");
             v = Input.GetAxisRaw("Vertical");


             bool hDown = Input.GetButtonDown("Horizontal");
             bool vDown = Input.GetButtonDown("Vertical");
             bool hUp = Input.GetButtonUp("Horizontal");
             bool vUp = Input.GetButtonUp("Vertical");


             if (hDown || vUp)
                 isHorizonMove = true;
             else if (vDown || hUp)
                 isHorizonMove = false;*/



    }
    void FixedUpdate()
    {
         Debug.Log("x" + h + " y" + v);
        Vector2 moveVec = new Vector2(h, v);// isHorizonMove ? new Vector2(h, v) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;
    }


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
